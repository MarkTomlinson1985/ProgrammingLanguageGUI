using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    internal interface IDrawCommand {
        void Execute(Drawer drawer, VariableManager variableManager);
    }
}
