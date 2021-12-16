namespace NET6ImageFilter.SpecificConfigurators
{
    partial class WinformsStatisticalConfigurator
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
            this.modeGroupBox = new System.Windows.Forms.GroupBox();
            this.modeRadioButton = new System.Windows.Forms.RadioButton();
            this.medianRadioButton = new System.Windows.Forms.RadioButton();
            this.maxRadioButton = new System.Windows.Forms.RadioButton();
            this.minRadioButton = new System.Windows.Forms.RadioButton();
            this.averageRadioButton = new System.Windows.Forms.RadioButton();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.blockSizeTextBox = new System.Windows.Forms.TextBox();
            this.blockSizeLabel = new System.Windows.Forms.Label();
            this.thresholdTextBox = new System.Windows.Forms.TextBox();
            this.thresholdCheckbox = new System.Windows.Forms.CheckBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.modeGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // modeGroupBox
            // 
            this.modeGroupBox.Controls.Add(this.modeRadioButton);
            this.modeGroupBox.Controls.Add(this.medianRadioButton);
            this.modeGroupBox.Controls.Add(this.maxRadioButton);
            this.modeGroupBox.Controls.Add(this.minRadioButton);
            this.modeGroupBox.Controls.Add(this.averageRadioButton);
            this.modeGroupBox.Location = new System.Drawing.Point(12, 12);
            this.modeGroupBox.Name = "modeGroupBox";
            this.modeGroupBox.Size = new System.Drawing.Size(161, 111);
            this.modeGroupBox.TabIndex = 0;
            this.modeGroupBox.TabStop = false;
            this.modeGroupBox.Text = "Mode";
            // 
            // modeRadioButton
            // 
            this.modeRadioButton.AutoSize = true;
            this.modeRadioButton.Location = new System.Drawing.Point(90, 49);
            this.modeRadioButton.Name = "modeRadioButton";
            this.modeRadioButton.Size = new System.Drawing.Size(56, 19);
            this.modeRadioButton.TabIndex = 0;
            this.modeRadioButton.TabStop = true;
            this.modeRadioButton.Text = "Mode";
            this.modeRadioButton.UseVisualStyleBackColor = true;
            // 
            // medianRadioButton
            // 
            this.medianRadioButton.AutoSize = true;
            this.medianRadioButton.Location = new System.Drawing.Point(90, 24);
            this.medianRadioButton.Name = "medianRadioButton";
            this.medianRadioButton.Size = new System.Drawing.Size(65, 19);
            this.medianRadioButton.TabIndex = 0;
            this.medianRadioButton.TabStop = true;
            this.medianRadioButton.Text = "Median";
            this.medianRadioButton.UseVisualStyleBackColor = true;
            // 
            // maxRadioButton
            // 
            this.maxRadioButton.AutoSize = true;
            this.maxRadioButton.Location = new System.Drawing.Point(16, 74);
            this.maxRadioButton.Name = "maxRadioButton";
            this.maxRadioButton.Size = new System.Drawing.Size(48, 19);
            this.maxRadioButton.TabIndex = 0;
            this.maxRadioButton.TabStop = true;
            this.maxRadioButton.Text = "Max";
            this.maxRadioButton.UseVisualStyleBackColor = true;
            // 
            // minRadioButton
            // 
            this.minRadioButton.AutoSize = true;
            this.minRadioButton.Location = new System.Drawing.Point(16, 49);
            this.minRadioButton.Name = "minRadioButton";
            this.minRadioButton.Size = new System.Drawing.Size(46, 19);
            this.minRadioButton.TabIndex = 0;
            this.minRadioButton.TabStop = true;
            this.minRadioButton.Text = "Min";
            this.minRadioButton.UseVisualStyleBackColor = true;
            // 
            // averageRadioButton
            // 
            this.averageRadioButton.AutoSize = true;
            this.averageRadioButton.Location = new System.Drawing.Point(16, 24);
            this.averageRadioButton.Name = "averageRadioButton";
            this.averageRadioButton.Size = new System.Drawing.Size(68, 19);
            this.averageRadioButton.TabIndex = 0;
            this.averageRadioButton.TabStop = true;
            this.averageRadioButton.Text = "Average";
            this.averageRadioButton.UseVisualStyleBackColor = true;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.blockSizeTextBox);
            this.optionsGroupBox.Controls.Add(this.blockSizeLabel);
            this.optionsGroupBox.Controls.Add(this.thresholdTextBox);
            this.optionsGroupBox.Controls.Add(this.thresholdCheckbox);
            this.optionsGroupBox.Location = new System.Drawing.Point(179, 12);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(200, 82);
            this.optionsGroupBox.TabIndex = 1;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // blockSizeTextBox
            // 
            this.blockSizeTextBox.Location = new System.Drawing.Point(90, 50);
            this.blockSizeTextBox.Name = "blockSizeTextBox";
            this.blockSizeTextBox.Size = new System.Drawing.Size(100, 23);
            this.blockSizeTextBox.TabIndex = 3;
            // 
            // blockSizeLabel
            // 
            this.blockSizeLabel.AutoSize = true;
            this.blockSizeLabel.Location = new System.Drawing.Point(25, 53);
            this.blockSizeLabel.Name = "blockSizeLabel";
            this.blockSizeLabel.Size = new System.Drawing.Size(59, 15);
            this.blockSizeLabel.TabIndex = 2;
            this.blockSizeLabel.Text = "Block Size";
            // 
            // thresholdTextBox
            // 
            this.thresholdTextBox.Location = new System.Drawing.Point(90, 20);
            this.thresholdTextBox.Name = "thresholdTextBox";
            this.thresholdTextBox.Size = new System.Drawing.Size(100, 23);
            this.thresholdTextBox.TabIndex = 1;
            this.thresholdTextBox.Text = "0";
            // 
            // thresholdCheckbox
            // 
            this.thresholdCheckbox.AutoSize = true;
            this.thresholdCheckbox.Location = new System.Drawing.Point(6, 22);
            this.thresholdCheckbox.Name = "thresholdCheckbox";
            this.thresholdCheckbox.Size = new System.Drawing.Size(78, 19);
            this.thresholdCheckbox.TabIndex = 0;
            this.thresholdCheckbox.Text = "Threshold";
            this.thresholdCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(179, 100);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(200, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // WinformsStatisticalConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 135);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.modeGroupBox);
            this.Name = "WinformsStatisticalConfigurator";
            this.Text = "WinformsStatisticalConfigurator";
            this.modeGroupBox.ResumeLayout(false);
            this.modeGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox modeGroupBox;
        private RadioButton modeRadioButton;
        private RadioButton medianRadioButton;
        private RadioButton maxRadioButton;
        private RadioButton minRadioButton;
        private RadioButton averageRadioButton;
        private GroupBox optionsGroupBox;
        private TextBox thresholdTextBox;
        private CheckBox thresholdCheckbox;
        private Button applyButton;
        private TextBox blockSizeTextBox;
        private Label blockSizeLabel;
    }
}