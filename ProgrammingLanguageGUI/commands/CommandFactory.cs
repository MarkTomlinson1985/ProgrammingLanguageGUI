using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;
using Pen = ProgrammingLanguageGUI.commands.drawing.Pen;
using Rectangle = ProgrammingLanguageGUI.commands.drawing.Rectangle;

namespace ProgrammingLanguageGUI.commands {
    /// <summary>
    /// Factory class for the construction of Command objects.
    /// </summary>
    public class CommandFactory {
        /// <summary>
        /// Constructs a Command object based on the provided command text.
        /// If command text does not match any defined commands, returns null.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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
                case "polygon":
                    return new Polygon(arguments);
                case "rotate":
                    return new Rotate(arguments);
                case "wave":
                    return new Wave(arguments);
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
