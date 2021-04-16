    using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer;
using DataAccessLayer.Services.Interface;

namespace BusinessLogic
{
    public class SoundModifyLogic : ISoundModifyLogic
    {
        private ISoundPlayer _soundPlayer;

        public SoundModifyLogic(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer ?? new SoundPlayer();
        }

        public void PlayRecording(Stream sound)
        {
            _soundPlayer.PlayRecording(sound);
        }
    }
}
