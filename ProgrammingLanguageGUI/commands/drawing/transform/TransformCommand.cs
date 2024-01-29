namespace ProgrammingLanguageGUI.commands.drawing {
    /// Derived abstract command class. Class from which commands that relate to
    /// transform operations can derive from.
    /// </summary>
    public abstract class TransformCommand : Command {
        public TransformCommand(params string[] arguments) : base(arguments) { }
    }
}
