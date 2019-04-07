using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Take4.Rs274ngcParser.Tests {
	[TestClass()]
	public class LineCommandTests {
		[TestMethod()]
		public void Comment1() {
			string line = ";hoge";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, line);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count == 0, true);
		}

		[TestMethod]
		public void Comment2() {
			string line = "   ;hoge";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, ";hoge");
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count == 0, true);
		}

		[TestMethod]
		public void NullLine() {
			string line = "";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count == 0, true);
		}

		[TestMethod]
		public void Value1() {
			string line = "G1";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 1);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 1.0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 0);
				Assert.AreEqual(test.Has('G'), true);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
				Assert.AreEqual(value, 0.0);
				Assert.AreEqual(pos, -1);
				Assert.AreEqual(test.Has('A'), false);
			}
		}

		[TestMethod]
		public void Value2() {
			string line = "G1.0";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 1);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 1.0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 1);
				Assert.AreEqual(test.Has('G'), true);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
				Assert.AreEqual(test.Has('A'), false);
			}
		}

		[TestMethod]
		public void Value3() {
			string line = "G11.0001";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 1);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 11.0001);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 4);
				Assert.AreEqual(test.Has('G'), true);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
				Assert.AreEqual(test.Has('A'), false);
			}
		}

		[TestMethod]
		public void Value4() {
			string line = "G";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 1);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), -1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
				Assert.AreEqual(value, 0.0);
				Assert.AreEqual(pos, -1);
			}
		}

		[TestMethod]
		public void Values1() {
			string line = "G11.1 Z22.22";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 11.1);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 1);
				Assert.AreEqual(test.Has('G'), true);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 22.22);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('Z'), 2);
				Assert.AreEqual(test.Has('Z'), true);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Values2() {
			string line = "Z11 G22";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 22);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('G'), 0);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 11);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('Z'), 0);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Values3() {
			string line = "Z G";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 0);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('G'), -1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('Z'), -1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Values4() {
			string line = "ZG";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 0);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('G'), -1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('Z'), -1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Values5() {
			string line = "ABCDE";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Count, 5);
			Assert.AreEqual(test.GetKey(-1), ' ');
			Assert.AreEqual(test.GetKey(5), ' ');
			Assert.AreEqual(test.GetKey(0), 'A');
			Assert.AreEqual(test.GetKey(1), 'B');
			Assert.AreEqual(test.GetKey(2), 'C');
			Assert.AreEqual(test.GetKey(3), 'D');
			Assert.AreEqual(test.GetKey(4), 'E');
		}

		[TestMethod]
		public void Values6() {
			string line = "G1 Z5.5";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Modify('A', (x) => {
				x.Value = 5;
			}), false);
			Assert.AreEqual(test.Modify('G', (x) => {
				x.Value = 5;
				x.FP = 1;
				x.Position = 2;
			}), true);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 5);
				Assert.AreEqual(pos, 2);
				Assert.AreEqual(test.GetFP('G'), 1);
			}
		}

		[TestMethod]
		public void Complex1() {
			string line = "Z11 G22 ; hoge";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, "; hoge");
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 22);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('G'), 0);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 11);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('Z'), 0);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Complex2() {
			string line = "Z11 ; hoge G22";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, "; hoge G22");
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 1);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 11);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('Z'), 0);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), false);
				Assert.AreEqual(test.GetFP('G'), 0);
			}
		}


		[TestMethod]
		public void CloneValues1() {
			string line = "G11.1 Z22.22";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), true);
			Assert.AreEqual(test.Comment, null);
			Assert.AreEqual(test.Original, line);
			Assert.AreEqual(test.Count, 2);

			var val = test.CloneValues();
			Assert.AreEqual(val.Count, 2);
			Assert.AreEqual(val[0].Key, 'G');
			Assert.AreEqual(val[0].Value, 11.1);
			Assert.AreEqual(val[0].FP, 1);
			Assert.AreEqual(val[0].Position, 0);
			Assert.AreEqual(val[1].Key, 'Z');
			Assert.AreEqual(val[1].Value, 22.22);
			Assert.AreEqual(val[1].FP, 2);
			Assert.AreEqual(val[1].Position, 1);

			// CloneValuesでもらったデータを変更したら、オリジナル側が変わっていないことも確認
			val[0].Value = 22.3;
			val[0].FP = 0;
			val[0].Position = 1;
			val[1].Value = 33.3;
			val[1].FP = 0;
			val[1].Position = 0;

			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 11.1);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 1);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('Z', out value, out pos), true);
				Assert.AreEqual(value, 22.22);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('Z'), 2);
			}
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('A', out value, out pos), false);
				Assert.AreEqual(test.GetFP('A'), 0);
			}
		}

		[TestMethod]
		public void Error1() {
			string line = "Z11 Z12";
			var test = new LineCommand();
			Assert.AreEqual(test.Parse(line), false);
		}

		[TestMethod]
		public void Add1() {
			var test = new LineCommand();
			Assert.AreEqual(test.Add('G', 10.1, 1), true);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 10.1);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 1);
			}
		}

		[TestMethod]
		public void Add2() {
			var line = "X10.0";
			var test = new LineCommand();
			test.Parse(line);
			Assert.AreEqual(test.Add('G', 10.1, 1), true);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 10.1);
				Assert.AreEqual(pos, 1);
				Assert.AreEqual(test.GetFP('G'), 1);
			}
		}

		[TestMethod]
		public void Add3() {
			var line = "G10.00";
			var test = new LineCommand();
			test.Parse(line);
			Assert.AreEqual(test.Add('G', 10.1, 1), false);
			{
				Double value;
				int pos;
				Assert.AreEqual(test.TryGetValue('G', out value, out pos), true);
				Assert.AreEqual(value, 10.0);
				Assert.AreEqual(pos, 0);
				Assert.AreEqual(test.GetFP('G'), 2);
			}
		}
	}
}