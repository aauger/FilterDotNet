using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET6ImageFilter.Dialogs
{
    public partial class ProcessingDialog : Form
    {
        public ProcessingDialog()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.ControlBox = false;
            InitializeComponent();
        }

        public void CloseForm()
        {
            if (this.InvokeRequired)
                this.Invoke(() => this.Close());
            else
                this.Close();
        }
    }
}
