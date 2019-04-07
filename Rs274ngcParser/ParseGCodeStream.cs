using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Take4.Rs274ngcParser {
	/// <summary>
	/// Gコードファイルのパース処理
	/// </summary>
	public class ParseGCodeStream {
		/// <summary>
		/// 解析対象のファイルストリーム
		/// </summary>
		Stream inputStream_;

		/// <summary>
		/// 文字コード変換用
		/// </summary>
		ASCIIEncoding ascii_ = new ASCIIEncoding();

		/// <summary>
		/// ファイルの解析
		/// </summary>
		/// <param name="inputStream">ファイルストリーム</param>
		/// <param name="actor">コマンド処理用のインターフェースクラス</param>
		/// <returns>最後まで処理した場合true</returns>
		public bool Parse(Stream inputStream, ICommandActor actor) {
			inputStream_ = inputStream;
			return ParseLines(actor);
		}

		/// <summary>
		/// 1行の生データの読み込み
		/// </summary>
		/// <returns>行データ</returns>
		private string ReadLine() {
			var buffer = new List<Byte>();
			for (var val = inputStream_.ReadByte(); val != -1; val = inputStream_.ReadByte()) {
				buffer.Add((Byte)val);
				if (val == 0x0a) {
					break;
				}
			}
			return ascii_.GetString(buffer.ToArray());
		}

		/// <summary>
		/// EOFかどうか
		/// </summary>
		/// <returns>EOFの場合true</returns>
		private bool EndOfStream() {
			return inputStream_.Position >= inputStream_.Length;
		}

		private bool ParseLine(string line, ICommandActor actor) {
			var current = new LineCommand();
			if (current.Parse(line)) {
				return actor.ActionLine(current); ;
			}
			else {
				return false;
			}
		}

		private bool ParseLines(ICommandActor actor) {
			actor.PreAction();
			bool ret = true;
			while (!EndOfStream()) {
				if (!ParseLine(ReadLine(), actor)) {
					ret = false;
					break;
				}
			}
			actor.PostAction();
			return ret;
		}

		/// <summary>
		/// StreamからXGcodeのヘッダーをスキップした位置に持っていく
		/// </summary>
		/// <param name="inputFile"></param>
		public static void SkipXGcode(Stream inputFile) {
			// 1行目相当を読み込み、ファイル種別を取得する
			var buffer = new List<Byte>();
			for (var val = inputFile.ReadByte(); val != -1; val = inputFile.ReadByte()) {
				buffer.Add((Byte)val);
				if (val == 0x0a) {
					break;
				}
			}
			var firstLine = Encoding.ASCII.GetString(buffer.ToArray());

			if (firstLine.StartsWith("xgcode 1.0")) {
				int headerSize;
				{
					// まずヘッダーに関する情報を取得、出力する。
					var work = new byte[0x20 - firstLine.Length];
					var size = inputFile.Read(work, 0, work.Length);
					headerSize = BitConverter.ToInt32(work, 0x14 - firstLine.Length);
				}
				{
					var work = new byte[headerSize - 0x20];
					var size = inputFile.Read(work, 0, work.Length);
				}
			}
			else {
				inputFile.Seek(0, SeekOrigin.Begin);
			}
		}
	}
}
