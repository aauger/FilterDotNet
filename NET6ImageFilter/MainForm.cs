using AAImageFilter.Filters;
using AAImageFilter.Interfaces;
using NET6ImageFilter.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NET6ImageFilter
{
    public partial class MainForm : Form
    {
        public event EventHandler? ImageChanged;
        private Image _image = new Bitmap(1, 1);
        private List<IFilter> _filters = new List<IFilter>();

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

        public MainForm(List<IFilter> filters)
        {
            this._filters = filters;
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image = Image.FromFile(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening image");
                }
            }
        }

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            IFilter? filter = null;

            using FilterDialog dialog = new(_filters);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.SelectedFilter != null)
                { 
                    filter = dialog.SelectedFilter;
                }
            }

            if (filter == null)
                return;

            //if our selected filter is configurable, initialize it.
            if (filter is IConfigurableFilter icf)
            {
                icf.Initialize();
            }

            Image = filter.Apply(Image);
            //this ought to be invoked by the data binding, but it isn't.
            imageViewer.Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            imageViewer.DataBindings.Add("Image", this, "Image", true);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { 
                Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}