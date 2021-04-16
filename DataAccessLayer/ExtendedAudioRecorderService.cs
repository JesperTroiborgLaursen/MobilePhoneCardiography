using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Plugin.AudioRecorder;

namespace DataAccessLayer
{
    /// <summary>
    /// Denne gør at vi kan afkoble AudioRecorderService under test
    /// </summary>
    public interface IAudioRecorderService
    {
        /// <summary>
        /// Retunerer true, hvis optagelsen er i gang
        /// </summary>
        bool IsRecording { get; }
        /// <summary>
        /// Den filsti som der gemmes en stream på, kan ændres
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// Get AudioRecorderService.TotalAudioTimeout som double
        /// Set AudioRecorderService.TotalAudioTimeout som double, mothoden konverter selv til TimeSpan
        /// </summary>
        double AudioTimeout { get; set; }
        event EventHandler<string> AudioInputReceived;
        /// <summary>
        /// Starter en async optagelse
        /// </summary>
        /// <returns></returns>
        Task<Task<string>> StartRecording();
        Stream GetAudioFileStream();
    }

    /// <summary>
    /// Denne klasse arver fra AudioRecorderService og implamentere IAudioRecorderService
    /// Det gør at man kan teste på dette datalag
    /// </summary>
    public class ExtendedAudioRecorderService : AudioRecorderService, IAudioRecorderService
    {
        /// <summary>
        /// Sætter som default optagelsen til at stoppe efter 10 sek, og uden at den stopper OnSilence
        /// </summary>
        /// <param name="handleRecordIsFinished">Notificeres når optagelsen er færdig</param>
        public ExtendedAudioRecorderService(EventHandler<string> handleRecordIsFinished)
        {
            StopRecordingOnSilence = false;
            StopRecordingAfterTimeout = true;
            //todo det er her vi sætter tiden til 10 eller 5 sek
            AudioTimeout = 30;

            AudioInputReceived += handleRecordIsFinished;
        }

        public double AudioTimeout
        {
            get => Convert.ToDouble(TotalAudioTimeout);
            set => TotalAudioTimeout = TimeSpan.FromSeconds(value);
        }

      
    }
}
