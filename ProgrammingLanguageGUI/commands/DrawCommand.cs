using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public abstract class DrawCommand : Command, IDrawCommand {
        public DrawCommand(params string[] arguments) : base(arguments) {}
        public abstract void Execute(Drawer drawer);
    }
}
