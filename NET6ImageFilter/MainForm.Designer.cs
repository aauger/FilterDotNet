namespace NET6ImageFilter
{
    partial class MainForm
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageViewer = new System.Windows.Forms.PictureBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.useGeneratorButton = new System.Windows.Forms.Button();
            this.copyToClipboardButton = new System.Windows.Forms.Button();
            this.fromClipboardButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageViewer)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageViewer
            // 
            this.imageViewer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewer.Location = new System.Drawing.Point(0, 36);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(800, 414);
            this.imageViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageViewer.TabIndex = 0;
            this.imageViewer.TabStop = false;
            // 
            // loadButton
            // 
            this.loadButton.Image = ((System.Drawing.Image)(resources.GetObject("loadButton.Image")));
            this.loadButton.Location = new System.Drawing.Point(3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(58, 30);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.Location = new System.Drawing.Point(67, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(57, 30);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("applyFilterButton.Image")));
            this.applyFilterButton.Location = new System.Drawing.Point(130, 3);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(59, 30);
            this.applyFilterButton.TabIndex = 1;
            this.applyFilterButton.Text = "Filter";
            this.applyFilterButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.applyFilterButton.UseVisualStyleBackColor = true;
            this.applyFilterButton.Click += new System.EventHandler(this.applyFilterButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.loadButton);
            this.flowLayoutPanel1.Controls.Add(this.saveButton);
            this.flowLayoutPanel1.Controls.Add(this.applyFilterButton);
            this.flowLayoutPanel1.Controls.Add(this.useGeneratorButton);
            this.flowLayoutPanel1.Controls.Add(this.copyToClipboardButton);
            this.flowLayoutPanel1.Controls.Add(this.fromClipboardButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 36);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // useGeneratorButton
            // 
            this.useGeneratorButton.Image = ((System.Drawing.Image)(resources.GetObject("useGeneratorButton.Image")));
            this.useGeneratorButton.Location = new System.Drawing.Point(195, 3);
            this.useGeneratorButton.Name = "useGeneratorButton";
            this.useGeneratorButton.Size = new System.Drawing.Size(78, 30);
            this.useGeneratorButton.TabIndex = 2;
            this.useGeneratorButton.Text = "Generate";
            this.useGeneratorButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.useGeneratorButton.UseVisualStyleBackColor = true;
            this.useGeneratorButton.Click += new System.EventHandler(this.useGeneratorButton_Click);
            // 
            // copyToClipboardButton
            // 
            this.copyToClipboardButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToClipboardButton.Image")));
            this.copyToClipboardButton.Location = new System.Drawing.Point(279, 3);
            this.copyToClipboardButton.Name = "copyToClipboardButton";
            this.copyToClipboardButton.Size = new System.Drawing.Size(59, 30);
            this.copyToClipboardButton.TabIndex = 3;
            this.copyToClipboardButton.Text = "Copy";
            this.copyToClipboardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.copyToClipboardButton.UseVisualStyleBackColor = true;
            this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
            // 
            // fromClipboardButton
            // 
            this.fromClipboardButton.Image = ((System.Drawing.Image)(resources.GetObject("fromClipboardButton.Image")));
            this.fromClipboardButton.Location = new System.Drawing.Point(344, 3);
            this.fromClipboardButton.Name = "fromClipboardButton";
            this.fromClipboardButton.Size = new System.Drawing.Size(61, 30);
            this.fromClipboardButton.TabIndex = 4;
            this.fromClipboardButton.Text = "Paste";
            this.fromClipboardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.fromClipboardButton.UseVisualStyleBackColor = true;
            this.fromClipboardButton.Click += new System.EventHandler(this.fromClipboardButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.imageViewer);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Filter Workshop";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageViewer)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox imageViewer;
        private Button loadButton;
        private Button saveButton;
        private Button applyFilterButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button useGeneratorButton;
        private Button copyToClipboardButton;
        private Button fromClipboardButton;
    }
}