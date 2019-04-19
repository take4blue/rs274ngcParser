# RS232-NGC Parser
3D�v�����^�[�̃X���C�T�[�\�t�g����o�͂��ꂽG�R�[�h�t�@�C������́E�C�����邽�߂ɍ�������C�u�����ƁA������g�p���T���v���Ƃ��č쐬�������p�v���O�����ł��B

A library created to analyze and correct G code files output from 3D printer slicer software, and an application program created using it as a sample.

# �\��(Solution structure)
## rs274ngcparser
G�R�[�h�t�@�C���̍\������͂��邽�́A��{���C�u�����ł��B
�x�[�X�ƂȂ�̂́AICommandActor,ParseGCodeStream,LineCommand�ŁAICommandActor���g�p�����N���X��ParseGCodeStream.Parse�ɓn�����ƂŁA��͏��������{���܂��B

A basic library for analyzing the structure of G code files.
The base is ICommandActor, ParseGCodeStream, and LineCommand, and pass the class using ICommandActor to ParseGCodeStream.Parse to perform parsing processing.

## Translator
FlashPrint, Simplify3D, Slic3r����o�͂��ꂽG�R�[�h�t�@�C�����AFlashForge��Adventurer3�ɓǂݍ��܂��邽�߂̕ϊ��N���X�ł��B
ToAdventurer3�͕ϊ�����@�\�̂ق��ɁA������G�R�[�h�t�@�C����␳���鏈��������Ă���܂��B

This is a conversion class for reading FlashCode, Simplify3D, and Gliccode files output from Slic3r into FlashForge's Adventurer3.
In addition to the conversion function, ToAdventurer3 includes some processing to correct G code files.

## TranlatorToAdventurer3
translator�̃��C�u�������g�����A�R���\�[���v���O�����ł��B

Console program using translator's library.

## CalcGcode
G�R�[�h�̈ړ��ʁA�t�B�������g�̓f�o�ʁA����ɂ����鎞�Ԃ��t�@�C�����甲���o���A�v�Z����A�R���\�[���v���O�����ł��B

This is a console program that extracts G-code movement amount, filament discharge amount, and printing time from files and calculates them.

# �N���X���p���@(Class usage)
CalcGcode���ɐ������܂��B

G�R�[�h�t�@�C������͂���ɂ́AICommandActor�����������N���X��p�ӂ��A�����ParseGCodeStream.Parse�ɓn�����ƂŎ������܂��B
ICommandActor��3�̃C���^�[�t�F�[�X���\�b�h�������܂����A�̂́Abool ActionLine(LineCommand line)�ł��B
ParseGCodeStream.Parse�ł̓t�@�C����1�s��ǂݍ��񂾂̂��A���̍s�̉�͌��ʂ�LineCommand��ICommandActor.ActionLine�ɓn���܂��B
LineCommand�͓ǂݍ��܂ꂽ�s���t�B�[���h�ɕ������A���̃t�B�[���h�̕����Ɛ��l�ɕ������ĕێ����Ă��܂��B
�Ⴆ��X10 Y11�Ƃ���1�s���������ꍇ�ALineCommand�ɂ�{X,10.0}{Y,11.0}�Ƃ���2�t�B�[���h�̏���ێ����A���̏������@��ActionLine�ɈϏ����܂��B
ActionLine���ł́A���̏�����͂��A�C���A�����Ȃǂ��s�����ƂɂȂ�܂��B

�܂�G�R�[�h��XYZ�Ɋւ��ẮA���΍��W�E��΍��W�w���A�C���`�E�~���w��Ȃǂɂ����W�ϊ�������܂��B
�������Ɋւ��ẮAValueXYZEF�N���X�ł��̂�����̏������ł���悤�ɁA�p�ӂ��Ă��܂��B

CalcGcode�ł�ValueXYZEF�𗘗p���AXYZEF�̏�񂩂�A�ړ��ʂ�f�o�ʂ̌v�Z�����Ă��܂��B

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
current_��ValueXYSEF�ł��B
�܂�current_.ActionLine���Ăяo���AXYZEF�̒l���m�肵�܂��B
���̌�AG1���߂ł���΁AXYZ�̈ړ��ʂ����߁A������ړ����Ԃɕϊ��AG92���߂ł���΁AE�̓f�o�ʂ̌v�Z�����Ă��܂��B
���ꂮ�炢�̃R�[�h�ʂŁA�ړ��ʁE�ړ����ԁE�f�o�ʂ̌v�Z���ł��܂��B

current_ is ValueXYSEF.
First call current_.ActionLine to determine the value of XYZEF.
After that, if it is a G1 instruction, the movement amount of XYZ is obtained, converted to the length and movement time, and if it is a G92 instruction, the ejection amount of E is calculated.
With this amount of code, it is possible to calculate movement amount, movement time, and discharge amount.

## �֗��ȃT�|�[�g�N���X(Convenient support class)
### OutputGCode
��͏��ł���LineCommand��ValuePosition���ēx�����񉻂��܂��B
�Ⴆ�΁AActionLine�Ŏ󂯎����LineCommand�����̂܂ܓn�����ƂŁA���͂��ꂽ�t�@�C����1�s�Ƃقړ���������𓾂邱�Ƃ��ł��܂��B

String the analysis information LineCommand and ValuePosition again.
For example, by passing the LineCommand received by ActionLine as it is, it is possible to obtain the same character string as one line of the input file.

### ValueXYZEF
��̂ق��ł��L�ڂ�����܂����ALineCommand�̒�����XYZEF�̒l�݂̂Ƀt�H�[�J�X�𓖂Ă���͏����ł��B
G�R�[�h�̐�Βl�E���Βl�w���C���`�E�~���w����󂯎��AXYZEF�̃~����Βl�Ƃ��ď���ێ����Ă��܂��B

As described above, this analysis process focuses on only the values of XYZEF in LineCommand.
It also receives G-code absolute value, relative value specification, inch and millimeter specification, and holds the information as the millimeter absolute value of XYZEF.

# License
This software is released under the MIT License, see LICENSE.txt.