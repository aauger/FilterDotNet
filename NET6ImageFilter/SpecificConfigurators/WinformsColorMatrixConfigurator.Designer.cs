namespace NET6ImageFilter.SpecificConfigurators
{
    partial class WinformsColorMatrixConfigurator
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
            this.redRedTextBox = new System.Windows.Forms.TextBox();
            this.redGreenTextBox = new System.Windows.Forms.TextBox();
            this.redBlueTextBox = new System.Windows.Forms.TextBox();
            this.greenRedTextBox = new System.Windows.Forms.TextBox();
            this.greenGreenTextBox = new System.Windows.Forms.TextBox();
            this.greenBlueTextBox = new System.Windows.Forms.TextBox();
            this.blueRedTextBox = new System.Windows.Forms.TextBox();
            this.blueGreenTextBox = new System.Windows.Forms.TextBox();
            this.blueBlueTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // redRedTextBox
            // 
            this.redRedTextBox.Location = new System.Drawing.Point(12, 12);
            this.redRedTextBox.Name = "redRedTextBox";
            this.redRedTextBox.Size = new System.Drawing.Size(47, 23);
            this.redRedTextBox.TabIndex = 0;
            this.redRedTextBox.Text = "1.0";
            // 
            // redGreenTextBox
            // 
            this.redGreenTextBox.Location = new System.Drawing.Point(65, 12);
            this.redGreenTextBox.Name = "redGreenTextBox";
            this.redGreenTextBox.Size = new System.Drawing.Size(47, 23);
            this.redGreenTextBox.TabIndex = 0;
            this.redGreenTextBox.Text = "0.0";
            // 
            // redBlueTextBox
            // 
            this.redBlueTextBox.Location = new System.Drawing.Point(118, 12);
            this.redBlueTextBox.Name = "redBlueTextBox";
            this.redBlueTextBox.Size = new System.Drawing.Size(47, 23);
            this.redBlueTextBox.TabIndex = 0;
            this.redBlueTextBox.Text = "0.0";
            // 
            // greenRedTextBox
            // 
            this.greenRedTextBox.Location = new System.Drawing.Point(12, 41);
            this.greenRedTextBox.Name = "greenRedTextBox";
            this.greenRedTextBox.Size = new System.Drawing.Size(47, 23);
            this.greenRedTextBox.TabIndex = 0;
            this.greenRedTextBox.Text = "0.0";
            // 
            // greenGreenTextBox
            // 
            this.greenGreenTextBox.Location = new System.Drawing.Point(65, 41);
            this.greenGreenTextBox.Name = "greenGreenTextBox";
            this.greenGreenTextBox.Size = new System.Drawing.Size(47, 23);
            this.greenGreenTextBox.TabIndex = 0;
            this.greenGreenTextBox.Text = "1.0";
            // 
            // greenBlueTextBox
            // 
            this.greenBlueTextBox.Location = new System.Drawing.Point(118, 41);
            this.greenBlueTextBox.Name = "greenBlueTextBox";
            this.greenBlueTextBox.Size = new System.Drawing.Size(47, 23);
            this.greenBlueTextBox.TabIndex = 0;
            this.greenBlueTextBox.Text = "0.0";
            // 
            // blueRedTextBox
            // 
            this.blueRedTextBox.Location = new System.Drawing.Point(12, 70);
            this.blueRedTextBox.Name = "blueRedTextBox";
            this.blueRedTextBox.Size = new System.Drawing.Size(47, 23);
            this.blueRedTextBox.TabIndex = 0;
            this.blueRedTextBox.Text = "0.0";
            // 
            // blueGreenTextBox
            // 
            this.blueGreenTextBox.Location = new System.Drawing.Point(65, 70);
            this.blueGreenTextBox.Name = "blueGreenTextBox";
            this.blueGreenTextBox.Size = new System.Drawing.Size(47, 23);
            this.blueGreenTextBox.TabIndex = 0;
            this.blueGreenTextBox.Text = "0.0";
            // 
            // blueBlueTextBox
            // 
            this.blueBlueTextBox.Location = new System.Drawing.Point(118, 70);
            this.blueBlueTextBox.Name = "blueBlueTextBox";
            this.blueBlueTextBox.Size = new System.Drawing.Size(47, 23);
            this.blueBlueTextBox.TabIndex = 0;
            this.blueBlueTextBox.Text = "1.0";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(90, 99);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ColorMatrixConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 133);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.blueBlueTextBox);
            this.Controls.Add(this.greenBlueTextBox);
            this.Controls.Add(this.redBlueTextBox);
            this.Controls.Add(this.blueGreenTextBox);
            this.Controls.Add(this.greenGreenTextBox);
            this.Controls.Add(this.redGreenTextBox);
            this.Controls.Add(this.blueRedTextBox);
            this.Controls.Add(this.greenRedTextBox);
            this.Controls.Add(this.redRedTextBox);
            this.Name = "ColorMatrixConfigurator";
            this.Text = "ColorMatrixConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox redRedTextBox;
        private TextBox redGreenTextBox;
        private TextBox redBlueTextBox;
        private TextBox greenRedTextBox;
        private TextBox greenGreenTextBox;
        private TextBox greenBlueTextBox;
        private TextBox blueRedTextBox;
        private TextBox blueGreenTextBox;
        private TextBox blueBlueTextBox;
        private Button okButton;
    }
}