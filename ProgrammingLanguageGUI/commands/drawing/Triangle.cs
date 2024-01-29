using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.drawing {
    /// <summary>
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Triangle : DrawCommand {
        private int width;
        private int height;

        public Triangle(params string[] arguments) : base(arguments) {
            numberOfArguments = 2;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.DrawTriangle(width, height);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            try {
                width = int.Parse(GetVariableOrValue(arguments[0], variableManager));
                height = int.Parse(GetVariableOrValue(arguments[1], variableManager));

                if (width < 0 || height < 0) {
                    throw new CommandArgumentException("Provided size arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided arguments are not valid numbers.");
            }
        }

        public override string ToString() {
            return $"TRIANGLE {arguments[0]} {arguments[1]}";
        }
    }
}
