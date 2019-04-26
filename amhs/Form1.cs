
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
        private float opacityvalue;
        private SoundPlayer my_wave_file;

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

            //display current time on form
            timer1.Enabled = true;
            timer1.Interval = 1000;
            opacityvalue = float.Parse("80") / 100;

            //load Config
            LoadConfig();
            //ping start
            networkHeartbeatStart();

        }

        private void c_PingDown(object sender, int e)
        {
            pictureBoxes[e].Image.Dispose();
            pictureBoxes[e].Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 
            pictureBoxes[e].Image.Tag = "red";
            //pictureBoxes[e].Image = Image.FromFile("redblink.gif");   //calling ChangeOpacity Function 

            //neu enable sound alarm thi moi keu
            if (listNode[e].AlarmEnable)
            {
                var alarmFilePath = !String.IsNullOrEmpty(listNode[e].AlarmFilename) ? Path.Combine(Directory.GetCurrentDirectory(), "soundAlarm", listNode[e].AlarmFilename) : Path.Combine(Directory.GetCurrentDirectory(), "soundAlarm", Properties.Settings.Default.AlarmFileName);
                my_wave_file = new SoundPlayer(alarmFilePath);
                my_wave_file.PlayLooping();
            }
            

        }

        private void c_PingUp(object sender, int e)
        {
            pictureBoxes[e].Image.Dispose();
            pictureBoxes[e].Image = ImageTransparency.ChangeOpacity(Image.FromFile("green.png"), opacityvalue);  //calling ChangeOpacity Function 
            pictureBoxes[e].Image.Tag = "green";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get IPadress and pass to ping function
            List<IPAddress> IPList = listNode.Select(o => IPAddress.Parse(o.IPAddress)).ToList();
            networkHeartbeat = new NetworkHeartbeat(IPList, 1000, 5000,null);
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
            if (Properties.Settings.Default.PictureBoxLabelShow)
            {
                e.Graphics.DrawString(pictureBox.Name, new Font("Arial", 8), Brushes.White, 0, pictureBox.Size.Height / 3); 
            }

            
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
            LoadConfig();
        }

        private void LoadConfig()
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
                CreatePicturebox(item);
            }
        }

        private void CreatePicturebox(Node item)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.Transparent;

            if (item.Location.Equals(new Point(0, 0)))
            {
                pictureBox.Location = new System.Drawing.Point(103, 82);
            }
            else
            {
                pictureBox.Location = item.Location;
            }
            pictureBox.Name = item.Name;
            pictureBox.Size = new System.Drawing.Size(Properties.Settings.Default.NodeSize, Properties.Settings.Default.NodeSize);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            

            //event draw pictureBox
            pictureBox.Paint += picBox_Paint;
            //event click pictureBox
            pictureBox.MouseDoubleClick += picBox_MouseClick;

            pictureBox.Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 
            pictureBox.Image.Tag = "red";

            this.Controls.Add(pictureBox);
            //check enable node move
            if (Properties.Settings.Default.MovingNodeEnable)
            {
                ControlMover.Init(pictureBox); 
            }
            pictureBoxes.Add(pictureBox);
        }

        private void picBox_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = ((PictureBox)sender);
            Node node = listNode.Find(o => o.Name.Equals(pictureBox.Name));
            NodeInfo nodeInfo = new NodeInfo(node);
            // Show nodeInfo as a modal dialog and determine if DialogResult = OK.
            var nodeInfoStatusReturn = nodeInfo.ShowDialog(this);
            if (nodeInfoStatusReturn == DialogResult.OK)
            {
                // Read the contents of nodeInfo's TextBox.
                node = nodeInfo.ReturnValue11;
                //update picturebox name if changed
                pictureBox.Name = node.Name;
                pictureBox.Image.Dispose();
                pictureBox.Image = ImageTransparency.ChangeOpacity(Image.FromFile("red.png"), opacityvalue);  //calling ChangeOpacity Function 
                pictureBox.Image.Tag = "red";
                pictureBox.Refresh();
                networkHeartbeatRestart();
            }
            //node delete
            else if (nodeInfoStatusReturn == DialogResult.Abort)
            {
                listNode.Remove(node);
                pictureBox.Image.Dispose();
                this.Controls.Remove(pictureBox);
                networkHeartbeatRestart();
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
                //reload list node
                drawListNode();

                if (networkHeartbeat!=null )
                {
                    if (networkHeartbeat.Running)
                    {
                        //restart ping process
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
            networkHeartbeat = new NetworkHeartbeat(IPList, Properties.Settings.Default.PingTimeout, Properties.Settings.Default.PingDelay, listNode);
            networkHeartbeat.PingUp += c_PingUp;
            networkHeartbeat.PingDown += c_PingDown;

            GC.Collect();
            GC.WaitForPendingFinalizers();

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //update clock
            label1.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");

            //update progress bar
            if (networkHeartbeat != null)
            {
                if (networkHeartbeat.Running)
                {
                    if (progressBar1.Value == 100)
                    {
                        progressBar1.Value = 0;
                    }
                  
                        progressBar1.Value += 10;
                    
                } 
            }

            //down node blink image
            if (Properties.Settings.Default.FlashDownEnable)
            {
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    if (Controls[i] is PictureBox)
                    {
                        var myImageBox = (PictureBox)Controls[i];
                        string tag = (string)((PictureBox)Controls[i]).Image.Tag;
                        if (tag.Equals("red"))
                        {
                            myImageBox.Visible = !myImageBox.Visible;
                        }
                    }

                } 
            }
        }

        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node;
            NodeInfo nodeInfo = new NodeInfo(listNode.Count);
            // Show nodeInfo as a modal dialog and determine if DialogResult = OK.
            var nodeInfoStatusReturn = nodeInfo.ShowDialog(this);
            if (nodeInfoStatusReturn == DialogResult.OK)
            {
                // Read the contents of nodeInfo's TextBox.
                node = nodeInfo.ReturnValue11;
         
                //add node to form and list
                listNode.Add(node);
                CreatePicturebox(node);
            }
            nodeInfo.Dispose();
        }

        private void DownAcknowledgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (my_wave_file!=null)
            {
                my_wave_file.Stop();
                my_wave_file.Dispose();
            }
        }

        private void UpdownTimeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdownTime updownTime = new UpdownTime(listNode);
            updownTime.Show();
        }
    }
}
