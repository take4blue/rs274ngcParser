using Microsoft.VisualStudio.TestTools.UnitTesting;
using Take4.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take4.Translator.Tests {
	[TestClass()]
	public class Simplify3DParameterTests {
		[TestMethod()]
		public void Construct() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter1() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(null), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter2() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(""), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter3() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter("; hoge"), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter5() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   defaultSpeed,"), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter6() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   rapidXYspeed,"), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter7() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   rapidZspeed,"), false);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter8() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   defaultSpeed,10"), true);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 10);
		}

		[TestMethod]
		public void ParseParameter9() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   rapidXYspeed,20"), true);
			Assert.AreEqual(target.RapidXYSpeed, 20);
			Assert.AreEqual(target.RapidZSpeed, 300);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}

		[TestMethod]
		public void ParseParameter10() {
			var target = new Simplify3DParameter();
			Assert.AreEqual(target.ParseParameter(";   rapidZspeed,30"), true);
			Assert.AreEqual(target.RapidXYSpeed, 4800);
			Assert.AreEqual(target.RapidZSpeed, 30);
			Assert.AreEqual(target.DefaultSpeed, 3600);
		}
	}
}