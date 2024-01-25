using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing
{
    public class Reset : DrawCommand
    {

        public Reset(params string[] arguments) : base(arguments)
        {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager)
        {
            ValidateCommand(variableManager);
            drawer.Reset();
        }

        public override string ToString()
        {
            return $"RESET";
        }
    }
}
