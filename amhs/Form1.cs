using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace amhs
{
    public partial class Form1 : Form
    {
        private NetworkHeartbeat networkHeartbeat;
        private List<Node> listNode;

        public Form1()
        {
            InitializeComponent();
            //ControlMover.Init(this.pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            
        }

        private void c_PingDown(object sender, int e)
        {
            if (e == 1)
            {
                pictureBox1.BackColor = Color.Red;
                //SoundPlayer my_wave_file = new SoundPlayer("AMHSDOWN-01.wav");
                //my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
            }
        }

        private void c_PingUp(object sender, int e)
        {
            if (e==1)
            {
                pictureBox1.BackColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get IPadress and pass to ping function
            List<IPAddress> IPList = listNode.Select(o => o.IPAddress).ToList();
            networkHeartbeat = new NetworkHeartbeat(IPList, 1000, 5000);
            networkHeartbeat.PingUp += c_PingUp;
            networkHeartbeat.PingDown += c_PingDown;

            networkHeartbeat.Start();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            networkHeartbeat.Stop();
        }

        private void loadBeginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listNode = File.ReadAllLines("nodeInputAtBeginning.txt")
                                        .Skip(1)
                                        .Select(v => Node.FromCsv(v))
                                        .ToList();

            List<PictureBox> pictureBoxes=new List<PictureBox>();
            foreach (var item in listNode)
            {
                PictureBox pictureBox = new PictureBox();
                // pictureBox1
                // 
               pictureBox.BackColor = System.Drawing.Color.Black;
               pictureBox.Location = new System.Drawing.Point(103, 82);
               pictureBox.Name = "pictureBox";
               pictureBox.Size = new System.Drawing.Size(100, 50);
               pictureBox.TabIndex = 0;
               pictureBox.TabStop = false;

                this.Controls.Add(pictureBox);
                ControlMover.Init(pictureBox);
                pictureBoxes.Add(pictureBox);
            }
        }
    }
}
