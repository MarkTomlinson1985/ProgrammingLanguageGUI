using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class DrawTo : Command {
        private int xCoordinate;
        private int yCoordinate;

        public DrawTo(string command) : base(command) {
            numberOfArguments = 3;
        }

        public override void Execute(Drawer drawer) {
            drawer.DrawLine(xCoordinate, yCoordinate);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();

            try {
                xCoordinate = int.Parse(command.Split(" ")[1]);
                yCoordinate = int.Parse(command.Split(" ")[2]);

                if (xCoordinate < 0 || yCoordinate < 0) {
                    throw new CommandArgumentException("Provided coordinate arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided arguments are not valid numbers.");
            }
        }
    }
}
