namespace ProgrammingLanguageGUI.exception {
    /// <summary>
    /// Derived command exception class relating to unimplemented commands.
    /// </summary>
    public class CommandNotFoundException : CommandException {
        public CommandNotFoundException(string message) : base(message) { }
    }
}
