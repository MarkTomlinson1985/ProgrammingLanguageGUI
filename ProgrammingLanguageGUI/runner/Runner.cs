using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.runner {
    public class Runner {
        private const string PROGRAM_SUCCESS_MESSAGE = "Program executed successfully.";
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

                return "Command run successfully.";
            } catch (Exception ex) {
                return ex.Message;
            }
        }

        public string RunProgram(string input) {
            ProgramResults results = processor.ParseProgram(input);

            List<Command> commands = results.GetCommands().Keys.ToList();
            List<CommandException> exceptions = results.GetExceptions();

            for (int i = 0; i < commands.Count; i++) {
                try {
                    if (commands[i] is TransformCommand) {
                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            commands[i].ValidateCommand(variableManager);
                            continue;
                        }
                    }

                    commands[i].Execute(drawer, variableManager);

                    // Special cases - These commands provide functionality beyond the execution
                    // stage, e.g. defining blocks of code or calling methods.

                    // While command
                    if (commands[i] is While) {                        
                        // If purely checking syntax and not running program, move on to first
                        // command in code block sequentially to check syntax instead of looping.
                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            continue;
                        }

                        i = HandleLoop(i, commands);
                        continue;
                    }

                    // If command
                    if (commands[i] is If ifCommand) {

                        if (ifCommand.Evaluate()) {
                            if (ifCommand.HasInlineCommand()) {
                                Command inlineCommand = ifCommand.Retrieve();
                                inlineCommand.Execute(drawer, variableManager);
                                continue;
                            }
                        }

                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            continue;
                        }

                        i = HandleIfBlock(i, commands);
                        continue;
                    }

                    // Method command
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

                    // CallMethod command
                    if (commands[i] is CallMethod callMethod) {
                        if (callMethod.GetMethodEnd() == -1) {
                            callMethod.UnassignVariables(variableManager);
                            throw new CommandNotFoundException($"Improperly declared method: '{callMethod.MethodName}'.");
                        }

                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            continue;
                        }

                        for (int j = callMethod.GetMethodStart(); j <= callMethod.GetMethodEnd(); j++) {
                            Command command = results.GetCommands().First(entry => entry.Value == j).Key;
                            command.Execute(drawer, variableManager);
                        }
                        // Descope variables declared in method.
                        callMethod.UnassignVariables(variableManager);
                        continue;
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

            return PROGRAM_SUCCESS_MESSAGE;
        }

        public SyntaxResults CheckProgramSyntax(string program) {
            try {
                drawer.DrawerProperties.DrawerEnabled = false;
                
                string programOutput = RunProgram(program);

                if (!PROGRAM_SUCCESS_MESSAGE.Equals(programOutput)) {
                    string[] exceptions = 
                        programOutput.Split('\n')
                            .Where(line => !line.Trim().Equals(string.Empty))
                            .OrderBy(line => int.Parse(line.Split(":")[0].Split(" ")[1]))
                            .ToArray();

                    int[] lineNumbers = 
                        exceptions.Select(
                            exception => int.Parse(exception.Split(":")[0].Replace("Line ", "")))
                        .ToArray();
                    
                    drawer.DrawerProperties.DrawerEnabled = true;

                    return SyntaxResults.Builder()
                        .LineNumbers(lineNumbers)
                        .SyntaxErrors(exceptions)
                        .Build();
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            drawer.DrawerProperties.DrawerEnabled = true;
            return SyntaxResults.Builder().LineNumbers([]).SyntaxErrors([]).Build();
        }

        private int HandleLoop(int loopIndex, List<Command> commands) {
            int endLoopIndex = 
                    commands.IndexOf(
                        commands.Skip(loopIndex)
                            .LastOrDefault(command => command is EndLoop, new EndLoop()));

            if (endLoopIndex == -1) {
                throw new CommandNotFoundException("Loop command has no defined end.");
            }

            string loopedProgram = 
                string.Join(
                    "\n", 
                    commands.Skip(loopIndex + 1)
                        .Take(endLoopIndex - loopIndex - 1)
                        .Select(command => command.ToString())
                        .ToArray());

            while (((While)commands[loopIndex]).Evaluate()) {
                RunProgram(loopedProgram);
                commands[loopIndex].Execute(drawer, variableManager);
            }

            return endLoopIndex;
        }

        private int HandleIfBlock(int ifIndex, List<Command> commands) {
            int endIfIndex = 
                commands.IndexOf(
                    commands.Skip(ifIndex)
                        .FirstOrDefault(command => command is EndIf, new EndIf()));

            if (endIfIndex == -1) {
                throw new CommandNotFoundException("If block has no defined end.");
            }

            string ifBlock = 
                string.Join(
                    "\n", 
                    commands.Skip(ifIndex + 1)
                        .Take(endIfIndex - ifIndex - 1)
                        .Select(command => command.ToString())
                        .ToArray());

            if (((If)commands[ifIndex]).Evaluate()) {
                RunProgram(ifBlock);
            }

            return endIfIndex;
        }
    }
}
