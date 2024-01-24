using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class DrawTo : DrawCommand {
        private int xCoordinate;
        private int yCoordinate;

        public DrawTo(params string[] arguments) : base(arguments) {
            numberOfArguments = 2;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.DrawLine(xCoordinate, yCoordinate);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            try {
                xCoordinate = int.Parse(GetVariableOrValue(arguments[0], variableManager));
                yCoordinate = int.Parse(GetVariableOrValue(arguments[1], variableManager));

                if (xCoordinate < 0 || yCoordinate < 0) {
                    throw new CommandArgumentException("Provided coordinate arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided arguments are not valid numbers.");
            }
        }

        public override string ToString() {
            return $"DRAWTO {arguments[0]} {arguments[1]}";
        }
    }
}
