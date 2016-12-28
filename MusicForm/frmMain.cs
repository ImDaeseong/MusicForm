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
    public partial class frmMain : Form
    {
        /*
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int which);
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern int SystemParametersInfo(int uAction, int uParam, out RECT lpvParam, int fuWinIni);
        private const int SPI_GETWORKAREA = 48;
        */

        private modPlayer.clsFmodPlayer FmodPlay = modPlayer.clsFmodPlayer.getInstance;

        private ArrayList m_AryFilelist = new ArrayList();
        private int m_nIndex = 0;
        private string m_strCurrentMp3Path = "";
        private string m_strCurrentTime = "";
        private string m_strPlayTime = "";
        private float m_fVolume = 0.5f;


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MoveLocationDialog();
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

        private void btnSkipLeft_Click(object sender, EventArgs e)
        {
            FmodPlay.SkipBack();
        }

        private void btnSkipRight_Click(object sender, EventArgs e)
        {
            FmodPlay.SkipForward();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_strCurrentTime = FmodPlay.GetCurrentTimeDisplay();
            m_strPlayTime = string.Format("{0} / {1}", m_strCurrentTime, FmodPlay.GetTotalTimeDisplay());
            lblTime.Text = m_strPlayTime;

            trackBar1.Value = (int)FmodPlay.GetPosition();

            if (!FmodPlay.IsPlaying())
            {
                timer1.Stop();
                NextPlayMusic();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            FmodPlay.SetPosition((uint)trackBar1.Value);
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

            trackBar1.Visible = true;
            trackBar1.Maximum = (int)FmodPlay.GetRunningTime();
            trackBar1.Minimum = 0;
            

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
