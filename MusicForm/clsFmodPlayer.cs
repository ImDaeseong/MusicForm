using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using FMOD;

namespace modPlayer
{
    class clsFmodPlayer
    {
        private static clsFmodPlayer selfInstance = null;
        public static clsFmodPlayer getInstance
        {
            get
            {
                if (selfInstance == null) selfInstance = new clsFmodPlayer();
                return selfInstance;
            }
        }


        private FMOD.System system = null;
        private FMOD.Sound sound = null;
        private FMOD.Channel channel = null;
        private FMOD.RESULT result;

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        public static void Init()
        {
            LoadLibrary("fmod.dll");
        }

        public clsFmodPlayer()
        {
            Init();

            result = FMOD.Factory.System_Create(out system);
            if (result == FMOD.RESULT.OK)
            {
                uint version = 0;
                result = system.getVersion(out version);
                result = system.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
                if (result != FMOD.RESULT.OK) return;
            }
        }

        ~clsFmodPlayer()
        {
            if (sound != null)
                sound.release();
            result = system.close();
            result = system.release();
        }

        public bool Open(string strFile)
        {
            if (channel != null)
                channel.stop();

            if (sound != null)
                sound.release();
            
            result = system.createStream(strFile, FMOD.MODE.DEFAULT, out sound);
            if (result != FMOD.RESULT.OK) return false;
            return true;
        }

        public bool Play()
        {
            result = system.playSound(sound, null, false, out channel);
            if (result != FMOD.RESULT.OK) return false;
            return true;
        }

        public void Stop()
        {
            bool isPlaying = false;
            if (channel != null)
            {
                result = channel.isPlaying(out isPlaying);

                if (isPlaying)
                    result = channel.stop();
                else
                    result = system.playSound(sound, null, false, out channel);
            }
        }

        public void Pause()
        {
            if (channel != null)
            {
                bool paused;
                result = channel.getPaused(out paused);
                result = channel.setPaused(!paused);
            }
        }

        public bool IsPlaying()
        {
            bool isPlaying = false;
            if (channel != null)
            {
                result = channel.isPlaying(out isPlaying);
                if (result != FMOD.RESULT.OK) return false;
            }
            return isPlaying;
        }

        public void SetMute(bool bMute)
        {
            if (channel != null)
                result = channel.setMute(bMute);
        }

        public void SetVolume(float fVolume)
        {
            if (channel != null)
                result = channel.setVolume(fVolume);
        }

        public uint GetPosition()
        {
            uint pos = 0;
            if (channel != null)
            {
                result = channel.getPosition(out pos, FMOD.TIMEUNIT.MS);
            }
            return pos;//return pos / 1000.f;
        }

        public void SetPosition(uint pos)
        {
            if (channel != null)
            {
                result = channel.setPosition(pos, FMOD.TIMEUNIT.MS);
            }
        }

        public uint GetRunningTime()
        {
            uint len = 0;
            if (sound != null)
            {
                result = sound.getLength(out len, FMOD.TIMEUNIT.MS);
                if (result != FMOD.RESULT.OK) return 0;
            }
            return len;//return float(len) / 1000.0f;
        }

        public void SkipBack()
        {
            uint pos;
            if (channel != null)
            {
                result = channel.getPosition(out pos, FMOD.TIMEUNIT.MS);
                if (result == FMOD.RESULT.OK)
                {
                    pos -= 1000;//pos -= 10000;
                    result = channel.setPosition(pos, FMOD.TIMEUNIT.MS);
                }
            }
        }

        public void SkipForward()
        {
            uint pos;
            if (channel != null)
            {
                result = channel.getPosition(out pos, FMOD.TIMEUNIT.MS);
                if (result == FMOD.RESULT.OK)
                {
                    pos += 1000;//pos += 10000;
                    result = channel.setPosition(pos, FMOD.TIMEUNIT.MS);
                }
            }
        }

        public string GetCurrentTimeDisplay()
        {
            string strCurTime = "";

            if (channel != null)
            {
                uint pos;
                result = channel.getPosition(out pos, FMOD.TIMEUNIT.MS);
                strCurTime = string.Format("{0:D2}:{1:D2}", pos / 1000 / 60, pos / 1000 % 60);
            }
            return strCurTime;
        }

        public string GetTotalTimeDisplay()
        {
            string strTotalTime = "";

            if (sound != null)
            {
                uint len = 0;
                result = sound.getLength(out len, FMOD.TIMEUNIT.MS);
                strTotalTime = string.Format("{0:D2}:{1:D2}", len / 1000 / 60, len / 1000 % 60);
            }
            return strTotalTime;
        }

    }
}
