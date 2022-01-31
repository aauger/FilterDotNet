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
    public partial class WinformThreeIntConfigurator : Form, IPluginConfigurator<(int,int,int)>
    {
        public WinformThreeIntConfigurator(string title, string firstMessage, string secondMessage, string thirdMessage)
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
               return 
               (
                int.Parse(inputBoxFirst.Text),
                int.Parse(inputBoxSecond.Text),
                int.Parse(inputBoxThird.Text)
               );
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
