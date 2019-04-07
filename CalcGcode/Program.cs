using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take4.Rs274ngcParser;
using System.IO;

namespace CalcGcode {
	internal class Coordinate {
		public Double X { get; set; } = 0.0;
		public Double Y { get; set; } = 0.0;
		public Double Z { get; set; } = 0.0;

		/// <summary>
		/// 値を更新する
		/// </summary>
		/// <param name="prev"></param>
		public void Update(ValueXYZEF prev) {
			X = prev.X;
			Y = prev.Y;
			Z = prev.Z;
		}

		/// <summary>
		/// 今回の値からの距離を測る
		/// </summary>
		/// <param name="current"></param>
		/// <returns>距離</returns>
		public Double LengthXYTo(ValueXYZEF current) {
			return Math.Sqrt(Math.Pow(current.X - X, 2) + Math.Pow(current.Y - Y, 2));
		}

		/// <summary>
		/// 今回の値からの距離を測る
		/// </summary>
		/// <param name="current"></param>
		/// <returns>距離</returns>
		public Double LengthZTo(ValueXYZEF current) {
			return Math.Abs(current.Z - Z);
		}
	}

	class CalcLength : ICommandActor {
		Coordinate prev_ = new Coordinate();
		ValueXYZEF current_ = new ValueXYZEF();
		Double totalE_ = 0.0;
		Double totalTime_ = 0.0;
		Double totalLength_ = 0.0;

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

		public void PostAction() {
			current_.PostAction();
			if (totalE_ == 0.0) {
				totalE_ = current_.E;
			}
			Console.Write(string.Format("Total Length {0}\nTotal Time {1}\nFillament {2}\n", totalLength_, totalTime_, totalE_));
		}

		public void PreAction() {
			current_.PreAction();
		}
	}

	class Program {
		static void Main(string[] args) {
			foreach(var arg in args) {
				if (File.Exists(arg)) {
					using (var stream = File.Open(arg, FileMode.Open)) {
						Console.WriteLine(arg);
						var actor = new CalcLength();
						var modifier = new ParseGCodeStream();
						ParseGCodeStream.SkipXGcode(stream);
						modifier.Parse(stream, actor);
					}
				}
			}
		}
	}
}
