using System.IO;


namespace DataAccessLayer
{
    /// <summary>
    /// Bruges til at afkoble systemet så det kan testes
    /// </summary>
    public interface ISaveToMobile
    {
        /// <summary>
        /// Kan gemme en stream
        /// </summary>
        /// <param name="SaveToFilePath">Stien på der der ønskes at gemmes</param>
        /// <param name="ElementToSave">Det stream objekt der ønskes at gemmes</param>
        void Save(string SaveToFilePath, Stream ElementToSave);
    }

    public class SaveToMobile : ISaveToMobile
    {
        public void Save(string saveToFilePath, Stream elementToSave)
        {
            using (var fileStream = new FileStream(saveToFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                elementToSave.CopyTo(fileStream);
                fileStream.Dispose();
            }
        }
    }
}
