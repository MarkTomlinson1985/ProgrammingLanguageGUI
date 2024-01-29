using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI.runner {
    /// <summary>
    /// Runner class for the execution of commands and programs.
    /// </summary>
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

        /// <summary>
        /// Run the provided string argument as an invidual command.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string RunCommand(string input) {
            try {
                Command command = processor.ParseCommand(input);
                command.Execute(drawer, variableManager);

                return "Command run successfully.";
            } catch (Exception ex) {
                return ex.Message;
            }
        }

        /// <summary>
        /// Runs the provided new-line separated string argument as a program.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="CommandNotFoundException"></exception>
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

                        i = HandleLoop(i, commands, exceptions);
                        continue;
                    }

                    // If command
                    if (commands[i] is If ifCommand) {
                        if (ifCommand.HasInlineCommand()) {
                            if (ifCommand.Evaluate()) {
                                Command inlineCommand = ifCommand.Retrieve();
                                inlineCommand.Execute(drawer, variableManager);
                            }
                            continue;
                        }

                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            continue;
                        }

                        i = HandleIfBlock(i, commands, exceptions);
                        continue;
                    }

                    // Method command
                    if (commands[i] is Method method) {
                        int startLineNumber = i + 1;
                        int endMethodIndex = commands.IndexOf(commands.Skip(i).FirstOrDefault(command => command is EndMethod, new EndMethod()));

                        if (endMethodIndex == -1) {
                            throw new CommandNotFoundException("Method command has no defined end.");
                        }

                        int endLineNumber = endMethodIndex;

                        Command[] methodCommands = commands.GetRange(startLineNumber, endLineNumber - startLineNumber).ToArray();
                        method.Commands = methodCommands;

                        i = endLineNumber;
                        continue;
                    }

                    // CallMethod command
                    if (commands[i] is CallMethod callMethod) {
                        Command[] methodCommands = callMethod.GetMethodCommands();

                        if (methodCommands == null || methodCommands.Length == 0) {
                            callMethod.UnassignVariables(variableManager);
                            throw new CommandNotFoundException($"Improperly declared method: '{callMethod.MethodName}'.");
                        }


                        if (!drawer.DrawerProperties.DrawerEnabled) {
                            continue;
                        }

                        string methodProgram =
                            string.Join(
                                "\n",
                                methodCommands
                                    .Select(command => command.ToString())
                                    .ToArray());

                        string output = RunProgram(methodProgram);
                        if (!PROGRAM_SUCCESS_MESSAGE.Equals(output)) {
                            foreach (string exception in output.Split("\n")) {
                                exceptions.Add(new CommandException(exception));
                            }
                        }


                        // Descope variables declared in method.
                        callMethod.UnassignVariables(variableManager);
                        continue;
                    }

                } catch (CommandException ex) {
                    exceptions.Add(new CommandException($"Line {results.GetCommands()[commands[i]]}: {commands[i]} - {ex.Message}"));
                }
            }

            // If there are any exceptions return these as the output.
            // Otherwise return a success message.
            if (exceptions.Count > 0) {
                string exceptionOutput = string.Empty;
                foreach (CommandException ex in exceptions) {
                    exceptionOutput = exceptionOutput + ex.Message + "\n";
                }
                return exceptionOutput;
            }

            return PROGRAM_SUCCESS_MESSAGE;
        }

        /// <summary>
        /// Checks the syntax of a program. Runs the program to validate commands
        /// but does not draw any output to the screen.
        /// </summary>
        /// <param name="program"></param>
        /// <returns>Syntax results, containing syntax errors and line numbers with syntax errors.</returns>
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

        /// <summary>
        /// Validates and executes a code block contained within a loop command.
        /// </summary>
        /// <param name="loopIndex"></param>
        /// <param name="commands"></param>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        /// <exception cref="CommandNotFoundException">If loop has no declared endloop command.</exception>
        private int HandleLoop(int loopIndex, List<Command> commands, List<CommandException> exceptions) {
            int endLoopIndex = FindBlockEnd(loopIndex, commands, typeof(While), typeof(EndLoop));

            if (endLoopIndex == -1) {
                throw new CommandNotFoundException("Loop command has no defined end.");
            }

            // Converts the commands inbetween the loop and endloop commands into 
            // the string representations of the commands.
            string loopedProgram = 
                string.Join(
                    "\n", 
                    commands.Skip(loopIndex + 1)
                        .Take(endLoopIndex - loopIndex - 1)
                        .Select(command => command.ToString())
                        .ToArray());

            // Executes the code block whilever the loop command evaluates to true.
            // Any exceptions within the block are added to the list of exceptions, unless
            // an identical exception message already exists. Prevents the same errors from
            // being logged every time round the loop.
            while (((While)commands[loopIndex]).Evaluate()) {
                string output = RunProgram(loopedProgram);
                if (!PROGRAM_SUCCESS_MESSAGE.Equals(output)) {
                    foreach (string exception in output.Split("\n")) {
                        if (!exceptions.Any(savedException => savedException.Message.Equals(exception))) {
                            exceptions.Add(new CommandException(exception));
                        }
                    }
                }
                commands[loopIndex].Execute(drawer, variableManager);
            }

            return endLoopIndex;
        }

        /// <summary>
        /// Validates and executes a code block contained within an If command.
        /// </summary>
        /// <param name="ifIndex"></param>
        /// <param name="commands"></param>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        /// <exception cref="CommandNotFoundException"></exception>
        private int HandleIfBlock(int ifIndex, List<Command> commands, List<CommandException> exceptions) {
            int endIfIndex = FindBlockEnd(ifIndex, commands, typeof(If), typeof(EndIf));

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

            // Runs the block of code within the If block if the If command
            // condition evaluates as true.
            if (((If)commands[ifIndex]).Evaluate()) {
                string output = RunProgram(ifBlock);
                if (!PROGRAM_SUCCESS_MESSAGE.Equals(output)) {
                    foreach (string exception in output.Split("\n")) {
                        exceptions.Add(new CommandException(exception));
                    }
                }
            }

            return endIfIndex;
        }

        /// <summary>
        /// Finds the corresponding end command index to for a given command that contains a block
        /// of code. 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="commands"></param>
        /// <param name="blockStartType"></param>
        /// <param name="blockEndType"></param>
        /// <returns></returns>
        /// <exception cref="CommandNotFoundException"></exception>
        private int FindBlockEnd(int startIndex, List<Command> commands, Type blockStartType, Type blockEndType) {
            int blockStartCommands = 1;
            int blockEndCommands = 0;
            int endIndex = -1;

            for (int i = startIndex + 1; i < commands.Count(); i++) {
                if (commands[i].GetType().Equals(blockEndType)) {
                    blockEndCommands++;

                    if (blockEndCommands == blockStartCommands) {
                        endIndex = i;
                        break;
                    }
                }

                if (commands[i].GetType().Equals(blockStartType)) {
                    if (commands[i] is not IInlineCommand
                        || (commands[i] is IInlineCommand inlineCommand && !inlineCommand.HasInlineCommand())) {
                        blockStartCommands++;
                    }
                }
            }

            if (endIndex == -1) {
                throw new CommandNotFoundException("Block command has no defined end.");
            }

            return endIndex;
        }
    }
}
