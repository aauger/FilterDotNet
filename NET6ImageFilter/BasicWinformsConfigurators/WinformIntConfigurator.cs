using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AAImageFilter.Interfaces;

namespace NET6ImageFilter
{
    public partial class WinformIntConfigurator : Form, IPluginConfigurator<int>
    {
        public WinformIntConfigurator(string message)
        {
            InitializeComponent();
            this.Text = message;
        }

        public int GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return int.Parse(inputBox.Text);
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
