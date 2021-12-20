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
    public partial class AnalyzerResultDialog : Form
    {
        private readonly string _resultText;

        public AnalyzerResultDialog(string resultText)
        {
            InitializeComponent();
            _resultText = resultText;
            this.resultTextBox.Text = _resultText;
        }
    }
}
