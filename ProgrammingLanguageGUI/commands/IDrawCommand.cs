using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    internal interface IDrawCommand {
        void Execute(Drawer drawer);
    }
}
