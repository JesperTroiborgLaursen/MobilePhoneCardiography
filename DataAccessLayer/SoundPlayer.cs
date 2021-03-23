using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using DataAccessLayer.Services.Interface;
using Plugin.AudioRecorder;

namespace DataAccessLayer
{
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

    public interface IAudioPlayer
    {
        event EventHandler FinishedPlaying;
        void Pause();
        void PlaySound(string pathToAudioFile);
    }


    public class SoundPlayer : ISoundPlayer
    {
        public string _filePath;
        private IAudioPlayer _player;
        private ISaveToMobile _localStorage;
        private IFileAccess _fileAccess;

        public SoundPlayer()
        {
            _fileAccess = new FileSystemAccess();
            _localStorage = new SaveToMobile();
            _player = new ExtendedAudioPlayer();

            _filePath = _fileAccess.GetCombinePath("Recording.wav");
        }

        /// <summary>
        /// Her bliver der afspillede en bestemt stream som skal injectieres.
        /// Denne Stream vil blive gemt i AppDataDir,
        /// hvorefter den bliver afspillet med vores default PlayRecording().
        /// 
        /// Dette er smart når vi skal til at hente tidligere målinger,
        /// da disse som udgangspunkt IKKE er gemt i AppDataDir fra start af.
        /// </summary>
        /// <param name="sound">Den specefikke lyd der ønskes afspillet</param>
        public void PlayRecording(Stream sound)
        {
            _localStorage.Save(_filePath, sound);
            _player.PlaySound(_filePath);
        }


        /// <summary>
        /// Kan gemme et Stream objekt ned i en fil, hvis sti medtages som parameter
        /// </summary>
        /// <param name="path">Sti hvor lyden er gemt</param>
        /// <param name="stream">Lyden der skal gemmes</param>
        private void SaveFileStream(string path, Stream stream)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
        }
    }
}
