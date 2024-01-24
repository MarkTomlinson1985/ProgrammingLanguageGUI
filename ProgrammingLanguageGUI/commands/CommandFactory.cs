using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;

namespace ProgrammingLanguageGUI.commands {
    public class CommandFactory {
        public static Command? BuildCommand(string command) {
            string commandType = command.Split(" ")[0].Trim();
            string[] arguments = command.Split(" ").Skip(1).Select(argument => argument.Trim()).ToArray();
            switch (commandType.ToLower()) {
                case "move":
                    return new Move(arguments);
                case "drawto":
                    return new DrawTo(arguments);
                case "circle":
                    return new Circle(arguments);
                case "clear":
                    return new Clear(arguments);
                case "reset":
                    return new Reset(arguments);
                case "rectangle":
                    return new Rectangle(arguments);
                case "triangle":
                    return new Triangle(arguments);
                case "pen":
                    return new Pen(arguments);
                case "fill":
                    return new Fill(arguments);
                case "var":
                    return new Var(arguments);
                case "while":
                    return new While(arguments);
                case "endloop":
                    return new EndLoop(arguments);
                case "if":
                    return new If(arguments);
                case "endif":
                    return new EndIf(arguments);
                case "method":
                    return new Method(arguments);
                case "endmethod":
                    return new EndMethod(arguments);
            }

            if (commandType.Contains('(') && commandType.Contains(')')) {
                string[] methodComponents = commandType.Split("(");

                if (")".Equals(methodComponents[1])) {
                    return new CallMethod(methodComponents[0]);
                }

                string[] methodName = [methodComponents[0]];
                string[] methodArguments = [.. methodName, .. methodComponents[1].Split(',').Select(component => component.Replace(")", "").Trim()).ToArray()];
                return new CallMethod(methodArguments);
            }

            return null;
        }
    }
}
