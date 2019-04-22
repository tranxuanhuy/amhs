namespace amhs
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBeginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sysParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sysParamToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updownTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updownTimeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sysParamToolStripMenuItem,
            this.pingToolStripMenuItem,
            this.updownTimeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadBeginToolStripMenuItem,
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadBeginToolStripMenuItem
            // 
            this.loadBeginToolStripMenuItem.Name = "loadBeginToolStripMenuItem";
            this.loadBeginToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadBeginToolStripMenuItem.Text = "Load Begin";
            this.loadBeginToolStripMenuItem.Click += new System.EventHandler(this.loadBeginToolStripMenuItem_Click);
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.loadConfigToolStripMenuItem_Click);
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            this.saveConfigToolStripMenuItem.Click += new System.EventHandler(this.saveConfigToolStripMenuItem_Click);
            // 
            // sysParamToolStripMenuItem
            // 
            this.sysParamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sysParamToolStripMenuItem1});
            this.sysParamToolStripMenuItem.Name = "sysParamToolStripMenuItem";
            this.sysParamToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.sysParamToolStripMenuItem.Text = "Edit";
            // 
            // sysParamToolStripMenuItem1
            // 
            this.sysParamToolStripMenuItem1.Name = "sysParamToolStripMenuItem1";
            this.sysParamToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.sysParamToolStripMenuItem1.Text = "Sys Param";
            this.sysParamToolStripMenuItem1.Click += new System.EventHandler(this.SysParamToolStripMenuItem1_Click);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.pingToolStripMenuItem.Text = "Ping";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // updownTimeToolStripMenuItem
            // 
            this.updownTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updownTimeToolStripMenuItem1});
            this.updownTimeToolStripMenuItem.Name = "updownTimeToolStripMenuItem";
            this.updownTimeToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.updownTimeToolStripMenuItem.Text = "Log";
            // 
            // updownTimeToolStripMenuItem1
            // 
            this.updownTimeToolStripMenuItem1.Name = "updownTimeToolStripMenuItem1";
            this.updownTimeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.updownTimeToolStripMenuItem1.Text = "Updown time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadBeginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sysParamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sysParamToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updownTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updownTimeToolStripMenuItem1;
    }
}

