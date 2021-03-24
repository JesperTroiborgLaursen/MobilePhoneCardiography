using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.Services.Interface;

namespace DataAccessLayer
{
    public class SoundPlayer : ISoundPlayer
    {
        #region Fields
        public string _filePath;
        #endregion
        #region Dependencies
        private IAudioPlayer _player;
        private ISaveToMobile _localStorage;
        private IFileAccess _fileAccess;
        #endregion
        #region Ctor

        public SoundPlayer()
        {
            _fileAccess = new FileSystemAccess();
            _localStorage = new SaveToMobile();
            _player = new ExtendedAudioPlayer();

            _filePath = _fileAccess.GetCombinePath("Recording.wav");
        }
        

        #endregion
        #region Metoder

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

        #endregion
    }
}
