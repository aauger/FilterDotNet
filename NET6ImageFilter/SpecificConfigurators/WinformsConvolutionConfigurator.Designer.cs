namespace NET6ImageFilter.SpecificConfigurators
{
    partial class WinformsConvolutionConfigurator
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
            this.matrixTextbox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.normalCheckbox = new System.Windows.Forms.CheckBox();
            this.biasTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // matrixTextbox
            // 
            this.matrixTextbox.Location = new System.Drawing.Point(12, 12);
            this.matrixTextbox.Multiline = true;
            this.matrixTextbox.Name = "matrixTextbox";
            this.matrixTextbox.Size = new System.Drawing.Size(231, 111);
            this.matrixTextbox.TabIndex = 0;
            this.matrixTextbox.Text = "0,1,0\r\n1,1,1\r\n0,1,0";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(249, 100);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(100, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // normalCheckbox
            // 
            this.normalCheckbox.AutoSize = true;
            this.normalCheckbox.Checked = true;
            this.normalCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalCheckbox.Location = new System.Drawing.Point(249, 41);
            this.normalCheckbox.Name = "normalCheckbox";
            this.normalCheckbox.Size = new System.Drawing.Size(85, 19);
            this.normalCheckbox.TabIndex = 2;
            this.normalCheckbox.Text = "Normalize?";
            this.normalCheckbox.UseVisualStyleBackColor = true;
            // 
            // biasTextbox
            // 
            this.biasTextbox.Location = new System.Drawing.Point(249, 12);
            this.biasTextbox.Name = "biasTextbox";
            this.biasTextbox.Size = new System.Drawing.Size(100, 23);
            this.biasTextbox.TabIndex = 3;
            this.biasTextbox.Text = "1.0";
            // 
            // WinformsConvolutionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 134);
            this.Controls.Add(this.biasTextbox);
            this.Controls.Add(this.normalCheckbox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.matrixTextbox);
            this.Name = "WinformsConvolutionConfigurator";
            this.Text = "WinformsConvolutionConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox matrixTextbox;
        private Button applyButton;
        private CheckBox normalCheckbox;
        private TextBox biasTextbox;
    }
}