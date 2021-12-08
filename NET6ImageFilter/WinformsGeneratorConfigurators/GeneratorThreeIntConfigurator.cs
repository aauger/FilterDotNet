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

namespace NET6ImageFilter.WinformsGeneratorConfigurators
{
    public partial class GeneratorThreeIntConfigurator : Form, IGeneratorConfigurator<(int,int,int)>
    {
        public GeneratorThreeIntConfigurator()
        {
            InitializeComponent();
        }

        public (int, int, int) GetGeneratorConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return
                (
                    int.Parse(textBox1.Text),
                    int.Parse(textBox2.Text),
                    int.Parse(textBox3.Text)
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
