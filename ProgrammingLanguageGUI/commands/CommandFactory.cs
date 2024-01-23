using ProgrammingLanguageGUI.drawer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI.commands {
    public class CommandFactory {
        public static Command? BuildCommand(string command) {
            string commandType = command.Split(" ")[0];
            string[] arguments = command.Split(" ").Skip(1).ToArray();
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
            }
            return null;
        }
    }
}
