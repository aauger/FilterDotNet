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
    public partial class AnalyzerDialog : Form
    {
        private List<IAnalyzer> _analyzers;
        public IAnalyzer? SelectedAnalyzer { get; set; }

        public AnalyzerDialog(List<IAnalyzer> analyzers)
        {
            _analyzers = analyzers;
            InitializeComponent();
            analyzerListBox.Items.AddRange(_analyzers.Select(a => a.Name).ToArray());
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if(this.analyzerListBox.SelectedIndex != -1)
                this.SelectedAnalyzer = _analyzers[analyzerListBox.SelectedIndex];
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}
