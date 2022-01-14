using FilterDotNet.Filters;
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

namespace NET6ImageFilter.SpecificConfigurators
{
    public partial class WinformsClippingRectConfigurator : Form, IPluginConfigurator<(ClippingRectangle, IFilter)>
    {
        private List<IFilter> _filters;

        public WinformsClippingRectConfigurator(List<IFilter> filters)
        {
            this._filters = filters;
            InitializeComponent();
            this.Shown += WinformsClippingRectConfigurator_Shown;
        }

        private void WinformsClippingRectConfigurator_Shown(object? sender, EventArgs e)
        {
            this.filterListBox.Items.Clear();
            this.filterListBox.Items.AddRange(this._filters.Select(f => f.Name).ToArray());
        }

        public (ClippingRectangle, IFilter) GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            { 
                IFilter filter = this._filters[this.filterListBox.SelectedIndex];
                if (filter != null)
                {
                    return
                    (
                        new ClippingRectangle
                        {
                            X = int.Parse(xPosTextBox.Text),
                            Y = int.Parse(yPosTextBox.Text),
                            Width = int.Parse(widthTextBox.Text),
                            Height = int.Parse(heightTextBox.Text)
                        },
                        filter
                    );
                }            
            }

            return (default, default);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
