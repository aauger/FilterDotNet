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
    public partial class WinformsTwoIntConfigurator : Form, IPluginConfigurator<(int, int)>
    {
        public WinformsTwoIntConfigurator(string title, string firstMessage, string secondMessage)
        {
            InitializeComponent();
            this.Text = title;
            this.promptLabelFirst.Text = firstMessage;
            this.promptLabelSecond.Text = secondMessage;
        }

        public (int, int) GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                int firstValue = default;
                int secondValue = default;
                bool parsed = int.TryParse(inputBoxFirst.Text, out firstValue) 
                    && int.TryParse(inputBoxSecond.Text, out secondValue);

                if (parsed)
                    return (firstValue, secondValue);
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
