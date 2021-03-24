using System;
using System.Diagnostics;
using Plugin.AudioRecorder;

namespace DataAccessLayer
{
    public interface IAudioPlayer
    {
        event EventHandler FinishedPlaying;
        void Pause();
        void PlaySound(string pathToAudioFile);
    }

    public class ExtendedAudioPlayer : AudioPlayer, IAudioPlayer
    {
        public void PlaySound(string pathToAudioFile)
        {
            Debug.WriteLine("Optagelsen forsøges afspillles og optagelsen er færdig" + DateTime.Now.ToString());
            try
            {
                Play(pathToAudioFile);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Optagelsen forsøges afspillles og optagelsen er færdig, men kunne ikke afspille " + DateTime.Now.ToString() + "\n" + e.Message);
            }
        }

    }
}