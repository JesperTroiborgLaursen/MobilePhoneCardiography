using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace DataAccessLayer
{
    public interface IFileAccess
    {
        string GetCombinePath(string fileName);
    }

    public class FileSystemAccess : IFileAccess
    {
        public string GetCombinePath(string fileName)
        {
            return Path.Combine(FileSystem.AppDataDirectory, fileName);
        }
    }
}
