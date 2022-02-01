namespace NET6ImageFilter.BasicWinformsConfigurators
{
    partial class WinformThreeIntConfigurator
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
            this.okButton = new System.Windows.Forms.Button();
            this.promptLabelFirst = new System.Windows.Forms.Label();
            this.inputBoxFirst = new System.Windows.Forms.TextBox();
            this.promptLabelSecond = new System.Windows.Forms.Label();
            this.inputBoxSecond = new System.Windows.Forms.TextBox();
            this.promptLabelThird = new System.Windows.Forms.Label();
            this.inputBoxThird = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(247, 148);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 24);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // promptLabelFirst
            // 
            this.promptLabelFirst.AutoSize = true;
            this.promptLabelFirst.Location = new System.Drawing.Point(12, 9);
            this.promptLabelFirst.Name = "promptLabelFirst";
            this.promptLabelFirst.Size = new System.Drawing.Size(113, 15);
            this.promptLabelFirst.TabIndex = 10;
            this.promptLabelFirst.Text = "promptLabelDefault";
            // 
            // inputBoxFirst
            // 
            this.inputBoxFirst.Location = new System.Drawing.Point(12, 27);
            this.inputBoxFirst.Name = "inputBoxFirst";
            this.inputBoxFirst.Size = new System.Drawing.Size(310, 23);
            this.inputBoxFirst.TabIndex = 0;
            // 
            // promptLabelSecond
            // 
            this.promptLabelSecond.AutoSize = true;
            this.promptLabelSecond.Location = new System.Drawing.Point(12, 53);
            this.promptLabelSecond.Name = "promptLabelSecond";
            this.promptLabelSecond.Size = new System.Drawing.Size(113, 15);
            this.promptLabelSecond.TabIndex = 10;
            this.promptLabelSecond.Text = "promptLabelDefault";
            // 
            // inputBoxSecond
            // 
            this.inputBoxSecond.Location = new System.Drawing.Point(12, 71);
            this.inputBoxSecond.Name = "inputBoxSecond";
            this.inputBoxSecond.Size = new System.Drawing.Size(310, 23);
            this.inputBoxSecond.TabIndex = 1;
            // 
            // promptLabelThird
            // 
            this.promptLabelThird.AutoSize = true;
            this.promptLabelThird.Location = new System.Drawing.Point(12, 97);
            this.promptLabelThird.Name = "promptLabelThird";
            this.promptLabelThird.Size = new System.Drawing.Size(113, 15);
            this.promptLabelThird.TabIndex = 10;
            this.promptLabelThird.Text = "promptLabelDefault";
            // 
            // inputBoxThird
            // 
            this.inputBoxThird.Location = new System.Drawing.Point(12, 115);
            this.inputBoxThird.Name = "inputBoxThird";
            this.inputBoxThird.Size = new System.Drawing.Size(310, 23);
            this.inputBoxThird.TabIndex = 2;
            // 
            // WinformThreeIntConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 181);
            this.Controls.Add(this.promptLabelThird);
            this.Controls.Add(this.promptLabelSecond);
            this.Controls.Add(this.promptLabelFirst);
            this.Controls.Add(this.inputBoxThird);
            this.Controls.Add(this.inputBoxSecond);
            this.Controls.Add(this.inputBoxFirst);
            this.Controls.Add(this.okButton);
            this.Name = "WinformThreeIntConfigurator";
            this.Text = "WinformThreeIntConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button okButton;
        private Label promptLabelFirst;
        private TextBox inputBoxFirst;
        private Label promptLabelSecond;
        private TextBox inputBoxSecond;
        private Label promptLabelThird;
        private TextBox inputBoxThird;
    }
}