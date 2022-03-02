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

namespace NET6ImageFilter.BasicWinformsConfigurators
{
    public partial class WinformsThreeIntConfigurator : Form, IPluginConfigurator<(int,int,int)>
    {
        public WinformsThreeIntConfigurator(string title, string firstMessage, string secondMessage, string thirdMessage)
        {
            InitializeComponent();
            this.Text = title;
            this.promptLabelFirst.Text = firstMessage;
            this.promptLabelSecond.Text = secondMessage;
            this.promptLabelThird.Text = thirdMessage;
        }

        public (int, int, int) GetPluginConfiguration()
        {

            if (this.ShowDialog() == DialogResult.OK)
            {
                int firstValue  = default;
                int secondValue = default;
                int thirdValue  = default;
                bool parsed = int.TryParse(inputBoxFirst.Text, out firstValue) 
                    && int.TryParse(inputBoxSecond.Text, out secondValue) 
                    && int.TryParse(inputBoxThird.Text, out thirdValue);

                if (parsed)
                    return (firstValue, secondValue, thirdValue);
            }
            return (default, default, default);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
