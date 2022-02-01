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
    public partial class WinformIntConfigurator : Form, IPluginConfigurator<int>
    {
        public WinformIntConfigurator(string title, string message)
        {
            InitializeComponent();
            this.Text = message;
            this.promptLabel.Text = message;
        }

        public int GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                int value = default;
                bool parsed = int.TryParse(inputBox.Text, out value);

                if (parsed)
                    return value;
            }
            return default(int);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
