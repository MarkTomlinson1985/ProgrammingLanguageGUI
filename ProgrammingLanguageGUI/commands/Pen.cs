using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.commands {
    public class Pen : Command {
        private Color colour;

        public Pen(params string[] arguments) : base(arguments) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer) {
            drawer.ChangePenColour(colour);
        }

        public override void ValidateCommand() {
            base.ValidateCommand();
            colour = Color.FromName(arguments[0].Substring(0, 1).ToUpper() + arguments[0].Substring(1).ToLower());

            if (!colour.IsKnownColor && !colour.IsSystemColor) {
                throw new CommandArgumentException("Colour '" + colour.Name + "' is not a valid colour.");
            }
        }
    }
}
