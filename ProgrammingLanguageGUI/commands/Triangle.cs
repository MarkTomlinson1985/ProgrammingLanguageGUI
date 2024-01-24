using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class Triangle : DrawCommand {
        private int width;
        private int height;

        public Triangle(params string[] arguments) : base(arguments) {
            numberOfArguments = 2;
        }

        public override void Execute(Drawer drawer) {
            drawer.DrawTriangle(width, height);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();

            try {
                width = int.Parse(arguments[0]);
                height = int.Parse(arguments[1]);

                if (width < 0 || height < 0) {
                    throw new CommandArgumentException("Provided size arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided arguments are not valid numbers.");
            }
        }
    }
}
