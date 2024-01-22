using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Circle : Command {
        private int radius;

        public Circle(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer) {
            drawer.DrawCircle(radius);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();

            try {
                radius = int.Parse(arguments[0]);

                if (radius < 0) {
                    throw new CommandArgumentException("Provided radius must not be negative.");
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided radius is not a valid number.");
            }
        }

        public override bool Equals(object? obj) {
            if (obj is Circle) {
                Circle c = (Circle)obj;
                return c.radius == radius;
            }
            return false;
        }
    }
}
