using System.IO;

namespace BusinessLogic
{
    public interface ISoundModifyLogic
    {
        void PlayRecording();
        void PlayRecording(Stream sound);
    }
}