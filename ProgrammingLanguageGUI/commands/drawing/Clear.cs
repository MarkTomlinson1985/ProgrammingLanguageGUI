using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing {
    /// <summary>
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Clear : DrawCommand {

        public Clear(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        /// <summary>
        /// Executes by validating the command then sending it to the drawer to render on screen.
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="variableManager"></param>
        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.Clear();
        }

        public override string ToString() {
            return "CLEAR";
        }
    }
}
