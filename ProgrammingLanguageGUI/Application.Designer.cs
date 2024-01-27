namespace ProgrammingLanguageGUI {
    public partial class Application
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application));
            runPanel = new Panel();
            toggleSyntaxButton = new Button();
            newButton = new Button();
            saveButton = new Button();
            openButton = new Button();
            runProgram = new Button();
            programEditor = new ProgramEditor();
            outputText = new RichTextBox();
            lineText = new TextBox();
            commandText = new TextBox();
            runCommand = new Button();
            drawingBox = new PictureBox();
            buttonTooltip = new ToolTip(components);
            runPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)drawingBox).BeginInit();
            SuspendLayout();
            // 
            // runPanel
            // 
            runPanel.BackColor = Color.FromArgb(84, 86, 109);
            runPanel.Controls.Add(toggleSyntaxButton);
            runPanel.Controls.Add(newButton);
            runPanel.Controls.Add(saveButton);
            runPanel.Controls.Add(openButton);
            runPanel.Controls.Add(runProgram);
            runPanel.ForeColor = SystemColors.ControlText;
            runPanel.Location = new Point(17, 3);
            runPanel.Name = "runPanel";
            runPanel.Size = new Size(1246, 39);
            runPanel.TabIndex = 2;
            // 
            // toggleSyntaxButton
            // 
            toggleSyntaxButton.BackColor = Color.FromArgb(84, 86, 109);
            toggleSyntaxButton.BackgroundImageLayout = ImageLayout.Stretch;
            toggleSyntaxButton.FlatAppearance.BorderSize = 0;
            toggleSyntaxButton.FlatStyle = FlatStyle.Flat;
            toggleSyntaxButton.ForeColor = Color.LimeGreen;
            toggleSyntaxButton.Location = new Point(105, 3);
            toggleSyntaxButton.Margin = new Padding(0);
            toggleSyntaxButton.Name = "toggleSyntaxButton";
            toggleSyntaxButton.Size = new Size(43, 30);
            toggleSyntaxButton.TabIndex = 4;
            toggleSyntaxButton.Text = "</>";
            toggleSyntaxButton.UseVisualStyleBackColor = false;
            toggleSyntaxButton.Click += toggleSyntaxButton_Click;
            toggleSyntaxButton.MouseHover += toggleSyntaxButton_MouseHover;
            // 
            // newButton
            // 
            newButton.BackColor = Color.FromArgb(84, 86, 109);
            newButton.BackgroundImage = (Image)resources.GetObject("newButton.BackgroundImage");
            newButton.BackgroundImageLayout = ImageLayout.Stretch;
            newButton.FlatAppearance.BorderSize = 0;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.ForeColor = Color.FromArgb(224, 224, 224);
            newButton.Location = new Point(8, 9);
            newButton.Name = "newButton";
            newButton.Size = new Size(22, 21);
            newButton.TabIndex = 3;
            newButton.UseVisualStyleBackColor = false;
            newButton.Click += newButton_Click;
            // 
            // saveButton
            // 
            saveButton.BackColor = Color.FromArgb(84, 86, 109);
            saveButton.BackgroundImage = (Image)resources.GetObject("saveButton.BackgroundImage");
            saveButton.BackgroundImageLayout = ImageLayout.Center;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.ForeColor = Color.FromArgb(224, 224, 224);
            saveButton.Location = new Point(57, 4);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(32, 29);
            saveButton.TabIndex = 2;
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // openButton
            // 
            openButton.BackColor = Color.FromArgb(84, 86, 109);
            openButton.BackgroundImage = (Image)resources.GetObject("openButton.BackgroundImage");
            openButton.BackgroundImageLayout = ImageLayout.Center;
            openButton.FlatAppearance.BorderSize = 0;
            openButton.FlatStyle = FlatStyle.Flat;
            openButton.ForeColor = Color.FromArgb(224, 224, 224);
            openButton.Location = new Point(32, 4);
            openButton.Name = "openButton";
            openButton.Size = new Size(32, 29);
            openButton.TabIndex = 1;
            openButton.UseVisualStyleBackColor = false;
            openButton.Click += openButton_Click;
            // 
            // runProgram
            // 
            runProgram.BackColor = Color.FromArgb(84, 86, 109);
            runProgram.BackgroundImage = (Image)resources.GetObject("runProgram.BackgroundImage");
            runProgram.BackgroundImageLayout = ImageLayout.Center;
            runProgram.FlatAppearance.BorderSize = 0;
            runProgram.FlatStyle = FlatStyle.Flat;
            runProgram.ForeColor = Color.FromArgb(224, 224, 224);
            runProgram.Location = new Point(587, 5);
            runProgram.Name = "runProgram";
            runProgram.Size = new Size(32, 29);
            runProgram.TabIndex = 0;
            runProgram.UseVisualStyleBackColor = false;
            runProgram.Click += runProgram_Click;
            // 
            // programEditor
            // 
            programEditor.AcceptsTab = true;
            programEditor.BackColor = Color.FromArgb(69, 69, 84);
            programEditor.BorderStyle = BorderStyle.None;
            programEditor.ForeColor = Color.FromArgb(232, 232, 232);
            programEditor.Location = new Point(57, 48);
            programEditor.Name = "programEditor";
            programEditor.Size = new Size(581, 523);
            programEditor.TabIndex = 3;
            programEditor.Text = "";
            programEditor.TextChanged += TextEditor_TextChanged;
            programEditor.KeyPress += programEditor_KeyPress;
            // 
            // outputText
            // 
            outputText.BackColor = Color.FromArgb(69, 69, 84);
            outputText.BorderStyle = BorderStyle.None;
            outputText.ForeColor = Color.FromArgb(232, 232, 232);
            outputText.Location = new Point(17, 611);
            outputText.Name = "outputText";
            outputText.Size = new Size(1246, 151);
            outputText.TabIndex = 4;
            outputText.Text = "";
            // 
            // lineText
            // 
            lineText.BackColor = Color.FromArgb(69, 69, 84);
            lineText.BorderStyle = BorderStyle.None;
            lineText.ForeColor = Color.FromArgb(232, 232, 232);
            lineText.Location = new Point(17, 48);
            lineText.Multiline = true;
            lineText.Name = "lineText";
            lineText.Size = new Size(34, 523);
            lineText.TabIndex = 5;
            lineText.Text = "1";
            lineText.TextAlign = HorizontalAlignment.Right;
            // 
            // commandText
            // 
            commandText.BackColor = Color.FromArgb(69, 69, 84);
            commandText.BorderStyle = BorderStyle.None;
            commandText.ForeColor = Color.FromArgb(224, 224, 224);
            commandText.Location = new Point(17, 579);
            commandText.Name = "commandText";
            commandText.Size = new Size(566, 20);
            commandText.TabIndex = 6;
            commandText.KeyPress += commandText_KeyPress;
            commandText.KeyUp += commandText_KeyUp;
            // 
            // runCommand
            // 
            runCommand.BackColor = Color.FromArgb(84, 86, 109);
            runCommand.BackgroundImage = (Image)resources.GetObject("runCommand.BackgroundImage");
            runCommand.BackgroundImageLayout = ImageLayout.Center;
            runCommand.FlatAppearance.BorderSize = 0;
            runCommand.FlatStyle = FlatStyle.Flat;
            runCommand.ForeColor = Color.FromArgb(224, 224, 224);
            runCommand.Location = new Point(589, 575);
            runCommand.Name = "runCommand";
            runCommand.Size = new Size(48, 29);
            runCommand.TabIndex = 1;
            runCommand.UseVisualStyleBackColor = false;
            runCommand.Click += runCommand_Click;
            // 
            // drawingBox
            // 
            drawingBox.BackColor = Color.FromArgb(71, 74, 92);
            drawingBox.Location = new Point(643, 48);
            drawingBox.Name = "drawingBox";
            drawingBox.Size = new Size(619, 555);
            drawingBox.TabIndex = 7;
            drawingBox.TabStop = false;
            // 
            // buttonTooltip
            // 
            buttonTooltip.AutoPopDelay = 5000;
            buttonTooltip.InitialDelay = 200;
            buttonTooltip.ReshowDelay = 100;
            // 
            // Application
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(58, 60, 78);
            ClientSize = new Size(1275, 773);
            Controls.Add(drawingBox);
            Controls.Add(runCommand);
            Controls.Add(commandText);
            Controls.Add(lineText);
            Controls.Add(outputText);
            Controls.Add(programEditor);
            Controls.Add(runPanel);
            Name = "Application";
            Text = "Programming GUI";
            runPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)drawingBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel runPanel;
        private ProgramEditor programEditor;
        private RichTextBox outputText;
        private TextBox lineText;
        private Button runProgram;
        private TextBox commandText;
        private Button runCommand;
        private PictureBox drawingBox;
        private Button openButton;
        private Button saveButton;
        private Button newButton;
        private Button toggleSyntaxButton;
        private ToolTip buttonTooltip;
    }
}
