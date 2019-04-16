using Prism.Commands;

namespace Take4.TestSupport {
	class TestShellCommand : PrintControler.Controler.IApplicationCommands {
		private CompositeCommand closeCommand_ = new CompositeCommand();
		public CompositeCommand CloseCommand {
			get => closeCommand_;
		}

		private CompositeCommand disConnectCommand_ = new CompositeCommand();
		public CompositeCommand DisConnectCommand {
			get => disConnectCommand_;
		}
	}
}