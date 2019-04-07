using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Take4.TestSupport;

namespace Take4.Translator.Tests {

	[TestClass()]
	public class Slic3rToBaseTests {
		[TestMethod()]
		[DeploymentItem(@"Slic3rToBaseTestData\test1.g")]
		[DeploymentItem(@"Slic3rToBaseTestData\result1.g")]
		public void ModifyTest() {
			using (var input = File.OpenRead(@"Slic3rToBaseTestData\test1.g")) {
				using (var output = File.Create(@"Slic3rToBaseTestData\output1.g")) {
					var process = new Slic3rToBase();
					process.SpeedZ = 30;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqualText(@"Slic3rToBaseTestData\result1.g", @"Slic3rToBaseTestData\output1.g");
		}

		/// <summary>
		/// コンストラクタでSpeedZの値の確認
		/// </summary>
		[TestMethod]
		public void Construct1() {
			var test = new Slic3rToBase();
			Assert.AreEqual(test.SpeedZ, 30);
		}
	}
}