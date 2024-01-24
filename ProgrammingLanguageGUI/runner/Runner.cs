using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.runner
{
    public class Runner {
        private CommandProcessor processor;
        private Drawer drawer;
        private VariableManager variableManager;

        public Runner(CommandProcessor processor, Drawer drawer, VariableManager variableManager) {
            this.processor = processor;
            this.drawer = drawer;
            this.variableManager = variableManager;
        }

        public string RunCommand(string input) {
            try {
                Command command = processor.ParseCommand(input);

                if (command is DrawCommand drawCommand) {
                    drawCommand.Execute(drawer, variableManager);
                } else if (command is FunctionCommand functionCommand) {
                    functionCommand.Execute(variableManager);
                }

                return "Command run successfully";
            } catch (Exception ex) {
                return ex.Message;
            }
        }

        public string RunProgram(string input) {
            ProgramResults results = processor.ParseProgram(input);
            List<CommandException> exceptions = results.GetExceptions();
            List<Command> commands = results.GetCommands().Keys.ToList();

            for (int i = 0; i < commands.Count; i++) {
                try {
                    if (commands[i] is DrawCommand drawCommand) {
                        drawCommand.Execute(drawer, variableManager);
                    } else if (commands[i] is FunctionCommand functionCommand) {
                        functionCommand.Execute(variableManager);
                        if (functionCommand is ILoop) {
                            i = HandleLoop(i, commands);
                            continue;
                        }
                    }
                } catch (CommandException ex) {
                    exceptions.Add(new CommandException($"Line {results.GetCommands()[commands[i]]}: {ex.Message}"));
                }
            }

            if (exceptions.Count > 0) {
                string exceptionOutput = string.Empty;
                foreach (CommandException ex in exceptions) {
                    exceptionOutput = exceptionOutput + ex.Message + "\n";
                }
                return exceptionOutput;
            }

            return "Program executed successfully.";
        }

        private int HandleLoop(int loopIndex, List<Command> commands) {
            int endLoopIndex = commands.IndexOf(commands.Skip(loopIndex).FirstOrDefault(command => command is EndLoop, new EndLoop()));

            if (endLoopIndex == -1) {
                throw new CommandNotFoundException("Loop command has no defined end.");
            }

            string loopedProgram = string.Join("\n", commands.Skip(loopIndex + 1).Take(endLoopIndex - loopIndex - 1).Select(command => command.ToString()).ToArray());

            while (((ILoop) commands[loopIndex]).Evaluate()) {
                RunProgram(loopedProgram);
                ((FunctionCommand) commands[loopIndex]).Execute(variableManager);
            }

            return endLoopIndex;
        }
    }
}
