using ProgrammingLanguageGUI.drawer;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class DrawTo : Command {
        private int xCoordinate;
        private int yCoordinate;

        public DrawTo(string command, Drawer drawer) : base(command, drawer) {
            numberOfArguments = 3;
        }

        public override void Execute() {
            drawer.DrawLine(xCoordinate, yCoordinate);
        }

        public override void ValidateCommand() {
            Debug.WriteLine("Processing draw argument");
            base.ValidateCommand();

            // Add specific validation for 'Move' command here.
            // check other arguments are numbers.
            try {
                xCoordinate = int.Parse(command.Split(" ")[1]);
                yCoordinate = int.Parse(command.Split(" ")[2]);

                if (xCoordinate < 0 || yCoordinate < 0) {
                    throw new ArgumentException("Provided coordinate arguments must not be negative.");
                }

            } catch (FormatException) {
                throw new ArgumentException("Provided arguments are not valid numbers.");
            }
        }
    }
}
