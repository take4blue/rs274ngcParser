using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Take4.TestSupport {
	/// <summary>
	/// staticなメソッドが入っているサポートクラス
	/// </summary>
	public static partial class SupportClass {
		/// <summary>
		/// ファイルの一致チェック(バイナリ的な一致チェック)
		/// </summary>
		public static void AreEqual(string fileA, string fileB) {
			CollectionAssert.AreEqual(System.IO.File.ReadAllBytes(fileA), System.IO.File.ReadAllBytes(fileB));
		}

		/// <summary>
		/// ファイルの一致チェック(テキスト的な一致チェックのため、改行コードの違いは無視される)
		/// </summary>
		public static void AreEqualText(string fileA, string fileB) {
			CollectionAssert.AreEqual(System.IO.File.ReadLines(fileA).Cast<string>().ToArray(), System.IO.File.ReadLines(fileB).Cast<string>().ToArray());
		}
	}
}
