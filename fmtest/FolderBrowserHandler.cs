using System.IO;
using Microsoft.Win32;

namespace fmtest
{
    public class FolderBrowserHandler
    {
        private const string CacheFilePath = "directoryCache.txt";

        public string OpenFolderBrowserDialog()
        {
            var dialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection.",
                Filter = "Folder|*.none"
            };

            if (dialog.ShowDialog() == true)
            {
                string selectedPath = Path.GetDirectoryName(dialog.FileName);
                CacheDirectory(selectedPath);
                return selectedPath;
            }
            return null;
        }

        private void CacheDirectory(string path)
        {
            try
            {
                File.WriteAllText(CacheFilePath, path);
            }
            catch (Exception ex)
            {
                
            }
        }

        public string GetCachedDirectory()
        {
            if (File.Exists(CacheFilePath))
            {
                try
                {
                    return File.ReadAllText(CacheFilePath);
                }
                catch (Exception ex)
                {
                    
                }
            }
            return null;
        }
    }


}
