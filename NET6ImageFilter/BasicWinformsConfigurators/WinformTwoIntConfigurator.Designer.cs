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
            this.fLabel = new System.Windows.Forms.Label();
            this.fInput = new System.Windows.Forms.TextBox();
            this.sLabel = new System.Windows.Forms.Label();
            this.sInput = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fLabel
            // 
            this.fLabel.AutoSize = true;
            this.fLabel.Location = new System.Drawing.Point(12, 9);
            this.fLabel.Name = "fLabel";
            this.fLabel.Size = new System.Drawing.Size(0, 15);
            this.fLabel.TabIndex = 0;
            // 
            // fInput
            // 
            this.fInput.Location = new System.Drawing.Point(12, 27);
            this.fInput.Name = "fInput";
            this.fInput.Size = new System.Drawing.Size(469, 23);
            this.fInput.TabIndex = 1;
            // 
            // sLabel
            // 
            this.sLabel.AutoSize = true;
            this.sLabel.Location = new System.Drawing.Point(12, 53);
            this.sLabel.Name = "sLabel";
            this.sLabel.Size = new System.Drawing.Size(0, 15);
            this.sLabel.TabIndex = 0;
            // 
            // sInput
            // 
            this.sInput.Location = new System.Drawing.Point(12, 71);
            this.sInput.Name = "sInput";
            this.sInput.Size = new System.Drawing.Size(469, 23);
            this.sInput.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(406, 100);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // WinformTwoIntConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 130);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.sInput);
            this.Controls.Add(this.fInput);
            this.Controls.Add(this.sLabel);
            this.Controls.Add(this.fLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WinformTwoIntConfigurator";
            this.Text = "WinformTwoIntConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label fLabel;
        private TextBox fInput;
        private Label sLabel;
        private TextBox sInput;
        private Button okButton;
    }
}