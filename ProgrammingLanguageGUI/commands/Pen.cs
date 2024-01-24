using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Pen : DrawCommand {
        private Color colour;

        public Pen(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            drawer.ChangePenColour(colour);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            if (arguments[0].Contains(',')) {
                try {
                    int[] rgbValues = arguments[0].Split(',').Select(value => int.Parse(GetVariableOrValue(value, variableManager))).ToArray();
                    
                    foreach (int rgbValue in rgbValues) {
                        if (rgbValue < 0 || rgbValue > 255) {
                            throw new CommandArgumentException("Provided rgb values must be between 0 and 255.");
                        }
                    }
                    
                    colour = Color.FromArgb(rgbValues[0], rgbValues[1], rgbValues[2]);

                } catch (FormatException) {
                    throw new CommandArgumentException("Provided rgb values are not valid numbers.");
                }
                return;
            }

            colour = Color.FromName(arguments[0].Substring(0, 1).ToUpper() + arguments[0].Substring(1).ToLower());

            if (!colour.IsKnownColor && !colour.IsSystemColor) {
                throw new CommandArgumentException("Colour '" + colour.Name + "' is not a valid colour.");
            }
        }

        public override string ToString() {
            return $"PEN {arguments[0]}";
        }
    }
}
