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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logOutPut = new System.Windows.Forms.RichTextBox();
            this.btnOnline = new System.Windows.Forms.Button();
            this.lbIP = new System.Windows.Forms.Label();
            this.comboIP = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // btnOnline
            // 
            this.btnOnline.Location = new System.Drawing.Point(17, 82);
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
            this.lbIP.Location = new System.Drawing.Point(14, 14);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(74, 17);
            this.lbIP.TabIndex = 2;
            this.lbIP.Text = "IP Address:";
            // 
            // comboIP
            // 
            this.comboIP.FormattingEnabled = true;
            this.comboIP.Items.AddRange(new object[] {
            "180.153.24.227",
            "180.153.29.213",
            "180.153.15.118",
            "180.153.38.219"});
            this.comboIP.Location = new System.Drawing.Point(104, 14);
            this.comboIP.Name = "comboIP";
            this.comboIP.Size = new System.Drawing.Size(121, 20);
            this.comboIP.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(17, 40);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Listen";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 501);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainFrm";
            this.Text = "AuctionSocketClient";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox logOutPut;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.ComboBox comboIP;
        private System.Windows.Forms.Button btnOnline;
    }
}

