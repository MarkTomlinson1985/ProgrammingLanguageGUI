namespace ProgrammingLanguageGUI.commands.drawing {
    public abstract class DrawCommand : Command {
        public DrawCommand(params string[] arguments) : base(arguments) { }
    }
}
