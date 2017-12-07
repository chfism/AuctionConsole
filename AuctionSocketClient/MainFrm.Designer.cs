namespace AuctionSocketClient
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logOutPut = new System.Windows.Forms.RichTextBox();
            this.txtidcard = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtBidNumber = new System.Windows.Forms.TextBox();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnGetImageCode = new System.Windows.Forms.Button();
            this.txtImageNumber = new System.Windows.Forms.TextBox();
            this.pictureBoxLogin = new System.Windows.Forms.PictureBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnOnline = new System.Windows.Forms.Button();
            this.lbIP = new System.Windows.Forms.Label();
            this.comboIP = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.logOutPut);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.txtidcard);
            this.splitContainer1.Panel2.Controls.Add(this.txtPassword);
            this.splitContainer1.Panel2.Controls.Add(this.txtBidNumber);
            this.splitContainer1.Panel2.Controls.Add(this.btnLogon);
            this.splitContainer1.Panel2.Controls.Add(this.btnGetImageCode);
            this.splitContainer1.Panel2.Controls.Add(this.txtImageNumber);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxLogin);
            this.splitContainer1.Panel2.Controls.Add(this.btnStop);
            this.splitContainer1.Panel2.Controls.Add(this.btnOnline);
            this.splitContainer1.Panel2.Controls.Add(this.lbIP);
            this.splitContainer1.Panel2.Controls.Add(this.comboIP);
            this.splitContainer1.Panel2.Controls.Add(this.btnStart);
            this.splitContainer1.Size = new System.Drawing.Size(903, 501);
            this.splitContainer1.SplitterDistance = 579;
            this.splitContainer1.TabIndex = 0;
            // 
            // logOutPut
            // 
            this.logOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutPut.Location = new System.Drawing.Point(0, 0);
            this.logOutPut.Name = "logOutPut";
            this.logOutPut.Size = new System.Drawing.Size(579, 501);
            this.logOutPut.TabIndex = 0;
            this.logOutPut.Text = "";
            // 
            // txtidcard
            // 
            this.txtidcard.Location = new System.Drawing.Point(17, 39);
            this.txtidcard.Name = "txtidcard";
            this.txtidcard.Size = new System.Drawing.Size(147, 21);
            this.txtidcard.TabIndex = 15;
            this.txtidcard.Text = "913101147818633852";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(135, 12);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.Text = "2054";
            // 
            // txtBidNumber
            // 
            this.txtBidNumber.Location = new System.Drawing.Point(17, 12);
            this.txtBidNumber.Name = "txtBidNumber";
            this.txtBidNumber.Size = new System.Drawing.Size(100, 21);
            this.txtBidNumber.TabIndex = 13;
            this.txtBidNumber.Text = "80658434";
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(17, 163);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(95, 23);
            this.btnLogon.TabIndex = 12;
            this.btnLogon.Text = "Login";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // btnGetImageCode
            // 
            this.btnGetImageCode.Location = new System.Drawing.Point(17, 66);
            this.btnGetImageCode.Name = "btnGetImageCode";
            this.btnGetImageCode.Size = new System.Drawing.Size(95, 23);
            this.btnGetImageCode.TabIndex = 11;
            this.btnGetImageCode.Text = "GetImageCode";
            this.btnGetImageCode.UseVisualStyleBackColor = true;
            this.btnGetImageCode.Click += new System.EventHandler(this.btnGetImageCode_Click);
            // 
            // txtImageNumber
            // 
            this.txtImageNumber.Location = new System.Drawing.Point(125, 124);
            this.txtImageNumber.Name = "txtImageNumber";
            this.txtImageNumber.Size = new System.Drawing.Size(100, 21);
            this.txtImageNumber.TabIndex = 10;
            // 
            // pictureBoxLogin
            // 
            this.pictureBoxLogin.Location = new System.Drawing.Point(17, 95);
            this.pictureBoxLogin.Name = "pictureBoxLogin";
            this.pictureBoxLogin.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxLogin.TabIndex = 9;
            this.pictureBoxLogin.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(118, 284);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnOnline
            // 
            this.btnOnline.Location = new System.Drawing.Point(17, 313);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(147, 23);
            this.btnOnline.TabIndex = 3;
            this.btnOnline.Text = "Send OnlineMessage";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbIP.Location = new System.Drawing.Point(14, 252);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(74, 17);
            this.lbIP.TabIndex = 2;
            this.lbIP.Text = "IP Address:";
            // 
            // comboIP
            // 
            this.comboIP.FormattingEnabled = true;
            this.comboIP.Items.AddRange(new object[] {
            "180.153.38.219",
            "180.153.29.213",
            "180.153.15.118",
            "180.153.24.227"});
            this.comboIP.Location = new System.Drawing.Point(104, 249);
            this.comboIP.Name = "comboIP";
            this.comboIP.Size = new System.Drawing.Size(121, 20);
            this.comboIP.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 284);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Listen";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 501);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainFrm";
            this.Text = "AuctionSocketClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox logOutPut;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.ComboBox comboIP;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxLogin;
        private System.Windows.Forms.TextBox txtImageNumber;
        private System.Windows.Forms.Button btnGetImageCode;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.TextBox txtBidNumber;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtidcard;
        private System.Windows.Forms.Button button1;
    }
}

