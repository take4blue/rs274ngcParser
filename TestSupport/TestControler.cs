using System.ComponentModel;
using System.Reflection;

namespace Take4.TestSupport {
	internal class Controler : PrintControler.Controler.IAdventurer3Controler {
		public bool IsConnected { get; set; }

		public bool LimitX { get; set; }

		public bool LimitY { get; set; }

		public bool LimitZ { get; set; }

		public PrintControler.Controler.MachineStatus Status { get; set; }

		public int CurrentTempBed { get; set; }

		public int CurrentTempNozel { get; set; }

		public int TargetTempBed { get; set; }
		public int TargetTempNozel { get; set; }

		public int SdProgress { get; set; }

		public int SdMax { get; set; }

		public double PosX { get; set; }

		public double PosY { get; set; }

		public double PosZ { get; set; }

		public double PosE { get; set; }

		public string BaseFolderName { get; set; }

		public bool CanJobStart { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public string cmdExec_ = "";

		public void EmergencyStop() {
			cmdExec_ = MethodBase.GetCurrentMethod().Name;
		}

		public bool isok_ = true;
		public bool IsOK(string retVal) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + retVal;
			return isok_;
		}

		public void Led(bool value) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + value.ToString();
		}

		public void MoveE(double e, uint f) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + e.ToString() + " " + f.ToString();
		}

		public void MoveX(double x, uint f) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + x.ToString() + " " + f.ToString();
		}

		public void MoveXY(double x, double y, uint f) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + x.ToString() + " " + y.ToString() + " " + f.ToString();
		}

		public void MoveY(double y, uint f) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + y.ToString() + " " + f.ToString();
		}

		public void MoveZ(double z, uint f) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + z.ToString() + " " + f.ToString();
		}

		public string sendRet_ = "";
		public string Send(string msg) {
			cmdExec_ = MethodBase.GetCurrentMethod().Name + " " + msg;
			return sendRet_;
		}

		public void StopJob() {
			cmdExec_ = MethodBase.GetCurrentMethod().Name;
		}

		public void rise(string value) {
			PropertyChanged(this, new PropertyChangedEventArgs(value));
		}
	}
}
