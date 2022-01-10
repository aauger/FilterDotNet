using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilterDotNet.Interfaces;

namespace NET6ImageFilter.BasicWinformsConfigurators
{
    public partial class WinformTwoIntConfigurator : Form, IPluginConfigurator<(int, int)>
    {
        public WinformTwoIntConfigurator(string firstMessage, string secondMessage)
        {
            InitializeComponent();
            this.fLabel.Text = firstMessage;
            this.sLabel.Text = secondMessage;
        }

        public (int, int) GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            { 
                return (int.Parse(fInput.Text), int.Parse(sInput.Text));
            }
            return (default, default);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void WinformTwoIntConfigurator_Load(object sender, EventArgs e)
        {

        }

        private void fInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
