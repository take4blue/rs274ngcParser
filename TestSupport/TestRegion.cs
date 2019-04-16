using System;
using System.Reflection;
using Prism.Regions;

namespace Take4.TestSupport {
	internal class RegionManager : IRegionManager {
		public IRegionCollection Regions => throw new NotImplementedException();

		public string cmdExec_;

		public IRegionManager AddToRegion(string regionName, object view) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + view.ToString();
			return this;
		}

		public IRegionManager CreateRegionManager() {
			cmdExec_ = MethodBase.GetCurrentMethod().Name;
			return this;
		}

		public IRegionManager RegisterViewWithRegion(string regionName, Type viewType) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + viewType.ToString();
			return this;
		}

		public IRegionManager RegisterViewWithRegion(string regionName, Func<object> getContentDelegate) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + getContentDelegate.ToString();
			return this;
		}

		public void RequestNavigate(string regionName, Uri source, Action<NavigationResult> navigationCallback) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + navigationCallback.ToString();
		}

		public void RequestNavigate(string regionName, Uri source) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + source.ToString();
		}

		public void RequestNavigate(string regionName, string source, Action<NavigationResult> navigationCallback) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + source.ToString() + " " + navigationCallback.ToString();
		}

		public void RequestNavigate(string regionName, string source) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + source.ToString();
		}

		public void RequestNavigate(string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + target.ToString() + " " + navigationCallback.ToString() + " " + navigationParameters.ToString();
		}

		public void RequestNavigate(string regionName, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + target.ToString() + " " + navigationCallback.ToString() + " " + navigationParameters.ToString();
		}

		public void RequestNavigate(string regionName, Uri target, NavigationParameters navigationParameters) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + target.ToString() + " " + " " + navigationParameters.ToString();
		}

		public void RequestNavigate(string regionName, string target, NavigationParameters navigationParameters) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + regionName + " " + target.ToString() + " " + navigationParameters.ToString();
		}
	}
}
