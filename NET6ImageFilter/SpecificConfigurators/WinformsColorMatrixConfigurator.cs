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
    public partial class WinformsColorMatrixConfigurator : Form, IPluginConfigurator<float[,]>
    {
        readonly float[,] IDENTITY = new float[,] 
        {
            { 1.0F, 0.0F, 0.0F },
            { 0.0F, 1.0F, 0.0F },
            { 0.0F, 0.0F, 1.0F },
        };

        public WinformsColorMatrixConfigurator()
        {
            InitializeComponent();
        }

        public float[,] GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                float[,] config = new float[3, 3];
                // Red composition
                config[0, 0] = float.Parse(redRedTextBox.Text);
                config[0, 1] = float.Parse(redGreenTextBox.Text);
                config[0, 2] = float.Parse(redBlueTextBox.Text);
                // Green composition
                config[1, 0] = float.Parse(greenRedTextBox.Text);
                config[1, 1] = float.Parse(greenGreenTextBox.Text);
                config[1, 2] = float.Parse(greenBlueTextBox.Text);
                // Blue composition
                config[2, 0] = float.Parse(blueRedTextBox.Text);
                config[2, 1] = float.Parse(blueGreenTextBox.Text);
                config[2, 2] = float.Parse(blueBlueTextBox.Text);

                return config;
            }
            return IDENTITY;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
