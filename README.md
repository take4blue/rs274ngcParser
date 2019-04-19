# RS232-NGC Parser
3Dプリンターのスライサーソフトから出力されたGコードファイルを解析・修正するために作ったライブラリと、それを使用しサンプルとして作成した応用プログラムです。

A library created to analyze and correct G code files output from 3D printer slicer software, and an application program created using it as a sample.

# 構成(Solution structure)
## rs274ngcparser
Gコードファイルの構造を解析するたの、基本ライブラリです。
ベースとなるのは、ICommandActor,ParseGCodeStream,LineCommandで、ICommandActorを使用したクラスをParseGCodeStream.Parseに渡すことで、解析処理を実施します。

A basic library for analyzing the structure of G code files.
The base is ICommandActor, ParseGCodeStream, and LineCommand, and pass the class using ICommandActor to ParseGCodeStream.Parse to perform parsing processing.

## Translator
FlashPrint, Simplify3D, Slic3rから出力されたGコードファイルを、FlashForgeのAdventurer3に読み込ませるための変換クラスです。
ToAdventurer3は変換する機能のほかに、いくつかGコードファイルを補正する処理も入れてあります。

This is a conversion class for reading FlashCode, Simplify3D, and Gliccode files output from Slic3r into FlashForge's Adventurer3.
In addition to the conversion function, ToAdventurer3 includes some processing to correct G code files.

## TranlatorToAdventurer3
translatorのライブラリを使った、コンソールプログラムです。

Console program using translator's library.

## CalcGcode
Gコードの移動量、フィラメントの吐出量、印刷にかかる時間をファイルから抜き出し、計算する、コンソールプログラムです。

This is a console program that extracts G-code movement amount, filament discharge amount, and printing time from files and calculates them.

# クラス利用方法(Class usage)
CalcGcodeを例に説明します。

Gコードファイルを解析するには、ICommandActorを実装したクラスを用意し、それをParseGCodeStream.Parseに渡すことで実現します。
ICommandActorは3つのインターフェースメソッドを持ちますが、肝は、bool ActionLine(LineCommand line)です。
ParseGCodeStream.Parseではファイルの1行を読み込んだのち、その行の解析結果をLineCommandでICommandActor.ActionLineに渡します。
LineCommandは読み込まれた行をフィールドに分解し、そのフィールドの文字と数値に分解して保持しています。
例えばX10 Y11という1行があった場合、LineCommandには{X,10.0}{Y,11.0}という2フィールドの情報を保持し、その処理方法をActionLineに委譲します。
ActionLine側では、その情報を解析し、修正、消去などを行うことになります。

またGコードのXYZに関しては、相対座標・絶対座標指定や、インチ・ミリ指定などによる座標変換が入ります。
これら情報に関しては、ValueXYZEFクラスでそのあたりの処理ができるように、用意しています。

CalcGcodeではValueXYZEFを利用し、XYZEFの情報から、移動量や吐出量の計算をしています。

I will explain CalcGcode as an example.

To parse G code file, prepare a class that implements ICommandActor and pass it to ParseGCodeStream.Parse.
ICommandActor has 3 interface methods, but the heart is bool ActionLine (LineCommand line).
ParseGCodeStream.Parse reads one line of the file and passes the analysis result of that line to ICommandActor.ActionLine with LineCommand.
LineCommand separates read lines into fields, and separates them into characters and numbers of the fields.
For example, if there is one line of X10 Y11, LineCommand holds information of 2 fields of {X, 10.0} {Y, 11.0}, and delegates the processing method to ActionLine.
On the ActionLine side, the information will be analyzed, corrected and deleted.

In addition, with regard to XYZ of the G code, coordinate conversion by relative coordinate / absolute coordinate specification, inch / mill specification, etc. is included.
We have prepared such information so that it can be processed by the ValueXYZEF class.

CalcGcode uses ValueXYSEF to calculate the movement amount and discharge amount from the information of XYZEF.

```csharp
public bool ActionLine(LineCommand line) {
	bool hasXY = line.Has('X') || line.Has('Y');
	bool hasZ = line.Has('Z');
	Double prevE = current_.E;
	if (current_.ActionLine(line)) {
		Double value;
		int position;
		if (line.TryGetValue('G', out value, out position)) {
			switch (value) {
			case 92:
				if (line.Has('E')) {
					totalE_ += prevE;
				}
				break;
			case 1:
				if (hasXY) {
					totalLength_ += prev_.LengthXYTo(current_);
					totalTime_ += prev_.LengthXYTo(current_) / current_.F;
				}
				if (hasZ) {
					totalLength_ += prev_.LengthZTo(current_);
					totalTime_ += prev_.LengthZTo(current_) / current_.F;
				}
				break;
			}
		}
		prev_.Update(current_);
		return true;
	}
	return false;
}
```
current_はValueXYSEFです。
まずcurrent_.ActionLineを呼び出し、XYZEFの値を確定します。
その後、G1命令であれば、XYZの移動量を求め、長さや移動時間に変換、G92命令であれば、Eの吐出量の計算をしています。
これぐらいのコード量で、移動量・移動時間・吐出量の計算ができます。

current_ is ValueXYSEF.
First call current_.ActionLine to determine the value of XYZEF.
After that, if it is a G1 instruction, the movement amount of XYZ is obtained, converted to the length and movement time, and if it is a G92 instruction, the ejection amount of E is calculated.
With this amount of code, it is possible to calculate movement amount, movement time, and discharge amount.

## 便利なサポートクラス(Convenient support class)
### OutputGCode
解析情報であるLineCommandやValuePositionを再度文字列化します。
例えば、ActionLineで受け取ったLineCommandをそのまま渡すことで、入力されたファイルの1行とほぼ同じ文字列を得ることができます。

String the analysis information LineCommand and ValuePosition again.
For example, by passing the LineCommand received by ActionLine as it is, it is possible to obtain the same character string as one line of the input file.

### ValueXYZEF
上のほうでも記載がありますが、LineCommandの中からXYZEFの値のみにフォーカスを当てた解析処理です。
Gコードの絶対値・相対値指定やインチ・ミリ指定も受け取り、XYZEFのミリ絶対値として情報を保持しています。

As described above, this analysis process focuses on only the values of XYZEF in LineCommand.
It also receives G-code absolute value, relative value specification, inch and millimeter specification, and holds the information as the millimeter absolute value of XYZEF.

# License
This software is released under the MIT License, see LICENSE.txt.