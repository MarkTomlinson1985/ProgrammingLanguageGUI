using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Fill : Command {
        private bool fill;

        public Fill(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer) {
            drawer.SetFillMode(fill);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();
            
            if (arguments[0].ToLower().Equals("on")) {
                fill = true;
            } else if (arguments[0].ToLower().Equals("off")) {
                fill = false;
            } else {
                throw new CommandArgumentException("Provided argument invalid - expected on/off.");
            }
        }
    }
}
