using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Take4.Rs274ngcParser.Tests {
	[TestClass()]
	public class OutputGCodeTests {
		[TestMethod()]
		public void ToString1() {
			var source = new List<ValuePosition>();
			var result = OutputGCode.ToString(source);
			Assert.AreEqual(result.Length, 0);
		}

		[TestMethod()]
		public void ToString2() {
			var source = new List<ValuePosition>();
			var result = OutputGCode.ToString(source, "hoge");
			Assert.AreEqual(result, ";hoge");
		}

		[TestMethod()]
		public void ToString3() {
			var source = new List<ValuePosition>();
			var result = OutputGCode.ToString(source, ";hoge");
			Assert.AreEqual(result, ";hoge");
		}

		[TestMethod()]
		public void ToString4() {
			var source = new List<ValuePosition>();
			var result = OutputGCode.ToString(source, " ;hoge");
			Assert.AreEqual(result, "; ;hoge");
		}

		[TestMethod()]
		public void ToString5() {
			var source = new List<ValuePosition>();
			source.Add(new ValuePosition('G') { Value = 1.1, FP = 1, Position = 2 });
			var result = OutputGCode.ToString(source);
			Assert.AreEqual(result, "G1.1");
		}

		[TestMethod()]
		public void ToString6() {
			var source = new List<ValuePosition>();
			source.Add(new ValuePosition('G') { Value = 1.1, FP = 1, Position = 2 });
			source.Add(new ValuePosition('A') { Value = 2.222, FP = 2, Position = 0 });
			var result = OutputGCode.ToString(source);
			Assert.AreEqual(result, "G1.1 A2.22");
		}

		[TestMethod()]
		public void ToString7() {
			var source = new List<ValuePosition>();
			source.Add(new ValuePosition('G') { Value = 1.1, FP = 1, Position = 2 });
			source.Add(new ValuePosition('A') { Value = 2.222, FP = 2, Position = 0 });
			source.Add(new ValuePosition('Z') { Value = -3.33, FP = 0, Position = 1 });
			var result = OutputGCode.ToString(source);
			Assert.AreEqual(result, "G1.1 A2.22 Z-3");
		}

		[TestMethod()]
		public void ToString8() {
			var source = new List<ValuePosition>();
			source.Add(new ValuePosition('G') { Value = 1.1, FP = 1, Position = 2 });
			source.Add(new ValuePosition('A') { Value = 2.222, FP = 2, Position = 0 });
			source.Add(new ValuePosition('Z') { Value = -3.33, FP = 0, Position = 1 });
			var result = OutputGCode.ToString(source, "comment");
			Assert.AreEqual(result, "G1.1 A2.22 Z-3;comment");
		}

		[TestMethod]
		public void ToString9() {
			var source = new LineCommand();
			string line = "G1.1 A2.22 Z-3;comment";
			source.Parse(line);

			var result = OutputGCode.ToString(source);
			Assert.AreEqual(result, "G1.1 A2.22 Z-3;comment");
		}

		[TestMethod]
		public void ToString10() {
			var source = new LineCommand();
			string line = "G1.1 A2.22 Z-3;comment";
			source.Parse(line);

			var result = OutputGCode.ToString(source, false);
			Assert.AreEqual(result, "G1.1 A2.22 Z-3");
		}
	}
}