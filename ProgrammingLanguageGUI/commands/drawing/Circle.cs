using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.drawing {
    public class Circle : DrawCommand {
        private int radius;

        public Circle(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);

            drawer.DrawCircle(radius);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            try {
                radius = int.Parse(GetVariableOrValue(arguments[0], variableManager));

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

        public override string ToString() {
            return $"CIRCLE {arguments[0]}";
        }
    }
}
