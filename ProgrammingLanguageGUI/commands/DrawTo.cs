using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class DrawTo : Command {
        private int xCoordinate;
        private int yCoordinate;

        public DrawTo(params string[] arguments) : base(arguments) {
            numberOfArguments = 2;
        }

        public override void Execute(Drawer drawer) {
            drawer.DrawLine(xCoordinate, yCoordinate);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();

            try {
                xCoordinate = int.Parse(arguments[0]);
                yCoordinate = int.Parse(arguments[1]);

                if (xCoordinate < 0 || yCoordinate < 0) {
                    throw new CommandArgumentException("Provided coordinate arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided arguments are not valid numbers.");
            }
        }
    }
}
