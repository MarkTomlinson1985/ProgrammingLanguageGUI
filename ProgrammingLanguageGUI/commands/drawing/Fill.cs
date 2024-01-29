using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.drawing {
    /// <summary>
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Fill : DrawCommand {
        private bool fill;

        public Fill(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.SetFillMode(fill);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            if (arguments[0].ToLower().Equals("on")) {
                fill = true;
            } else if (arguments[0].ToLower().Equals("off")) {
                fill = false;
            } else {
                throw new CommandArgumentException("Provided argument invalid - expected on/off.");
            }
        }

        public override string ToString() {
            return $"FILL {arguments[0]}";
        }
    }
}
