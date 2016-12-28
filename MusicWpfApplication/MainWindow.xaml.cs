using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;

namespace MusicWpfApplication
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        static private DispatcherTimer dispatcherTimer;

        private modPlayer.clsFmodPlayer FmodPlay = modPlayer.clsFmodPlayer.getInstance;

        private ArrayList m_AryFilelist = new ArrayList();
        private int m_nIndex = 0;
        private string m_strCurrentMp3Path = "";
        private string m_strCurrentTime = "";
        private string m_strPlayTime = "";
        private float m_fVolume = 0.5f;

        private void InitTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }
        private void StartTimer()
        {
            dispatcherTimer.Start();
        }

        private void stopTimer()
        {
            dispatcherTimer.Stop();
        }

        private void MoveLocationDialog(bool bCenter = false)
        {           
            if (bCenter)
            {
                double dScreenWidth =  System.Windows.SystemParameters.WorkArea.Width;
                double dScreenHeight = System.Windows.SystemParameters.WorkArea.Height;
                double dWidht = (dScreenWidth - this.Width);
                double dHeight = (dScreenHeight - this.Height);
                this.Left = (dWidht / 2);
                this.Top = (dHeight / 2);
            }
            else
            {
                double dScreenWidth =  System.Windows.SystemParameters.WorkArea.Width;
                double dScreenHeight = System.Windows.SystemParameters.WorkArea.Height;
                double dWidht = (dScreenWidth - this.Width);
                double dHeight = (dScreenHeight - this.Height);
                this.Left = dWidht;
                this.Top = dHeight;
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

            stopTimer();
        }

        private void Pause()
        {
            FmodPlay.Pause();
        }

        private void Play()
        {
            FmodPlay.Open(m_strCurrentMp3Path);

            txtBlockMusicInfo.Text = ReadMp3TagInfo();

            stopTimer();
            StartTimer();

            slider.Visibility = Visibility.Visible;
            slider.Maximum = FmodPlay.GetRunningTime();
            slider.Minimum = 0;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void imgLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrePlayMusic();
        }

        private void imgRight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NextPlayMusic();
        }

        private void imgPlus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (m_fVolume > 1)
                m_fVolume = 1f;
            else
                m_fVolume += 0.1f;
            FmodPlay.SetVolume(m_fVolume);
        }

        private void imgMinus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (m_fVolume < 0)
                m_fVolume = 0f;
            else
                m_fVolume -= 0.1f;
            FmodPlay.SetVolume(m_fVolume);
        }

        private void imgPlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FmodPlay.Pause();
        }

        private void imgAddfile_MouseDown(object sender, MouseButtonEventArgs e)
        {          
            m_AryFilelist.Clear();

            FolderBrowserDialog selectfolder = new FolderBrowserDialog();
            selectfolder.Description = "Select Mp3";
            selectfolder.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;//Directory.GetCurrentDirectory(); 
            DialogResult result = selectfolder.ShowDialog();
            if (result.ToString() == "OK")
            {
                if (selectfolder.SelectedPath != "")
                    SetFileInfo(selectfolder.SelectedPath, m_AryFilelist);
            }

            NextPlayMusic(true);           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveLocationDialog();

            InitTimer();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            m_strCurrentTime = FmodPlay.GetCurrentTimeDisplay();
            m_strPlayTime = string.Format("{0} / {1}", m_strCurrentTime, FmodPlay.GetTotalTimeDisplay());
            txtBlockTime.Text = m_strPlayTime;

            slider.Value = FmodPlay.GetPosition();

            if (!FmodPlay.IsPlaying())
            {
                stopTimer();
                NextPlayMusic();
            }
        }

        private void btnSkipLeft_Click(object sender, RoutedEventArgs e)
        {
            FmodPlay.SkipBack();
        }

        private void btnSkipRight_Click(object sender, RoutedEventArgs e)
        {
            FmodPlay.SkipForward();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FmodPlay.SetPosition((uint)slider.Value);
        }
       
        private void Window_Closed(object sender, EventArgs e)
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
            }
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }        
    }
}
