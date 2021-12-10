using AAImageFilter.Filters;
using AAImageFilter.Interfaces;
using NET6ImageFilter.Dialogs;
using NET6ImageFilter.ImageProviders;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NET6ImageFilter
{
    public partial class MainForm : Form
    {
        public event EventHandler? ImageChanged;
        private Image _image = new Bitmap(1, 1);
        private List<IFilter> _filters = new();
        private List<IGenerator> _generators = new();

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

        public MainForm(List<IFilter> filters, List<IGenerator> generators)
        {
            this._filters = filters;
            this._generators = generators;
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image = Image.FromFile(ofd.FileName);
                }
                catch (Exception)
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
                if (dialog.SelectedFilter is not null)
                { 
                    filter = dialog.SelectedFilter;
                }
            }

            if (filter is null)
                return;

            //if our selected filter is configurable, initialize it.
            if (filter is IConfigurableFilter icf)
            {
                icf.Initialize();
            }

            DrawingImage di = DrawingImage.EncapsulateBitmap((Bitmap)Image);

            //show processing wait dialog
            using ProcessingDialog pd = new();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Image = filter.Apply(Image);
                }
                catch
                {
                    MessageBox.Show("There was an error applying the filter.");
                }
                pd.CloseForm();
                imageViewer.Invalidate();
            });
            pd.ShowDialog();
        }

        private void useGeneratorButton_Click(object sender, EventArgs e)
        {
            IGenerator? generator = null;

            using GeneratorDialog dialog = new(_generators);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.SelectedGenerator is not null)
                {
                    generator = dialog.SelectedGenerator;
                }
            }

            if (generator is null)
                return;

            if (generator is IConfigurableGenerator icg)
            {
                icg.Initialize();
            }

            //show processing wait dialog
            using ProcessingDialog pd = new();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Image = generator.Generate();
                }
                catch 
                {
                    MessageBox.Show("There was an error running the generator.");
                }
                pd.CloseForm();
                imageViewer.Invalidate();
            });
            pd.ShowDialog();
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

        private void copyToClipboardButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(Image);
        }

        private void fromClipboardButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
                Image = Clipboard.GetImage();
        }
    }
}