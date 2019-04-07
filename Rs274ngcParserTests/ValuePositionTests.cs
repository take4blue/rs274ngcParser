using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Take4.Rs274ngcParser.Tests {
	[TestClass()]
	public class ValuePositionTests {
		[TestMethod]
		public void ValuePosition1() {
			var test = new ValuePosition('A');
			test.Value = 2.345;
			test.FP = 0;

			Assert.AreEqual(test.ToString(), "A2");

			test.FP = 1;
			Assert.AreEqual(test.ToString(), "A2.3");

			test.FP = 2;
			Assert.AreEqual(test.ToString(), "A2.35");

			test.FP = 4;
			Assert.AreEqual(test.ToString(), "A2.3450");
		}

		[TestMethod]
		public void ValuePosition2() {
			var test = new ValuePosition('A');
			test.Value = -2.345;
			test.FP = 0;

			Assert.AreEqual(test.ToString(), "A-2");

			test.FP = 1;
			Assert.AreEqual(test.ToString(), "A-2.3");

			test.FP = 2;
			Assert.AreEqual(test.ToString(), "A-2.35");

			test.FP = 4;
			Assert.AreEqual(test.ToString(), "A-2.3450");
		}

		[TestMethod]
		public void ValuePosition3() {
			var test = new ValuePosition('A');
			test.Value = -2.345;
			test.FP = -1;

			Assert.AreEqual(test.ToString(), "A");
		}
	}
}