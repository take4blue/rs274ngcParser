using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.ComponentModel;

namespace Take4.TestSupport {
	/// <summary>
	/// staticなメソッドが入っているサポートクラス
	/// </summary>
	public static partial class SupportClass {
		/// <summary>
		/// プロパティのset/getを範囲内の値で設定して、エラーとならないことを確認する
		/// </summary>
		/// <param name="obj">チェック対象のオブジェクト</param>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="start">初期値</param>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		public static void CheckValid<T1, T2>(T1 obj, string propertyName, T2 start, T2 min, T2 max) where T1 : INotifyDataErrorInfo, INotifyPropertyChanged {
			PropertyInfo property = obj.GetType().GetProperty(propertyName);
			if (property == null || (!property.CanRead && !property.CanWrite)) {
				Assert.Fail();
			}
			var target = (T2)(property.GetValue(obj));
			property.SetValue(obj, start);
			var prop = new Microsoft.Practices.Prism.TestSupport.PropertyChangeTracker(obj);
			property.SetValue(obj, min);
			Assert.AreEqual((T2)(property.GetValue(obj)), min);
			Assert.AreEqual(obj.HasErrors, false);
			Assert.AreEqual(prop.ChangedProperties.Length, 1);
			Assert.AreEqual(prop.ChangedProperties[0], propertyName);
			property.SetValue(obj, max);
			Assert.AreEqual((T2)(property.GetValue(obj)), max);
			Assert.AreEqual(obj.HasErrors, false);
		}

		/// <summary>
		/// プロパティのset/getを範囲外の値で設定して、エラーとなることを確認する
		/// </summary>
		/// <param name="obj">チェック対象のオブジェクト</param>
		/// <param name="propertyName">プロパティ名</param>
		/// <param name="start">初期値</param>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		public static void CheckInvalid<T1>(T1 obj, string propertyName, int start, int min, int max) where T1 : INotifyDataErrorInfo, INotifyPropertyChanged {
			PropertyInfo property = obj.GetType().GetProperty(propertyName);
			if (property == null || (!property.CanRead && !property.CanWrite)) {
				Assert.Fail();
			}
			var original = Thread.CurrentThread.CurrentUICulture;

			try {
				string[] lang = { "en", "ja-JP" };
				for (int i = 0; i < 2; i++) {
					Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang[i]);
					var target = (int)(property.GetValue(obj));
					property.SetValue(obj, start);
					var prop = new Microsoft.Practices.Prism.TestSupport.PropertyChangeTracker(obj);
					property.SetValue(obj, min - 1);
					Assert.AreEqual((int)(property.GetValue(obj)), min - 1);
					errorcheck(obj, propertyName, min, max, true);
					Assert.AreEqual(prop.ChangedProperties.Length, 2);
					Assert.AreEqual(prop.ChangedProperties[0], propertyName);
					Assert.AreEqual(prop.ChangedProperties[1], propertyName);
					property.SetValue(obj, max + 1);
					Assert.AreEqual((int)(property.GetValue(obj)), max + 1);
					errorcheck(obj, propertyName, min, max, true);
				}
			}
			finally {
				Thread.CurrentThread.CurrentUICulture = original;
			}
		}

		/// <summary>
		/// データの範囲チェックでのメッセージ確認
		/// </summary>
		/// <typeparam name="T1">チェック対象とするクラスでINotifyDataErrorInfoを実装している</typeparam>
		/// <typeparam name="T2">チェック対象とするデータの型</typeparam>
		/// <param name="test">チェックするオブジェクト</param>
		/// <param name="name">プロパティの名前(この名前でアクセスする)</param>
		/// <param name="min">最小値</param>
		/// <param name="max">最大値</param>
		/// <param name="hasError">エラーがあったかどうか</param>
		public static void errorcheck<T1, T2>(T1 test, string name, T2 min, T2 max, bool hasError) where T1 : System.ComponentModel.INotifyDataErrorInfo {
			var str = test.GetErrors(name).Cast<string>().ToArray();
			Assert.AreEqual(str.Length, hasError ? 1 : 0);
			if (hasError) {
				switch (Thread.CurrentThread.CurrentUICulture.Name) {
				case "en":
					Assert.AreEqual(str[0], string.Format("Please enter a value in the range: {1} - {2}.", 0, min, max));
					break;
				case "ja-JP":
					Assert.AreEqual(str[0], string.Format("この値は{1}から{2}の範囲で入力してください。", 0, min, max));
					break;
				}
			}
		}

		public static void noerrorcheck<T1>(T1 test, string name) where T1 : System.ComponentModel.INotifyDataErrorInfo {
			var str = test.GetErrors(name).Cast<string>().ToArray();
			Assert.AreEqual(str.Length, 0);
		}
	}
}
