using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Move : Command {
        private int xCoordinate;
        private int yCoordinate;

        public Move(string command, Drawer drawer) : base(command, drawer) {
            numberOfArguments = 3;
        }

        public override void Execute() {
            drawer.MoveTo(xCoordinate, yCoordinate);
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

        public override bool Equals(object? obj) {
            if (obj is Move) {
                Move m = (Move)obj;
                return m.yCoordinate == yCoordinate && m.xCoordinate == xCoordinate;
            }
            return false;
        }
    }
}
