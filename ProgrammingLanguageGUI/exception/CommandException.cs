namespace ProgrammingLanguageGUI.exception {
    /// <summary>
    /// Base exception class for command related exceptions.
    /// </summary>
    public class CommandException : SystemException {
        public CommandException(string message) : base(message) { }
    }
}
