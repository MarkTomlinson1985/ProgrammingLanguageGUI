using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing
{
    public class Clear : DrawCommand
    {

        public Clear(params string[] arguments) : base(arguments)
        {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager)
        {
            ValidateCommand(variableManager);
            drawer.Clear();
        }

        public override string ToString()
        {
            return "CLEAR";
        }
    }
}
