
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
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;


        }

        private void c_PingDown(object sender, int e)
        {
            float opacityvalue = float.Parse("80") / 100;
            pictureBoxes[e].Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 

          var alarmFilePath=!String.IsNullOrEmpty(listNode[e].AlarmFilename)? Path.Combine(Directory.GetCurrentDirectory(), "soundAlarm", listNode[e].AlarmFilename) :  Path.Combine(Directory.GetCurrentDirectory(), "soundAlarm", Properties.Settings.Default.AlarmFileName);
            SoundPlayer my_wave_file = new SoundPlayer(alarmFilePath);
            my_wave_file.Play();
        }

        private void c_PingUp(object sender, int e)
        {

            float opacityvalue = float.Parse("80") / 100;
            pictureBoxes[e].Image = ImageTransparency.ChangeOpacity(Image.FromFile("green.png"), opacityvalue);  //calling ChangeOpacity Function 


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
                                        .Select(v => Node.FromTxt(v))
                                        .ToList();

            drawListNode();
        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            var pictureBox = ((PictureBox)sender);
            e.Graphics.DrawString(pictureBox.Name, new Font("Arial", 8), Brushes.White, 0, pictureBox.Size.Height / 3);

            
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
            
                for (int i = Controls.Count-1; i >=0 ; i--)
                {
                    if (Controls[i] is PictureBox)
                    {
                        this.Controls.Remove(Controls[i]);
                    }

                }
         
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
                pictureBox.Size = new System.Drawing.Size(60, 60);
                pictureBox.TabIndex = 0;
                pictureBox.TabStop = false;

                //event draw pictureBox
                pictureBox.Paint += picBox_Paint;
                //event click pictureBox
                pictureBox.MouseDoubleClick += picBox_MouseClick;

                float opacityvalue = float.Parse("80") / 100;
                pictureBox.Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 
                

                this.Controls.Add(pictureBox);
                ControlMover.Init(pictureBox);
                pictureBoxes.Add(pictureBox);
            }
        }

        private void picBox_MouseClick(object sender, MouseEventArgs e)
        {
            Node node = listNode.Find(o => o.Name.Equals(((PictureBox)sender).Name));
            NodeInfo nodeInfo = new NodeInfo(node);
            // Show nodeInfo as a modal dialog and determine if DialogResult = OK.
            if (nodeInfo.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of nodeInfo's TextBox.
                node = nodeInfo.ReturnValue11;
                //update picturebox name if changed
                ((PictureBox)sender).Name = node.Name;
            }
            else
            {
               
            }
            nodeInfo.Dispose();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            clearAllPictureBox();
        }

        private void SysParamToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SysParam sysParamForm = new SysParam();
            // Show nodeInfo as a modal dialog and determine if DialogResult = OK.
            if (sysParamForm.ShowDialog(this) == DialogResult.OK)
            {

                if (networkHeartbeat!=null )
                {
                    if (networkHeartbeat.Running)
                    {
                        networkHeartbeatRestart();  
                    }
                }
            }
            else
            {

            }
            sysParamForm.Dispose();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            networkHeartbeatStart();
        }

        private void networkHeartbeatStart()
        {
            //get IPadress and pass to ping function
            List<IPAddress> IPList = listNode.Select(o => IPAddress.Parse(o.IPAddress)).ToList();
            networkHeartbeat = new NetworkHeartbeat(IPList, Properties.Settings.Default.PingTimeout, Properties.Settings.Default.PingDelay);
            networkHeartbeat.PingUp += c_PingUp;
            networkHeartbeat.PingDown += c_PingDown;

            networkHeartbeat.Start();
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            networkHeartbeat.Stop();
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            networkHeartbeatRestart();

        }

        private void networkHeartbeatRestart()
        {
            networkHeartbeat.Stop();
            networkHeartbeatStart();
        }
    }
}
