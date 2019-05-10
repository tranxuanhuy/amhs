using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace amhs
{
  
    public partial class UpdownTime : Form
    {
        private object MyList;

        public UpdownTime(List<Node> listNode)
        {
            InitializeComponent();

            //load list node to list view
            var MyList = new List<Role>();
foreach (var item in listNode)
            {
                MyList.Add(new Role() { Id = item.ID1, Name = item.Name });
            }
  

            this.listBox1.DataSource = MyList;

            this.listBox1.DisplayMember = "Name";

            this.listBox1.ValueMember = "Name";
        }

        private void UpdownTime_Load(object sender, EventArgs e)
        {
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            IEnumerable<string> relevantLines=null;
            textBox1.Clear();
            try
            {
                relevantLines = new ReadWriteFile().ReadLines(dateTimePicker1.Value.ToString("yyyy_MM_dd") + ".log")
                                                .Where(l => l.EndsWith(listBox1.SelectedValue.ToString()));
            }
            catch (Exception)
            {

                
            }

            if (relevantLines!=null)
            {
                if (relevantLines.Count() == 0)
                {
                    textBox1.Text = "No log";
                }
                else
                {
                    foreach (var item in relevantLines)
                    {
                        textBox1.AppendText(item);
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
            }
            else
            {
                textBox1.Text = "No log";

            }
        }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
