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
    public partial class LineDrawingConfigurator : Form, IPluginConfigurator<LineDrawingConfiguration>
    {
        public LineDrawingConfigurator()
        {
            InitializeComponent();
        }

        public LineDrawingConfiguration GetPluginConfiguration()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return new()
                {
                    ColorDistance                   = int.Parse(colorDistanceTextBox.Text),
                    Count                           = int.Parse(pointCountTextBox.Text),
                    PixelDistanceHorizontal         = int.Parse(pixelDistanceHorizontalTextBox.Text),
                    PixelDistanceVertical           = int.Parse(pixelDistanceVerticalTextBox.Text),
                    SegmentCount                    = int.Parse(segmentCountTextBox.Text),
                    SegmentRandomDistanceHorizontal = int.Parse(segmentRandomDistanceHorizontalTextBox.Text),
                    SegmentRandomDistanceVertical   = int.Parse(segmentRandomDistanceVerticalTextBox.Text),
                };
            }
            return new();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
