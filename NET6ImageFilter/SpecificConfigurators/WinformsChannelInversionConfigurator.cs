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
    public partial class WinformsChannelInversionConfigurator : Form, IPluginConfigurator<InversionChannel>
    {
        public WinformsChannelInversionConfigurator()
        {
            InitializeComponent();
        }

        public InversionChannel GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return (redRadioButton.Checked, greenRadioButton.Checked, blueRadioButton.Checked) switch
                {
                    (true, _, _) => InversionChannel.R,
                    (_, true, _) => InversionChannel.G,
                    (_, _, true) => InversionChannel.B,
                    _ => throw new NotImplementedException(),
                };
            }
            return InversionChannel.None;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
