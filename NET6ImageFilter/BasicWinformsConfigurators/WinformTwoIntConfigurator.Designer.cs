namespace NET6ImageFilter.BasicWinformsConfigurators
{
    partial class WinformTwoIntConfigurator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.promptLabelFirst = new System.Windows.Forms.Label();
            this.inputBoxFirst = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.promptLabelSecond = new System.Windows.Forms.Label();
            this.inputBoxSecond = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // promptLabelFirst
            // 
            this.promptLabelFirst.AutoSize = true;
            this.promptLabelFirst.Location = new System.Drawing.Point(12, 9);
            this.promptLabelFirst.Name = "promptLabelFirst";
            this.promptLabelFirst.Size = new System.Drawing.Size(113, 15);
            this.promptLabelFirst.TabIndex = 5;
            this.promptLabelFirst.Text = "promptLabelDefault";
            // 
            // inputBoxFirst
            // 
            this.inputBoxFirst.Location = new System.Drawing.Point(12, 26);
            this.inputBoxFirst.Name = "inputBoxFirst";
            this.inputBoxFirst.Size = new System.Drawing.Size(310, 23);
            this.inputBoxFirst.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(247, 99);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 24);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // promptLabelSecond
            // 
            this.promptLabelSecond.AutoSize = true;
            this.promptLabelSecond.Location = new System.Drawing.Point(12, 52);
            this.promptLabelSecond.Name = "promptLabelSecond";
            this.promptLabelSecond.Size = new System.Drawing.Size(113, 15);
            this.promptLabelSecond.TabIndex = 5;
            this.promptLabelSecond.Text = "promptLabelDefault";
            // 
            // inputBoxSecond
            // 
            this.inputBoxSecond.Location = new System.Drawing.Point(12, 70);
            this.inputBoxSecond.Name = "inputBoxSecond";
            this.inputBoxSecond.Size = new System.Drawing.Size(310, 23);
            this.inputBoxSecond.TabIndex = 4;
            // 
            // WinformTwoIntConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 131);
            this.Controls.Add(this.promptLabelSecond);
            this.Controls.Add(this.promptLabelFirst);
            this.Controls.Add(this.inputBoxSecond);
            this.Controls.Add(this.inputBoxFirst);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinformTwoIntConfigurator";
            this.Text = "WinformTwoIntConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label promptLabelFirst;
        private TextBox inputBoxFirst;
        private Button okButton;
        private Label promptLabelSecond;
        private TextBox inputBoxSecond;
    }
}