using FilterDotNet.Interfaces;
using FastImageLibrary;
using FastImageProvider;

namespace NET6ImageFilter.SpecificConfigurators
{
    public partial class WinformsNormalMapConfigurator : Form, IPluginConfigurator<(IImage,double)>
    {
        public WinformsNormalMapConfigurator()
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
