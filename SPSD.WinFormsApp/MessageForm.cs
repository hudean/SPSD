using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPSD.WinFormsApp
{
    public partial class MessageForm : Form
    {
        public MessageForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCpu.Text = "4";
            txtMemory.Text = "2048";
        }

        public int CpuCount  = 0;

        public int MemoryCount  = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCpu.Text, out int cpuCount))
            {
                MessageBox.Show("只能输入数字");
                return;
            }

            if (!int.TryParse(txtMemory.Text, out int memoryCount))
            {
                MessageBox.Show("只能输入数字");
                return;
            }

            CpuCount = cpuCount;
            MemoryCount = memoryCount;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
