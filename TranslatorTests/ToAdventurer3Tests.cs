using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Json;
using Take4.TestSupport;

namespace Take4.Translator.Tests {
	[TestClass()]
	public class ToAdventurer3Tests {
		[TestMethod()]
		public void parameterTest1() {
			var data1 = new ToAdventurer3();
			var data = data1.Parameter;
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
		public void parameterTest2() {
			var data = new ToAdventurer3();
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
			data.Parameter = data2;

			var data1 = data.Parameter;
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

		/// <summary>
		/// gxファイルのヘッダーが削除されることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test1.gx")]
		[DeploymentItem(@"ToAdventurer3TestData\result1.gx")]
		public void update1FlashPrint() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test1.gx")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output1.gx")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqualText(@"ToAdventurer3TestData\result1.gx", @"ToAdventurer3TestData\output1.gx");
		}

		/// <summary>
		/// 遊び除去コマンドの変更がされていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test1.gx")]
		[DeploymentItem(@"ToAdventurer3TestData\result2.gx")]
		public void update2FlashPrint() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.5;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test1.gx")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output2.gx")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqualText(@"ToAdventurer3TestData\output2.gx", @"ToAdventurer3TestData\result2.gx");
		}

		/// <summary>
		/// Z軸オフセットがされていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test1.gx")]
		[DeploymentItem(@"ToAdventurer3TestData\result3.gx")]
		public void update3FlashPrint() {
			var parameter = new ToAdventurer3Parameter();
			parameter.OffsetZ = -0.05;
			parameter.PlayRemovalLength = 0.0;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test1.gx")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output3.gx")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqualText(@"ToAdventurer3TestData\output3.gx", @"ToAdventurer3TestData\result3.gx");
		}

		/// <summary>
		/// 開始・終了コードとコメントの除去がされていることを確認するテスト
		/// Simplify3D側のファイルは、基本、この操作がされてしまう。
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result1.g")]
		public void update1S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output1.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output1.g", @"ToAdventurer3TestData\result1.g");
		}

		/// <summary>
		/// 開始・終了コードに筐体ファン動作コードが追加されていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result2.g")]
		public void update2S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = true;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output2.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output2.g", @"ToAdventurer3TestData\result2.g");
		}

		/// <summary>
		/// 開始コードのモーター駆動倍率にユーザー設定の数値が格納されていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result3.g")]
		public void update3S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			parameter.MotorX = 1;
			parameter.MotorY = 2;
			parameter.MotorZ = 3;
			parameter.MotorA = 4;
			parameter.MotorB = 5;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output3.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output3.g", @"ToAdventurer3TestData\result3.g");
		}

		/// <summary>
		/// 遊び除去コマンドが出力されていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result4.g")]
		public void update4S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 5.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output4.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output4.g", @"ToAdventurer3TestData\result4.g");
		}

		/// <summary>
		/// Z軸オフセットがされていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result5.g")]
		public void update5S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.OffsetZ = -0.05;
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output5.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output5.g", @"ToAdventurer3TestData\result5.g");
		}

		/// <summary>
		/// Z軸オフセット・遊び除去コマンドがされていることを確認するテスト
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result6.g")]
		public void update6S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.OffsetZ = -0.05;
			parameter.PlayRemovalLength = 0.5;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output6.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output6.g", @"ToAdventurer3TestData\result6.g");
		}

		/// <summary>
		/// ブリムスピードの設定確認：絶対速度にする
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result7.g")]
		public void update7S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.Absolute;
			parameter.BrimSpeed = 200;
			parameter.BrimSpeedRatio = 50;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output7.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output7.g", @"ToAdventurer3TestData\result7.g");
		}

		/// <summary>
		/// ブリムスピードの設定確認：相対速度にする
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result8.g")]
		public void update8S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.Ratio;
			parameter.BrimSpeed = 200;
			parameter.BrimSpeedRatio = 50;
			parameter.BrimExtrudeRatio = 100;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output8.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output8.g", @"ToAdventurer3TestData\result8.g");
		}

		/// <summary>
		/// ブリム吐出量の変更
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result9.g")]
		public void update9S3D() {
			var parameter = new ToAdventurer3Parameter();
			parameter.PlayRemovalLength = 0.0;
			parameter.BrimSpeedTypeValue = ToAdventurer3Parameter.BrimSpeedType.NoChange;
			parameter.BrimExtrudeRatio = 200;
			parameter.EnclosureFanOn = false;
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output9.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output9.g", @"ToAdventurer3TestData\result9.g");
		}

		/// <summary>
		/// preが複数出てきてしまった例:不具合検証用
		/// </summary>
		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\test3.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result10.g")]
		[DeploymentItem(@"ToAdventurer3TestData\test1.json")]
		public void update10S3D() {
			var wStream = File.OpenRead(@"ToAdventurer3TestData\test1.json");
			var settings = new DataContractJsonSerializerSettings();
			var serializer = new DataContractJsonSerializer(typeof(ToAdventurer3Parameter), settings);
			var parameter = (ToAdventurer3Parameter)serializer.ReadObject(wStream);
			using (var input = File.OpenRead(@"ToAdventurer3TestData\test3.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output10.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output10.g", @"ToAdventurer3TestData\result10.g");
		}

		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\ret-1.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result11.g")]
		[DeploymentItem(@"ToAdventurer3TestData\test1.json")]
		public void update11S3D() {
			var wStream = File.OpenRead(@"ToAdventurer3TestData\test1.json");
			var settings = new DataContractJsonSerializerSettings();
			var serializer = new DataContractJsonSerializer(typeof(ToAdventurer3Parameter), settings);
			var parameter = (ToAdventurer3Parameter)serializer.ReadObject(wStream);
			using (var input = File.OpenRead(@"ToAdventurer3TestData\ret-1.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output11.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output11.g", @"ToAdventurer3TestData\result11.g");
		}

		[TestMethod()]
		[DeploymentItem(@"ToAdventurer3TestData\ret-2.g")]
		[DeploymentItem(@"ToAdventurer3TestData\result12.g")]
		[DeploymentItem(@"ToAdventurer3TestData\test1.json")]
		public void update12S3D() {
			var wStream = File.OpenRead(@"ToAdventurer3TestData\test1.json");
			var settings = new DataContractJsonSerializerSettings();
			var serializer = new DataContractJsonSerializer(typeof(ToAdventurer3Parameter), settings);
			var parameter = (ToAdventurer3Parameter)serializer.ReadObject(wStream);
			using (var input = File.OpenRead(@"ToAdventurer3TestData\ret-2.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\output12.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\output12.g", @"ToAdventurer3TestData\result12.g");
		}

		[TestMethod]
		[DeploymentItem(@"ToAdventurer3TestData\slic3r1.g")]
		[DeploymentItem(@"ToAdventurer3TestData\test1.json")]
		[DeploymentItem(@"ToAdventurer3TestData\resslic3r1.g")]
		public void updateSlic3r1() {
			var wStream = File.OpenRead(@"ToAdventurer3TestData\test1.json");
			var settings = new DataContractJsonSerializerSettings();
			var serializer = new DataContractJsonSerializer(typeof(ToAdventurer3Parameter), settings);
			var parameter = (ToAdventurer3Parameter)serializer.ReadObject(wStream);
			using (var input = File.OpenRead(@"ToAdventurer3TestData\slic3r1.g")) {
				using (var output = File.Create(@"ToAdventurer3TestData\outslic3r1.g")) {
					var process = new ToAdventurer3();
					process.Parameter = parameter;
					Assert.AreEqual(process.Modify(input, output), true);
				}
			}
			SupportClass.AreEqual(@"ToAdventurer3TestData\outslic3r1.g", @"ToAdventurer3TestData\resslic3r1.g");
		}
	}
}