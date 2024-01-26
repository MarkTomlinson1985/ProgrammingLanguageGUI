using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.drawing {
    public class Polygon : DrawCommand {
        private Point[] points;

        public Polygon(params string[] arguments) : base(arguments) {}

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.DrawPolygon(points);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            try {

                if (arguments.Length % 2 != 0) {
                    throw new CommandArgumentException("Invalid number of arguments: odd number of coordinates.");
                }

                points = new Point[arguments.Length / 2];

                int pointIndex = 0;
                for (int i = 0; i < arguments.Length; i += 2) {
                    int pointOne = int.Parse(arguments[i]);
                    int pointTwo = int.Parse(arguments[i + 1]);

                    if (pointOne < 0 || pointTwo < 0) {
                        throw new CommandArgumentException("Provided coordinates must not be negative.");
                    }

                    points[pointIndex] = new Point(pointOne, pointTwo);
                    pointIndex++;
                }

            } catch (FormatException) {
                throw new CommandArgumentException("Provided coordinates are not valid numbers.");
            }
        }

        public override string ToString() {
            return $"POLYGON {string.Join(" ", arguments)}";
        }
    }
}
