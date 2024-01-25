using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.runner {
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

                command.Execute(drawer, variableManager);

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
                    commands[i].Execute(drawer, variableManager);

                    if (commands[i] is ISelection) {
                        // Do not perform loop commands in realtime syntax checking.
                        if (commands[i] is While && !drawer.DisableDrawer) {
                            i = HandleLoop(i, commands);
                            continue;
                        }
                        if (commands[i] is If ifCommand) {
                            if (ifCommand.Evaluate()) {
                                if (ifCommand.HasInlineCommand()) {
                                    Command inlineCommand = ifCommand.Retrieve();
                                    inlineCommand.Execute(drawer, variableManager);
                                    continue;
                                }
                            }
                            i = HandleIfBlock(i, commands);
                        }
                    }

                    if (commands[i] is Method method) {
                        method.StartLineNumber = results.GetCommands().GetValueOrDefault(method) + 1;
                        int endMethodIndex = commands.IndexOf(commands.Skip(i).FirstOrDefault(command => command is EndMethod, new EndMethod()));

                        if (endMethodIndex == -1) {
                            method.EndLineNumber = -1;
                            throw new CommandNotFoundException("Method command has no defined end.");
                        }

                        method.EndLineNumber = results.GetCommands().GetValueOrDefault(commands[endMethodIndex]) - 1;
                        i = method.EndLineNumber;
                        continue;
                    }

                    if (commands[i] is CallMethod callMethod) {
                        if (callMethod.GetMethodEnd() == -1) {
                            callMethod.UnassignVariables(variableManager);
                            throw new CommandNotFoundException($"Improperly declared method: '{callMethod.MethodName}'.");
                        }

                        for (int j = callMethod.GetMethodStart(); j <= callMethod.GetMethodEnd(); j++) {
                            Command command = results.GetCommands().First(entry => entry.Value == j).Key;
                            command.Execute(drawer, variableManager);
                        }
                        // Descope variables declared in method. Are these being descoped incorrectly? Seems to have issues with while loops..
                        callMethod.UnassignVariables(variableManager);
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

        public SyntaxResults CheckProgramSyntax(string program) {
            try {
                drawer.DisableDrawer = true;
                string programOutput = RunProgram(program);
                if (!"Program executed successfully.".Equals(programOutput)) {
                    string[] exceptions = programOutput.Split('\n')
                        .Where(line => !line.Trim().Equals(string.Empty))
                        .OrderBy(line => int.Parse(line.Split(":")[0].Split(" ")[1]))
                        .ToArray();

                    int[] lineNumbers = exceptions.Select(exception => int.Parse(exception.Split(":")[0].Replace("Line ", ""))).ToArray();
                    drawer.DisableDrawer = false;

                    return SyntaxResults.Builder()
                        .LineNumbers(lineNumbers)
                        .SyntaxErrors(exceptions)
                        .Build();
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            drawer.DisableDrawer = false;
            return SyntaxResults.Builder().Build();
        }

        private int HandleLoop(int loopIndex, List<Command> commands) {
            int endLoopIndex = commands.IndexOf(commands.Skip(loopIndex).FirstOrDefault(command => command is EndLoop, new EndLoop()));

            if (endLoopIndex == -1) {
                throw new CommandNotFoundException("Loop command has no defined end.");
            }

            string loopedProgram = string.Join("\n", commands.Skip(loopIndex + 1).Take(endLoopIndex - loopIndex - 1).Select(command => command.ToString()).ToArray());

            while (((ISelection)commands[loopIndex]).Evaluate()) {
                RunProgram(loopedProgram);
                commands[loopIndex].Execute(drawer, variableManager);
            }

            return endLoopIndex;
        }

        private int HandleIfBlock(int ifIndex, List<Command> commands) {
            int endIfIndex = commands.IndexOf(commands.Skip(ifIndex).FirstOrDefault(command => command is EndIf, new EndIf()));

            if (endIfIndex == -1) {
                throw new CommandNotFoundException("If block has no defined end.");
            }

            string ifBlock = string.Join("\n", commands.Skip(ifIndex + 1).Take(endIfIndex - ifIndex - 1).Select(command => command.ToString()).ToArray());

            if (((ISelection)commands[ifIndex]).Evaluate()) {
                RunProgram(ifBlock);
            }

            return endIfIndex;
        }
    }
}
