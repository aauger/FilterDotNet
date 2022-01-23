using FilterDotNet.Filters;
using FilterDotNet.Interfaces;
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
        private List<IAnalyzer> _analyzers = new();

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

        public MainForm(List<IFilter> filters, List<IGenerator> generators, List<IAnalyzer> analyzers)
        {
            this._filters = filters;
            this._generators = generators;
            this._analyzers = analyzers;
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
                try
                {
                    icf.Initialize();
                }
                catch
                {
                    MessageBox.Show("An error occurred during plugin initialization");
                }
            }

            FIDrawingImage di = new FIDrawingImage(Image);

            //show processing wait dialog
            using ProcessingDialog pd = new();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = (filter.Apply(di));

                    if (result is FIDrawingImage fdi)
                    {
                        Image = fdi.UnwrapFastImage().ToBitmap();
                    }

                    if (result is GDIDrawingImage gdi)
                    {
                        Image = gdi.UnwrapBitmap();
                    }
                    pd.CloseForm();
                }
                catch(Exception ex)
                {
                    pd.CloseForm();
                    throw ex;
                    MessageBox.Show("There was an error applying the filter.");
                }
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
                try
                {
                    icg.Initialize();
                }
                catch
                {
                    MessageBox.Show("There was an error during initialization");
                }
            }

            //show processing wait dialog
            using ProcessingDialog pd = new();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = generator.Generate();

                    if (result is GDIDrawingImage gdi)
                    {
                        Image = gdi.UnwrapBitmap();
                    }

                    if (result is FIDrawingImage fdi)
                    {
                        Image = fdi.UnwrapFastImage().ToBitmap();
                    }
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

        private void analyzeButton_Click(object sender, EventArgs e)
        {
            IAnalyzer? analyzer = null;

            using AnalyzerDialog dialog = new AnalyzerDialog(_analyzers);


            if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedAnalyzer is not null)
                analyzer = dialog.SelectedAnalyzer;

            if (analyzer is null)
                return;

            if (analyzer is IConfigurableAnalyzer ica)
            {
                try
                {
                    ica.Initialize();
                }
                catch
                {
                    MessageBox.Show("There was an error during initialization");
                }
            }

            FIDrawingImage di = new(Image);

            using ProcessingDialog pd = new();
            Task.Factory.StartNew(() => 
            {
                try
                {
                    var result = analyzer.Analyze(di);
                    Task.Factory.StartNew(() =>
                    {
                        var resultDialog = new AnalyzerResultDialog(result);
                        resultDialog.ShowDialog();
                    });
                }
                catch
                { 
                    MessageBox.Show("There was an error running the analyzer.");
                }
                pd.CloseForm();
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