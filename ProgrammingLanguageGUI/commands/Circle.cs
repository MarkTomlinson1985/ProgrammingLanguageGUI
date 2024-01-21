using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Circle : Command {
        private int radius;

        public Circle(string command, Drawer drawer) : base(command, drawer) {
            numberOfArguments = 2;
        }

        public override void Execute() {
            drawer.DrawCircle(radius);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();

            // Add specific validation for 'Circle' command here.
            // check other arguments are numbers.
            try {
                radius = int.Parse(command.Split(" ")[1]);

                if (radius < 0) {
                    throw new CommandArgumentException("Provided radius must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided radius is not a valid number.");
            }
        }
    }
}
