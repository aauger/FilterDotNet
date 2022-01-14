using FilterDotNet;
using FilterDotNet.Interfaces;
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
    public partial class FilterDialog : Form
    {
        private List<IFilter> _filters;
        public IFilter? SelectedFilter { get; set; }

        public FilterDialog(List<IFilter> filters)
        {
            _filters = filters;
            InitializeComponent();
            filterListBox.Items.AddRange(_filters.Select(f => f.Name).ToArray());
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if(this.filterListBox.SelectedIndex != -1)
                this.SelectedFilter = _filters[filterListBox.SelectedIndex];
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
