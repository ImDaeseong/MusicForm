using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace MusicForm
{   
    internal class API
    {
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
        public const Int32 ULW_ALPHA = 0x00000002;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);


        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);


        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);
    }

    public partial class frmMain : Form
    {
        private modPlayer.clsFmodPlayer FmodPlay = modPlayer.clsFmodPlayer.getInstance;

        private ArrayList m_AryFilelist = new ArrayList();
        private int m_nIndex = 0;
        private string m_strCurrentMp3Path = "";
        private string m_strCurrentTime = "";
        private string m_strPlayTime = "";
        private float m_fVolume = 0.5f;
               
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_LAYERED = 0x00080000;
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_LAYERED;
                return cp;
            }
        }
      
        public void UpdateFormDisplay(Image backgroundImage)
        {
            IntPtr screenDc = API.GetDC(IntPtr.Zero);
            IntPtr memDc = API.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                //Display-image
                Bitmap bmp = new Bitmap(backgroundImage);
                hBitmap = bmp.GetHbitmap(Color.FromArgb(0));  //Set the fact that background is transparent
                oldBitmap = API.SelectObject(memDc, hBitmap);

                //Display-rectangle
                Size size = bmp.Size;
                Point pointSource = new Point(0, 0);
                Point topPos = new Point(this.Left, this.Top);

                //Set up blending options
                API.BLENDFUNCTION blend = new API.BLENDFUNCTION();
                blend.BlendOp = API.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = 255;
                blend.AlphaFormat = API.AC_SRC_ALPHA;

                API.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, API.ULW_ALPHA);

                //Clean-up
                bmp.Dispose();
                API.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    API.SelectObject(memDc, oldBitmap);
                    API.DeleteObject(hBitmap);
                }
                API.DeleteDC(memDc);
            }
            catch (Exception)
            {
            }
        }        

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateFormDisplay(BackgroundImage);

            MoveLocationDialog();
        }
        protected override void OnPaint(PaintEventArgs e)
        {            
            UpdateFormDisplay(BackgroundImage);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void pbLeft_Click(object sender, EventArgs e)
        {
            PrePlayMusic();
        }

        private void pbLeft_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbLeft_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbRight_Click(object sender, EventArgs e)
        {
            NextPlayMusic();
        }

        private void pbRight_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbRight_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbPlus_Click(object sender, EventArgs e)
        {
            if (m_fVolume > 1)
                m_fVolume = 1f;
            else
                m_fVolume += 0.1f;
            FmodPlay.SetVolume(m_fVolume);
        }

        private void pbPlus_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbPlus_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbMinus_Click(object sender, EventArgs e)
        {
            if (m_fVolume < 0 )
                m_fVolume = 0f;
            else
                m_fVolume -= 0.1f;
            FmodPlay.SetVolume(m_fVolume);
        }

        private void pbMinus_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbMinus_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pbPlay_Click(object sender, EventArgs e)
        {
            FmodPlay.Pause();
        }

        private void pbPlay_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbPlay_MouseLeave(object sender, EventArgs e)
        {

        }
                
        private void pbAddfile_Click(object sender, EventArgs e)
        {
            m_AryFilelist.Clear();

            FolderBrowserDialog selectfolder = new FolderBrowserDialog();
            selectfolder.Description = "Select Mp3";
            selectfolder.SelectedPath = Application.StartupPath;
            if (selectfolder.ShowDialog() == DialogResult.OK)
            {
                if(selectfolder.SelectedPath != "")
                    SetFileInfo(selectfolder.SelectedPath, m_AryFilelist);
            }

            NextPlayMusic(true);
        }

        private void pbAddfile_MouseHover(object sender, EventArgs e)
        {

        }

        private void pbAddfile_MouseLeave(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_strCurrentTime = FmodPlay.GetCurrentTimeDisplay();
            m_strPlayTime = string.Format("{0} / {1}", m_strCurrentTime, FmodPlay.GetTotalTimeDisplay());
            lblTime.Text = m_strPlayTime;

            if (!FmodPlay.IsPlaying())
            {
                timer1.Stop();
                NextPlayMusic();
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MoveLocationDialog(bool bCenter = false)
        {
            if (bCenter)
            {
                int nScreenWidth = Screen.PrimaryScreen.Bounds.Width;
                int nScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                int nWidht = (nScreenWidth - this.Width);
                int nHeight = (nScreenHeight - this.Height);
                this.Location = new Point((nWidht / 2), (nHeight / 2));
            }
            else
            {
                int nScreenWidth = Screen.PrimaryScreen.Bounds.Width;
                int nScreenHeight = Screen.PrimaryScreen.Bounds.Height;
                int cx = ClientSize.Width;
                int cy = ClientSize.Height;
                this.Location = new Point((nScreenWidth - cx), (nScreenHeight - cy) - 30);

                /*
                int cx = ClientSize.Width;
                int cy = ClientSize.Height;
                RECT rcWorkArea = new RECT();
                SystemParametersInfo(SPI_GETWORKAREA, 0, out rcWorkArea, 0);
                this.Location = new Point((rcWorkArea.right - cx), (rcWorkArea.bottom - cy));
                */
            }
        }

        string GetFileExtName(string strFilename)
        {
            int nPos = strFilename.LastIndexOf('.');
            int nLength = strFilename.Length;
            if (nPos < nLength)
                return strFilename.Substring(nPos + 1, (nLength - nPos) - 1);
            return string.Empty;
        }

        string GetFileName(string strFilename)
        {
            int nPos = strFilename.LastIndexOf('\\');
            int nLength = strFilename.Length;
            if (nPos < nLength)
                return strFilename.Substring(nPos + 1, (nLength - nPos) - 1);
            return strFilename;
        }
        private void SetFileInfo(string strPath, ArrayList AryFilelist)
        {
            string[] strfileList = Directory.GetFiles(strPath);
            foreach (string strFileName in strfileList)
            {
                if (GetFileExtName(strFileName).ToLower() == "mp3")
                    AryFilelist.Add(strFileName);
            }

            string[] strDirList = Directory.GetDirectories(strPath);
            foreach (string strDir in strDirList)
                SetFileInfo(strDir, AryFilelist);
        }

        private void Stop()
        {
            FmodPlay.Stop();

            timer1.Stop();
        }

        private void Pause()
        {
            FmodPlay.Pause();
        }

        private void Play()
        {
            FmodPlay.Open(m_strCurrentMp3Path);
            
            lblMusicInfo.Text = ReadMp3TagInfo();

            timer1.Stop();
            timer1.Start();

            FmodPlay.Play();
            
            FmodPlay.SetVolume(m_fVolume);
        }

        private string ReadMp3TagInfo()
        {
            string strMusicInfo = "";
            using (FileStream fs = File.OpenRead(m_strCurrentMp3Path))
            {
                try
                {
                    byte[] byID = new byte[3];         //  3
                    byte[] byTitle = new byte[30];     //  30
                    byte[] byArtist = new byte[30];    //  30 
                    byte[] byAlbum = new byte[30];     //  30 
                    byte[] byYear = new byte[4];       //  4 
                    byte[] byComment = new byte[30];   //  30 
                    byte[] byGenre = new byte[1];      //  1
                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(byID, 0, 3);
                    fs.Read(byTitle, 0, 30);
                    fs.Read(byArtist, 0, 30);
                    fs.Read(byAlbum, 0, 30);
                    fs.Read(byYear, 0, 4);
                    fs.Read(byComment, 0, 30);
                    fs.Read(byGenre, 0, 1);
                    string strID = Encoding.Default.GetString(byID);
                    if (strID.Equals("TAG"))
                    {
                        string strTitle = Encoding.Default.GetString(byTitle).Replace("\0", "");
                        string strArtist = Encoding.Default.GetString(byArtist).Replace("\0", "");
                        string strAlbum = Encoding.Default.GetString(byAlbum).Replace("\0", "");
                        string strYear = Encoding.Default.GetString(byYear).Replace("\0", "");
                        string strComment = Encoding.Default.GetString(byComment).Replace("\0", "");
                        string strGenre = Encoding.Default.GetString(byGenre).Replace("\0", "");

                        if (strArtist != "" && strTitle != "")
                            strMusicInfo = string.Format("{0} - {1}", strArtist, strTitle);
                    }                    
                }
                catch (Exception) { }
            }
            return strMusicInfo;
        }

        private void PrePlayMusic()
        {
            if (m_AryFilelist.Count == 0) return;

            m_nIndex--;

            if (m_nIndex < 0)
                m_nIndex = m_AryFilelist.Count - 1;

            m_strCurrentMp3Path = m_AryFilelist[m_nIndex].ToString();

            Stop();
            Play();
        }

        private void NextPlayMusic(bool bFirstPlay = false)
        {
            if (m_AryFilelist.Count == 0) return;

            if (bFirstPlay)
            {
                m_nIndex = 0;
                m_strCurrentMp3Path = m_AryFilelist[m_nIndex].ToString();

                Stop();
                Play();
            }
            else
            {
                m_nIndex++;

                if (m_nIndex > (m_AryFilelist.Count - 1))
                    m_nIndex = 0;

                m_strCurrentMp3Path = m_AryFilelist[m_nIndex].ToString();
               
                Stop();
                Play();
            }
        }        
    }
}
