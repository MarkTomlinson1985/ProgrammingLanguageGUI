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
            runPanel = new Panel();
            runProgram = new Button();
            programEditor = new RichTextBox();
            outputText = new RichTextBox();
            lineText = new TextBox();
            commandText = new TextBox();
            runCommand = new Button();
            drawingBox = new PictureBox();
            runPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)drawingBox).BeginInit();
            SuspendLayout();
            // 
            // runPanel
            // 
            runPanel.BackColor = Color.FromArgb(84, 86, 109);
            runPanel.Controls.Add(runProgram);
            runPanel.ForeColor = SystemColors.ControlText;
            runPanel.Location = new Point(17, 3);
            runPanel.Name = "runPanel";
            runPanel.Size = new Size(1246, 39);
            runPanel.TabIndex = 2;
            // 
            // runProgram
            // 
            runProgram.BackColor = Color.FromArgb(84, 86, 109);
            runProgram.FlatStyle = FlatStyle.Flat;
            runProgram.ForeColor = Color.FromArgb(224, 224, 224);
            runProgram.Location = new Point(572, 7);
            runProgram.Name = "runProgram";
            runProgram.Size = new Size(48, 29);
            runProgram.TabIndex = 0;
            runProgram.Text = "Run";
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
            programEditor.Size = new Size(580, 522);
            programEditor.TabIndex = 3;
            programEditor.Text = "";
            programEditor.TextChanged += TextEditor_TextChanged;
            // 
            // outputText
            // 
            outputText.BackColor = Color.FromArgb(69, 69, 84);
            outputText.BorderStyle = BorderStyle.None;
            outputText.ForeColor = Color.FromArgb(232, 232, 232);
            outputText.Location = new Point(17, 610);
            outputText.Name = "outputText";
            outputText.Size = new Size(1246, 151);
            outputText.TabIndex = 4;
            outputText.Text = "Output text\n";
            // 
            // lineText
            // 
            lineText.BackColor = Color.FromArgb(69, 69, 84);
            lineText.BorderStyle = BorderStyle.None;
            lineText.ForeColor = Color.FromArgb(232, 232, 232);
            lineText.Location = new Point(17, 48);
            lineText.Multiline = true;
            lineText.Name = "lineText";
            lineText.Size = new Size(34, 522);
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
            // 
            // runCommand
            // 
            runCommand.BackColor = Color.FromArgb(84, 86, 109);
            runCommand.FlatStyle = FlatStyle.Flat;
            runCommand.ForeColor = Color.FromArgb(224, 224, 224);
            runCommand.Location = new Point(589, 575);
            runCommand.Name = "runCommand";
            runCommand.Size = new Size(48, 29);
            runCommand.TabIndex = 1;
            runCommand.Text = "Run";
            runCommand.UseVisualStyleBackColor = false;
            runCommand.Click += runCommand_Click;
            // 
            // drawingBox
            // 
            drawingBox.BackColor = SystemColors.ControlDark;
            drawingBox.Location = new Point(643, 48);
            drawingBox.Name = "drawingBox";
            drawingBox.Size = new Size(616, 554);
            drawingBox.TabIndex = 7;
            drawingBox.TabStop = false;
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
        private RichTextBox programEditor;
        private RichTextBox outputText;
        private TextBox lineText;
        private Button runProgram;
        private TextBox commandText;
        private Button runCommand;
        private PictureBox drawingBox;
    }
}
