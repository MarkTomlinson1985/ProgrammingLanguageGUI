using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing {
    /// <summary>
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Reset : DrawCommand {
        public Reset(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.Reset();
        }

        public override string ToString() {
            return $"RESET";
        }
    }
}
