using AAImageFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET6ImageFilter.Dialogs
{
    public partial class GeneratorDialog : Form
    {
        private List<IGenerator> _generators;
        public IGenerator? SelectedGenerator;

        public GeneratorDialog(List<IGenerator> generators)
        {
            _generators = generators;
            InitializeComponent();
            generatorListBox.Items.AddRange(_generators.Select(g => g.GetName()).ToArray());
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.SelectedGenerator = _generators[generatorListBox.SelectedIndex];
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
