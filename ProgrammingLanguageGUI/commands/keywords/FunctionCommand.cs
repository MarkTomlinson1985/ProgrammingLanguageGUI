using ProgrammingLanguageGUI.commands.keywords;

namespace ProgrammingLanguageGUI.commands {
    public abstract class FunctionCommand : Command, IFunctionCommand {
        public FunctionCommand(params string[] arguments) : base(arguments) {}
        public abstract void Execute(VariableManager variableManager);
    }
}
