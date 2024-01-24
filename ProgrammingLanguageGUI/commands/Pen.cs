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
