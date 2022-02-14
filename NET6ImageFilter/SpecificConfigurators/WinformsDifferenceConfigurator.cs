using FilterDotNet.Interfaces;
using FastImageLibrary;
using NET6ImageFilter.ImageProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastImageProvider;

namespace NET6ImageFilter.SpecificConfigurators
{
    public partial class WinformsDifferenceConfigurator : Form, IPluginConfigurator<(IImage,double)>
    {
        public WinformsDifferenceConfigurator()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = ofd.FileName;
            }
        }

        public (IImage, double) GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            { 
                return (new FIDrawingImage(new FastImage(Image.FromFile(this.filePathTextBox.Text))),
                    double.Parse(multiplierTextBox.Text));
            }
            return (new FIDrawingImage(1, 1), 1.0);
        }
    }
}
