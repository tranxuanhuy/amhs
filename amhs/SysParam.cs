using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace amhs
{
    public partial class SysParam : Form
    {
        public SysParam()
        {
            InitializeComponent();
        }

        private void SysParam_Load(object sender, EventArgs e)
        {
            textBox1.Text= Properties.Settings.Default.PingTimeout.ToString();
            textBox2.Text = Properties.Settings.Default.PingDelay.ToString();
            textBox3.Text = Properties.Settings.Default.AlarmFileName;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PingTimeout = int.Parse(textBox1.Text);
            Properties.Settings.Default.PingDelay = int.Parse(textBox2.Text);
             Properties.Settings.Default.AlarmFileName= textBox3.Text;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
