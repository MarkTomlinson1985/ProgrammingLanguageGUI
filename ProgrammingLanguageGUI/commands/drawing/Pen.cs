using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands.drawing {
    public class Pen : DrawCommand {
        private Color colour;
        private Color multiColour = Color.Empty;

        enum MultiColour {
            REDGREEN,
            BLUEYELLOW,
            BLACKWHITE
        }

        public Pen(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);

            if (multiColour.Equals(Color.Empty)) {
                drawer.ChangePenColour(colour);
            } else {
                drawer.ChangePenMultiColour(colour, multiColour);
            }

        }

        public override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            if (arguments[0].Contains(',')) {
                ValidateRGBParameters(variableManager);
                return;
            }

            ValidateColourFromName();
        }

        private void ValidateRGBParameters(VariableManager variableManager) {
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
        }

        private void ValidateColourFromName() {
            if (MatchesMultiColour(arguments[0])) {
                ValidateMultiColour();
                return;
            }

            colour = Color.FromName(arguments[0].Substring(0, 1).ToUpper() + arguments[0].Substring(1).ToLower());

            if (!colour.IsKnownColor && !colour.IsSystemColor) {
                throw new CommandArgumentException("Colour '" + colour.Name + "' is not a valid colour.");
            }
        }

        private bool MatchesMultiColour(string multiColour) {
            return Enum.GetNames<MultiColour>().Any(colour => colour.ToLower().Equals(multiColour));
        }

        private void ValidateMultiColour() {
            MultiColour multi = GetMultiColourType(arguments[0]);
            switch (multi) {
                case MultiColour.REDGREEN:
                    colour = Color.Red;
                    multiColour = Color.Green;
                    return;
                case MultiColour.BLUEYELLOW:
                    colour = Color.Blue;
                    multiColour = Color.Yellow;
                    return;
                case MultiColour.BLACKWHITE:
                    colour = Color.Black;
                    multiColour = Color.White;
                    return;
            }
        }

        private MultiColour GetMultiColourType(string multiColour) {
            switch (multiColour) {
                case "redgreen":
                    return MultiColour.REDGREEN;
                case "blueyellow":
                    return MultiColour.BLUEYELLOW;
                case "blackwhite":
                    return MultiColour.BLACKWHITE;
            }
            throw new CommandArgumentException($"MultiColour '{multiColour}' not supported.");
        }

        public override string ToString() {
            return $"PEN {arguments[0]}";
        }
    }
}
