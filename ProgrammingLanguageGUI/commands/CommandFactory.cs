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
            switch (commandType.ToLower()) {
                case "move":
                    return new Move(command);
                case "drawto":
                    return new DrawTo(command);
                case "circle":
                    return new Circle(command);
                case "clear":
                    return new Clear(command);
            }
            return null;
        }
    }
}
