namespace NET6ImageFilter.Dialogs
{
    partial class ProcessingDialog
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
            this.loadingImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingImage
            // 
            this.loadingImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingImage.Image = global::NET6ImageFilter.Properties.Resources._1494;
            this.loadingImage.InitialImage = null;
            this.loadingImage.Location = new System.Drawing.Point(0, 0);
            this.loadingImage.Name = "loadingImage";
            this.loadingImage.Size = new System.Drawing.Size(63, 48);
            this.loadingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingImage.TabIndex = 0;
            this.loadingImage.TabStop = false;
            // 
            // ProcessingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(63, 48);
            this.Controls.Add(this.loadingImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProcessingDialog";
            this.Text = "Processing ...";
            ((System.ComponentModel.ISupportInitialize)(this.loadingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox loadingImage;
    }
}