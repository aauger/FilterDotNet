namespace NET6ImageFilter.SpecificConfigurators
{
    partial class WinformsNormalMapConfigurator
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
            this.multiplierTextBox = new System.Windows.Forms.TextBox();
            this.multiplierLabel = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.filePathLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // multiplierTextBox
            // 
            this.multiplierTextBox.Location = new System.Drawing.Point(12, 71);
            this.multiplierTextBox.Name = "multiplierTextBox";
            this.multiplierTextBox.Size = new System.Drawing.Size(299, 23);
            this.multiplierTextBox.TabIndex = 0;
            // 
            // multiplierLabel
            // 
            this.multiplierLabel.AutoSize = true;
            this.multiplierLabel.Location = new System.Drawing.Point(12, 53);
            this.multiplierLabel.Name = "multiplierLabel";
            this.multiplierLabel.Size = new System.Drawing.Size(58, 15);
            this.multiplierLabel.TabIndex = 1;
            this.multiplierLabel.Text = "Multiplier";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(12, 27);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(213, 23);
            this.filePathTextBox.TabIndex = 2;
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Location = new System.Drawing.Point(12, 9);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(52, 15);
            this.filePathLabel.TabIndex = 3;
            this.filePathLabel.Text = "File Path";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(231, 27);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(80, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 111);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(299, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // WinformsNormalMapConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 146);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.filePathLabel);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.multiplierLabel);
            this.Controls.Add(this.multiplierTextBox);
            this.Name = "WinformsNormalMapConfigurator";
            this.Text = "WinformsNormalMapConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox multiplierTextBox;
        private Label multiplierLabel;
        private TextBox filePathTextBox;
        private Label filePathLabel;
        private Button browseButton;
        private Button okButton;
    }
}