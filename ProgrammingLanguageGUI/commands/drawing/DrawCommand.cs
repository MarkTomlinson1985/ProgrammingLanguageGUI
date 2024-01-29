namespace ProgrammingLanguageGUI.commands.drawing {
    /// <summary>
    /// Abstract derived command class. Used primarily to identity commands that produce a drawn output.
    /// </summary>
    public abstract class DrawCommand : Command {
        public DrawCommand(params string[] arguments) : base(arguments) { }
    }
}
