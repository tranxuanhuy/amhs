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
    public partial class NodeInfo : Form
    {
        private Node node;
        private Node ReturnValue1;

        public Node ReturnValue11 { get => ReturnValue1; set => ReturnValue1 = value; }

        public NodeInfo()
        {
            InitializeComponent();
        }

        public NodeInfo(Node node)
        {
            InitializeComponent();
            this.node = node;
            Text = node.Name;
        }

        private void NodeInfo_Load(object sender, EventArgs e)
        {
            ObjectToForm();
        }

        private void ObjectToForm()
        {
            textBox1.Text = node.Name;
            textBox2.Text = node.Remark;
            textBox3.Text = node.Location.ToString();
            textBox4.Text = node.IPAddress;
            textBox5.Text = node.Timeout.ToString();
            textBox6.Text = node.AddressName;
            textBox7.Text = node.AlarmFilename;
            textBox8.Text = node.RemoteConnectUser;
            textBox9.Text = node.RemoteConnectPass;
            textBox10.Text = node.ID1.ToString() ;
            checkBox1.Checked = node.AlarmEnable;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FormToObject();
            this.ReturnValue11 = node;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormToObject()
        {
         node.Name = textBox1.Text ;
         node.Remark              =       textBox2.Text ;
            //string[] coords = textBox3.Text.Split(',');
            //node.Location = new Point(int.Parse(coords[0]), int.Parse(coords[1])) ;
         node.IPAddress           =       textBox4.Text ;
         node.Timeout  =       int.Parse(textBox5.Text) ;
         node.AddressName         =       textBox6.Text ;
         node.AlarmFilename       =       textBox7.Text ;
         node.RemoteConnectUser   =       textBox8.Text ;
         node.RemoteConnectPass   =       textBox9.Text ;
            node.ID1 = int.Parse(textBox10.Text);
            node.AlarmEnable         =   checkBox1.Checked ;
        }
    }
}
