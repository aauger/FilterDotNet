using AAImageFilter.Filters;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NET6ImageFilter
{
    public partial class MainForm : Form
    {
        public event EventHandler ImageChanged;
        private Image _image = new Bitmap(1, 1);
        public Image Image
        {
            get { return _image; }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    ImageChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            { 
                Image = Image.FromFile(ofd.FileName);
            }
        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            Threshold thresh = (Threshold)new Threshold().Initialize(new WinformIntConfigurator("Threshold value:").GetPluginConfiguration());
            Image = thresh.Apply(Image);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            imageViewer.DataBindings.Add("Image", this, "Image", true);
        }
    }
}