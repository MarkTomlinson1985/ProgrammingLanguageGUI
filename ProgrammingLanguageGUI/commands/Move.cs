using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Move : Command {
        private int xCoordinate;
        private int yCoordinate;

        public Move(string command) : base(command) {
            numberOfArguments = 3;
        }

        public override void Execute() {
            Debug.WriteLine("Executing move command");
        }

        public override void ValidateCommand() {
            Debug.WriteLine("Processing move argument");
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
