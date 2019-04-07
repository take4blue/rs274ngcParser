using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Take4.Translator.Tests {
	[TestClass()]
	public class ToAdventurer3ParameterTests {
		[TestMethod()]
		public void FileModifyParameterTest() {
			var data = new ToAdventurer3Parameter();
			Assert.AreEqual(data.EnclosureFanOn, false);
			Assert.AreEqual(data.MotorA, 100);
			Assert.AreEqual(data.MotorB, 20);
			Assert.AreEqual(data.MotorX, 100);
			Assert.AreEqual(data.MotorY, 100);
			Assert.AreEqual(data.MotorZ, 40);
			Assert.AreEqual(data.PlayRemovalLength, 0.5);
			Assert.AreEqual(data.OffsetZ, 0.0);
			Assert.AreEqual(data.BrimSpeedTypeValue, ToAdventurer3Parameter.BrimSpeedType.NoChange);
			Assert.AreEqual(data.BrimSpeed, 420);
			Assert.AreEqual(data.BrimSpeedRatio, 50);
			Assert.AreEqual(data.BrimExtrudeRatio, 20);
		}

		[TestMethod()]
		public void setTest() {
			var data1 = new ToAdventurer3Parameter();
			var data2 = new ToAdventurer3Parameter();
			data2.EnclosureFanOn = true;
			data2.MotorA = 1;
			data2.MotorB = 2;
			data2.MotorX = 3;
			data2.MotorY = 4;
			data2.MotorZ = 5;
			data2.PlayRemovalLength = 1.0;
			data2.OffsetZ = 2.0;
			data2.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.Absolute;
			data2.BrimSpeed = 6;
			data2.BrimSpeedRatio = 7;
			data2.BrimExtrudeRatio = 8;
			data1.Set(data2);
			Assert.AreEqual(data1.EnclosureFanOn, true);
			Assert.AreEqual(data1.MotorA, 1);
			Assert.AreEqual(data1.MotorB, 2);
			Assert.AreEqual(data1.MotorX, 3);
			Assert.AreEqual(data1.MotorY, 4);
			Assert.AreEqual(data1.MotorZ, 5);
			Assert.AreEqual(data1.PlayRemovalLength, 1.0);
			Assert.AreEqual(data1.OffsetZ, 2.0);
			Assert.AreEqual(data1.BrimSpeedTypeValue, ToAdventurer3Parameter.BrimSpeedType.Absolute);
			Assert.AreEqual(data1.BrimSpeed, 6);
			Assert.AreEqual(data1.BrimSpeedRatio, 7);
			Assert.AreEqual(data1.BrimExtrudeRatio, 8);
		}

		[TestMethod]
		public void check1() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				var data1 = new ToAdventurer3Parameter();
				var ret = data1.IsValid(true);
				Assert.AreEqual(ret.Count, 0);
				ret = data1.IsValid(false);
				Assert.AreEqual(ret.Count, 0);
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}
		[TestMethod]
		public void check2() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorA = 100;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorB = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorB = 100;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorX = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorX = 100;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorY = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorY = 100;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorZ = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorZ = 100;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeedRatio = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimSpeedRatio = 999;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimExtrudeRatio = 1;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimExtrudeRatio = 999;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.PlayRemovalLength = -.004;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.PlayRemovalLength = 10.004;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.OffsetZ = -10.004;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.OffsetZ = 10.004;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeed = 60;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimSpeed = 4859;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 0);
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		[TestMethod]
		public void check3() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorA = 100;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorB = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorB = 100;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorX = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorX = 100;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorY = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorY = 100;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorZ = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.MotorZ = 100;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeedRatio = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimSpeedRatio = 999;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimExtrudeRatio = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimExtrudeRatio = 999;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.PlayRemovalLength = -.004;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.PlayRemovalLength = 10.004;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.OffsetZ = -10.004;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.OffsetZ = 10.004;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeed = 1;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
					data1.BrimSpeed = 4800;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 0);
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		[TestMethod]
		public void check4() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
					data1.MotorA = 101;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorB = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
					data1.MotorB = 101;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorX = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
					data1.MotorX = 101;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorY = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
					data1.MotorY = 101;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorZ = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
					data1.MotorZ = 101;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeedRatio = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
					data1.BrimSpeedRatio = 1000;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimExtrudeRatio = 0;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
					data1.BrimExtrudeRatio = 1000;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.PlayRemovalLength = -.005;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
					data1.PlayRemovalLength = 10.005;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.OffsetZ = -10.005;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
					data1.OffsetZ = 10.005;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeed = 59;
					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から80の範囲で入力してください。");
					data1.BrimSpeed = 4860;
					ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から80の範囲で入力してください。");
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		[TestMethod]
		public void check5() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
					data1.MotorA = 101;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorB = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
					data1.MotorB = 101;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorX = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
					data1.MotorX = 101;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorY = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
					data1.MotorY = 101;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorZ = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
					data1.MotorZ = 101;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeedRatio = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
					data1.BrimSpeedRatio = 1000;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimExtrudeRatio = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
					data1.BrimExtrudeRatio = 1000;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.PlayRemovalLength = -.005;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
					data1.PlayRemovalLength = 10.005;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.OffsetZ = -10.005;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
					data1.OffsetZ = 10.005;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
				}
				{
					var data1 = new ToAdventurer3Parameter();
					data1.BrimSpeed = 0;
					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から4800の範囲で入力してください。");
					data1.BrimSpeed = 4801;
					ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 1);
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から4800の範囲で入力してください。");
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		[TestMethod]
		public void check6() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 0;
					data1.MotorB = 0;
					data1.MotorX = 0;
					data1.MotorY = 0;
					data1.MotorZ = 0;
					data1.BrimSpeedRatio = 0;
					data1.BrimExtrudeRatio = 0;
					data1.PlayRemovalLength = -.005;
					data1.OffsetZ = -10.005;
					data1.BrimSpeed = 59;

					var ret = data1.IsValid(true);
					Assert.AreEqual(ret.Count, 10);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から80の範囲で入力してください。");
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		[TestMethod]
		public void check7() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 0;
					data1.MotorB = 0;
					data1.MotorX = 0;
					data1.MotorY = 0;
					data1.MotorZ = 0;
					data1.BrimSpeedRatio = 0;
					data1.BrimExtrudeRatio = 0;
					data1.PlayRemovalLength = -.005;
					data1.OffsetZ = -10.005;
					data1.BrimSpeed = 0;

					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 10);
					Assert.AreEqual(ret["MotorA"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorB"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorX"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorY"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["MotorZ"], "この値は1から100の範囲で入力してください。");
					Assert.AreEqual(ret["BrimSpeedRatio"], "この値は1から999の範囲で入力してください。");
					Assert.AreEqual(ret["BrimExtrudeRatio"], "この値は1から999の範囲で入力してください。");
					Assert.AreEqual(ret["PlayRemovalLength"], "この値は0から10の範囲で入力してください。");
					Assert.AreEqual(ret["OffsetZ"], "この値は-10から10の範囲で入力してください。");
					Assert.AreEqual(ret["BrimSpeed"], "この値は1から4800の範囲で入力してください。");
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}
		[TestMethod]
		public void check8() {
			var original = Thread.CurrentThread.CurrentUICulture;
			try {
				Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
				{
					var data1 = new ToAdventurer3Parameter();
					data1.MotorA = 0;
					data1.MotorB = 0;
					data1.MotorX = 0;
					data1.MotorY = 0;
					data1.MotorZ = 0;
					data1.BrimSpeedRatio = 0;
					data1.BrimExtrudeRatio = 0;
					data1.PlayRemovalLength = -.005;
					data1.OffsetZ = -10.005;
					data1.BrimSpeed = 0;

					var ret = data1.IsValid(false);
					Assert.AreEqual(ret.Count, 10);
					Assert.AreEqual(ret["MotorA"], "Please enter a value in the range: 1 - 100.");
					Assert.AreEqual(ret["MotorB"], "Please enter a value in the range: 1 - 100.");
					Assert.AreEqual(ret["MotorX"], "Please enter a value in the range: 1 - 100.");
					Assert.AreEqual(ret["MotorY"], "Please enter a value in the range: 1 - 100.");
					Assert.AreEqual(ret["MotorZ"], "Please enter a value in the range: 1 - 100.");
					Assert.AreEqual(ret["BrimSpeedRatio"], "Please enter a value in the range: 1 - 999.");
					Assert.AreEqual(ret["BrimExtrudeRatio"], "Please enter a value in the range: 1 - 999.");
					Assert.AreEqual(ret["PlayRemovalLength"], "Please enter a value in the range: 0 - 10.");
					Assert.AreEqual(ret["OffsetZ"], "Please enter a value in the range: -10 - 10.");
					Assert.AreEqual(ret["BrimSpeed"], "Please enter a value in the range: 1 - 4800.");
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}
	}
}