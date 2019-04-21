using Newtonsoft.Json;
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
        private List<PictureBox> pictureBoxes;

        public Form1()
        {
            InitializeComponent();
           pictureBoxes = new List<PictureBox>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            
        }

        private void c_PingDown(object sender, int e)
        {
            if (e == 1)
            {
                //pictureBox1.BackColor = Color.Red;
                //SoundPlayer my_wave_file = new SoundPlayer("AMHSDOWN-01.wav");
                //my_wave_file.PlaySync(); // PlaySync means that once sound start then no other activity if form will occur untill sound goes to finish
            }
        }

        private void c_PingUp(object sender, int e)
        {
            if (e==1)
            {
                //pictureBox1.BackColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get IPadress and pass to ping function
            List<IPAddress> IPList = listNode.Select(o => IPAddress.Parse(o.IPAddress)).ToList();
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

            drawListNode();
        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            var pictureBox = ((PictureBox)sender);
            e.Graphics.DrawString(pictureBox.Name, new Font("Arial", 8), Brushes.White, 5, pictureBox.Size.Height / 3);

            float opacityvalue = float.Parse("80") / 100;
            pictureBox.Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 

        }

        private void saveConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get position of pictureBox to save
            for (int i = 0; i < listNode.Count; i++)
            {
                listNode.ElementAt(i).Location = pictureBoxes.ElementAt(i).Location;
            }

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"config.json", JsonConvert.SerializeObject(listNode));
        }

        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listNode = JsonConvert.DeserializeObject<List<Node>>(File.ReadAllText(@"config.json"));
            
            drawListNode();
        }

        private void clearAllPictureBox()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is PictureBox)
                    Controls.Remove(    (control as PictureBox));
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void drawListNode()
        {
            pictureBoxes.Clear();
            clearAllPictureBox();
            foreach (var item in listNode)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.BackColor = Color.Transparent;

                if (item.Location.Equals(new Point(0,0)))
                {
                    pictureBox.Location = new System.Drawing.Point(103, 82); 
                }
                else
                {
                    pictureBox.Location = item.Location;
                }
                pictureBox.Name = item.Name;
                pictureBox.Size = new System.Drawing.Size(50, 50);
                pictureBox.TabIndex = 0;
                pictureBox.TabStop = false;

                //event draw pictureBox
                pictureBox.Paint += new PaintEventHandler(this.picBox_Paint) ;

                this.Controls.Add(pictureBox);
                ControlMover.Init(pictureBox);
                pictureBoxes.Add(pictureBox);
            }
        }

      
    }
}
