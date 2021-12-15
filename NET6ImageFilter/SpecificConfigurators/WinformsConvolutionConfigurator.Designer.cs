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
            this.biasTextbox = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // matrixTextbox
            // 
            this.matrixTextbox.Location = new System.Drawing.Point(12, 12);
            this.matrixTextbox.Multiline = true;
            this.matrixTextbox.Name = "matrixTextbox";
            this.matrixTextbox.Size = new System.Drawing.Size(463, 235);
            this.matrixTextbox.TabIndex = 0;
            this.matrixTextbox.Text = "0,1,0\r\n1,1,1\r\n0,1,0";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(375, 254);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(100, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // biasTextbox
            // 
            this.biasTextbox.Location = new System.Drawing.Point(269, 254);
            this.biasTextbox.Name = "biasTextbox";
            this.biasTextbox.Size = new System.Drawing.Size(100, 23);
            this.biasTextbox.TabIndex = 3;
            this.biasTextbox.Text = "1.0";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 253);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(93, 253);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // WinformsConvolutionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 288);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.biasTextbox);
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
        private TextBox biasTextbox;
        private Button loadButton;
        private Button saveButton;
    }
}