using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    internal interface ICommand
    {
        void Execute(Drawer drawer);
    }
}
