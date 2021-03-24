using System.IO;

namespace DataAccessLayer.Services.Interface
{
    public interface ISoundPlayer
    {
        /// <summary>
        /// Her bliver der afspillede en bestemt stream som skal injectieres.
        /// Denne Stream vil blive gemt i AppDataDir,
        /// hvorefter den bliver afspillet med vores default PlayRecording().
        /// 
        /// Dette er smart når vi skal til at hente tidligere målinger,
        /// da disse som udgangspunkt IKKE er gemt i AppDataDir fra start af.
        /// </summary>
        /// <param name="sound">Den specefikke lyd der ønskes afspillet</param>
        void PlayRecording(Stream sound);
    }
}
