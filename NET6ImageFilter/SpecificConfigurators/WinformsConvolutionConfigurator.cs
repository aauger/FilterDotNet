using AAImageFilter.Filters;
using AAImageFilter.Interfaces;
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
    public partial class WinformsConvolutionConfigurator : Form, IPluginConfigurator<ConvolutionConfiguration>
    {
        public WinformsConvolutionConfigurator()
        {
            InitializeComponent();
        }

        public ConvolutionConfiguration GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                string[] subs = this.matrixTextbox.Text.Split('\n');
                int lenX = subs.Length;
                int lenY = subs[0].Split(',').Length;

                double[,] values = new double[lenX,lenY];

                for (int i = 0; i < lenX; i++)
                {
                    for (int j = 0; j < lenY; j++)
                    {
                        values[i, j] = Double.Parse(subs[i].Split(',')[j]);
                    }
                }

                return new()
                {
                    Bias = double.Parse(this.biasTextbox.Text),
                    Normalize = this.normalizeCheckbox.Checked,
                    Values = values
                };
            }
            return new()
            {
                Bias = 1.0,
                Normalize = true,
                Values = new double[,] { }
            };
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.matrixTextbox.Text = File.ReadAllText(ofd.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, this.matrixTextbox.Text);
            }
        }
    }
}
