using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Take4.Rs274ngcParser.Tests {
	[TestClass()]
	public class ValueXYZEFTests {
		[TestMethod()]
		public void ActionLineTest1() {
			string line = "G1 X1 Y2 Z3 E4 F5";
			var parser = new LineCommand();
			parser.Parse(line);
			var test = new ValueXYZEF();
			test.PreAction();
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 1);
			Assert.AreEqual(test.Y, 2);
			Assert.AreEqual(test.Z, 3);
			Assert.AreEqual(test.E, 4);
			Assert.AreEqual(test.F, 5);
		}

		[TestMethod()]
		public void ActionLineTest2() {
			string line = "";
			var parser = new LineCommand();
			parser.Parse(line);
			var test = new ValueXYZEF();
			test.PreAction();
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 0);
			Assert.AreEqual(test.Y, 0);
			Assert.AreEqual(test.Z, 0);
			Assert.AreEqual(test.E, 0);
			Assert.AreEqual(test.F, 0);
		}

		[TestMethod()]
		public void ActionLineTest3() {
			string line = "G1 X1 Y2 E4 F5";
			var parser = new LineCommand();
			parser.Parse(line);
			var test = new ValueXYZEF();
			test.PreAction();
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 1);
			Assert.AreEqual(test.Y, 2);
			Assert.AreEqual(test.Z, 0);
			Assert.AreEqual(test.E, 4);
			Assert.AreEqual(test.F, 5);
		}

		[TestMethod()]
		public void ActionLineTest4() {
			string line = "X1 Y2 Z3 E4 F5";
			var parser = new LineCommand();
			parser.Parse(line);
			var test = new ValueXYZEF();
			test.PreAction();
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 0);
			Assert.AreEqual(test.Y, 0);
			Assert.AreEqual(test.Z, 0);
			Assert.AreEqual(test.E, 0);
			Assert.AreEqual(test.F, 0);
		}

		[TestMethod()]
		public void ActionLineTestAbsUnit() {
			var test = new ValueXYZEF();
			test.PreAction();

			string line = "G20";
			var parser = new LineCommand();
			parser.Parse(line);
			test.ActionLine(parser);
			Assert.AreEqual(test.IsMM, false);

			line = "G1 X1 Y2 Z3 E4 F5";
			parser.Parse(line);
			test.ActionLine(parser);

			Assert.AreEqual(test.X, 2.54 * 1);
			Assert.AreEqual(test.Y, 2.54 * 2);
			Assert.AreEqual(test.Z, 2.54 * 3);
			Assert.AreEqual(test.E, 2.54 * 4);
			Assert.AreEqual(test.F, 2.54 * 5);
			line = "G21";
			parser.Parse(line);
			test.ActionLine(parser);
			Assert.AreEqual(test.IsMM, true);

			line = "G1 X1 Y2 Z3 E4 F5";
			parser.Parse(line);
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 1);
			Assert.AreEqual(test.Y, 2);
			Assert.AreEqual(test.Z, 3);
			Assert.AreEqual(test.E, 4);
			Assert.AreEqual(test.F, 5);
		}

		[TestMethod()]
		public void ActionLineTestAbs() {
			string line = "G1 X1 Y2 Z3 E4 F5";
			var parser = new LineCommand();
			parser.Parse(line);
			var test = new ValueXYZEF();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;
			test.ActionLine(parser);
			Assert.AreEqual(test.X, 1);
			Assert.AreEqual(test.Y, 2);
			Assert.AreEqual(test.Z, 3);
			Assert.AreEqual(test.E, 4);
			Assert.AreEqual(test.F, 5);
		}

		[TestMethod()]
		public void ActionLineTestInc1() {
			var test = new ValueXYZEF();
			test.PreAction();

			string line = "G91";
			var parser = new LineCommand();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;
			parser.Parse(line);
			test.ActionLine(parser);

			line = "G1 X1 Y2 Z3 E4 F5";
			parser.Parse(line);
			test.ActionLine(parser);

			Assert.AreEqual(test.X, 1.1);
			Assert.AreEqual(test.Y, 2.2);
			Assert.AreEqual(test.Z, 3.3);
			Assert.AreEqual(test.E, 4.4);
			Assert.AreEqual(test.F, 5.5);
		}

		[TestMethod()]
		public void ActionLineTestInc2() {
			var test = new ValueXYZEF();
			test.PreAction();

			string line = "G91";
			var parser = new LineCommand();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;
			parser.Parse(line);
			test.ActionLine(parser);

			line = "G1 X-1 Y-2 Z-3 E-4 F-5";
			parser.Parse(line);
			test.ActionLine(parser);

			Assert.AreEqual(test.X, -1 + 0.1);
			Assert.AreEqual(test.Y, -2 + 0.2);
			Assert.AreEqual(test.Z, -3 + 0.3);
			Assert.AreEqual(test.E, -4 + 0.4);
			Assert.AreEqual(test.F, -5 + 0.5);
		}

		[TestMethod()]
		public void ActionLineTestToAbs() {
			var test = new ValueXYZEF();
			test.PreAction();

			string line = "G91";
			var parser = new LineCommand();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;
			parser.Parse(line);
			test.ActionLine(parser);
			Assert.AreEqual(test.IsAbsolute, false);

			line = "G90";
			parser.Parse(line);
			test.ActionLine(parser);
			Assert.AreEqual(test.IsAbsolute, true);

			line = "G1 X1 Y2 Z3 E4 F5";
			parser.Parse(line);
			test.ActionLine(parser);

			Assert.AreEqual(test.X, 1);
			Assert.AreEqual(test.Y, 2);
			Assert.AreEqual(test.Z, 3);
			Assert.AreEqual(test.E, 4);
			Assert.AreEqual(test.F, 5);
		}

		[TestMethod()]
		public void ActionLineTestIncUnit() {
			var test = new ValueXYZEF();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;

			string line = "G20";
			var parser = new LineCommand();
			parser.Parse(line);
			test.ActionLine(parser);
			line = "G91";
			parser.Parse(line);
			test.ActionLine(parser);

			line = "G1 X1 Y2 Z3 E4 F5";
			parser.Parse(line);
			test.ActionLine(parser);

			Assert.AreEqual(test.X, 2.54 * 1 + 0.1);
			Assert.AreEqual(test.Y, 2.54 * 2 + 0.2);
			Assert.AreEqual(test.Z, 2.54 * 3 + 0.3);
			Assert.AreEqual(test.E, 2.54 * 4 + 0.4);
			Assert.AreEqual(test.F, 2.54 * 5 + 0.5);
		}

		[TestMethod()]
		public void PreActionTest() {
			var test = new ValueXYZEF();
			test.X = 0.1;
			test.Y = 0.2;
			test.Z = 0.3;
			test.E = 0.4;
			test.F = 0.5;
			test.PreAction();
			Assert.AreEqual(test.X, 0);
			Assert.AreEqual(test.Y, 0);
			Assert.AreEqual(test.Z, 0);
			Assert.AreEqual(test.E, 0);
			Assert.AreEqual(test.F, 0);
		}
	}
}