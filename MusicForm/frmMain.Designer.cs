namespace MusicForm
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.pbPlus = new System.Windows.Forms.PictureBox();
            this.pbMinus = new System.Windows.Forms.PictureBox();
            this.pbPlay = new System.Windows.Forms.PictureBox();
            this.pbAddfile = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMusicInfo = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnSkipLeft = new System.Windows.Forms.Button();
            this.btnSkipRight = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLeft
            // 
            this.pbLeft.BackColor = System.Drawing.Color.Transparent;
            this.pbLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLeft.Image = global::MusicForm.Properties.Resources.bw;
            this.pbLeft.Location = new System.Drawing.Point(28, 115);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(18, 18);
            this.pbLeft.TabIndex = 0;
            this.pbLeft.TabStop = false;
            this.pbLeft.Click += new System.EventHandler(this.pbLeft_Click);
            this.pbLeft.MouseLeave += new System.EventHandler(this.pbLeft_MouseLeave);
            this.pbLeft.MouseHover += new System.EventHandler(this.pbLeft_MouseHover);
            // 
            // pbRight
            // 
            this.pbRight.BackColor = System.Drawing.Color.Transparent;
            this.pbRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRight.Image = global::MusicForm.Properties.Resources.FW;
            this.pbRight.Location = new System.Drawing.Point(139, 115);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(18, 18);
            this.pbRight.TabIndex = 1;
            this.pbRight.TabStop = false;
            this.pbRight.Click += new System.EventHandler(this.pbRight_Click);
            this.pbRight.MouseLeave += new System.EventHandler(this.pbRight_MouseLeave);
            this.pbRight.MouseHover += new System.EventHandler(this.pbRight_MouseHover);
            // 
            // pbPlus
            // 
            this.pbPlus.BackColor = System.Drawing.Color.Transparent;
            this.pbPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPlus.Image = global::MusicForm.Properties.Resources.plus;
            this.pbPlus.Location = new System.Drawing.Point(84, 60);
            this.pbPlus.Name = "pbPlus";
            this.pbPlus.Size = new System.Drawing.Size(18, 18);
            this.pbPlus.TabIndex = 2;
            this.pbPlus.TabStop = false;
            this.pbPlus.Click += new System.EventHandler(this.pbPlus_Click);
            this.pbPlus.MouseLeave += new System.EventHandler(this.pbPlus_MouseLeave);
            this.pbPlus.MouseHover += new System.EventHandler(this.pbPlus_MouseHover);
            // 
            // pbMinus
            // 
            this.pbMinus.BackColor = System.Drawing.Color.Transparent;
            this.pbMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinus.Image = global::MusicForm.Properties.Resources.min;
            this.pbMinus.Location = new System.Drawing.Point(84, 170);
            this.pbMinus.Name = "pbMinus";
            this.pbMinus.Size = new System.Drawing.Size(18, 18);
            this.pbMinus.TabIndex = 3;
            this.pbMinus.TabStop = false;
            this.pbMinus.Click += new System.EventHandler(this.pbMinus_Click);
            this.pbMinus.MouseLeave += new System.EventHandler(this.pbMinus_MouseLeave);
            this.pbMinus.MouseHover += new System.EventHandler(this.pbMinus_MouseHover);
            // 
            // pbPlay
            // 
            this.pbPlay.BackColor = System.Drawing.Color.Transparent;
            this.pbPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPlay.Image = global::MusicForm.Properties.Resources.pp1;
            this.pbPlay.Location = new System.Drawing.Point(84, 116);
            this.pbPlay.Name = "pbPlay";
            this.pbPlay.Size = new System.Drawing.Size(18, 18);
            this.pbPlay.TabIndex = 4;
            this.pbPlay.TabStop = false;
            this.pbPlay.Click += new System.EventHandler(this.pbPlay_Click);
            this.pbPlay.MouseLeave += new System.EventHandler(this.pbPlay_MouseLeave);
            this.pbPlay.MouseHover += new System.EventHandler(this.pbPlay_MouseHover);
            // 
            // pbAddfile
            // 
            this.pbAddfile.BackColor = System.Drawing.Color.Transparent;
            this.pbAddfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbAddfile.Image = global::MusicForm.Properties.Resources.addfiles2;
            this.pbAddfile.Location = new System.Drawing.Point(68, 225);
            this.pbAddfile.Name = "pbAddfile";
            this.pbAddfile.Size = new System.Drawing.Size(50, 50);
            this.pbAddfile.TabIndex = 5;
            this.pbAddfile.TabStop = false;
            this.pbAddfile.Click += new System.EventHandler(this.pbAddfile_Click);
            this.pbAddfile.MouseLeave += new System.EventHandler(this.pbAddfile_MouseLeave);
            this.pbAddfile.MouseHover += new System.EventHandler(this.pbAddfile_MouseHover);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblTime.Location = new System.Drawing.Point(11, 408);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(162, 23);
            this.lblTime.TabIndex = 6;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblMusicInfo
            // 
            this.lblMusicInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblMusicInfo.Location = new System.Drawing.Point(11, 375);
            this.lblMusicInfo.Name = "lblMusicInfo";
            this.lblMusicInfo.Size = new System.Drawing.Size(162, 23);
            this.lblMusicInfo.TabIndex = 8;
            this.lblMusicInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClose.Image = global::MusicForm.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(161, 12);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(16, 16);
            this.pbClose.TabIndex = 9;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 302);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(161, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnSkipLeft
            // 
            this.btnSkipLeft.Location = new System.Drawing.Point(68, 320);
            this.btnSkipLeft.Name = "btnSkipLeft";
            this.btnSkipLeft.Size = new System.Drawing.Size(21, 23);
            this.btnSkipLeft.TabIndex = 11;
            this.btnSkipLeft.Text = "<<";
            this.btnSkipLeft.UseVisualStyleBackColor = true;
            this.btnSkipLeft.Visible = false;
            this.btnSkipLeft.Click += new System.EventHandler(this.btnSkipLeft_Click);
            // 
            // btnSkipRight
            // 
            this.btnSkipRight.Location = new System.Drawing.Point(95, 320);
            this.btnSkipRight.Name = "btnSkipRight";
            this.btnSkipRight.Size = new System.Drawing.Size(21, 23);
            this.btnSkipRight.TabIndex = 12;
            this.btnSkipRight.Text = ">>";
            this.btnSkipRight.UseVisualStyleBackColor = true;
            this.btnSkipRight.Visible = false;
            this.btnSkipRight.Click += new System.EventHandler(this.btnSkipRight_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MusicForm.Properties.Resources.SKIN1_layer;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(185, 450);
            this.ControlBox = false;
            this.Controls.Add(this.btnSkipRight);
            this.Controls.Add(this.btnSkipLeft);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.lblMusicInfo);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.pbAddfile);
            this.Controls.Add(this.pbPlay);
            this.Controls.Add(this.pbMinus);
            this.Controls.Add(this.pbPlus);
            this.Controls.Add(this.pbRight);
            this.Controls.Add(this.pbLeft);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLeft;
        private System.Windows.Forms.PictureBox pbRight;
        private System.Windows.Forms.PictureBox pbPlus;
        private System.Windows.Forms.PictureBox pbMinus;
        private System.Windows.Forms.PictureBox pbPlay;
        private System.Windows.Forms.PictureBox pbAddfile;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblMusicInfo;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnSkipLeft;
        private System.Windows.Forms.Button btnSkipRight;
    }
}

