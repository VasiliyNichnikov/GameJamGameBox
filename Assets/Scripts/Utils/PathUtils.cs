using System;
using System.IO;

namespace Utils
{
    public static class PathUtils
    {
        public static string GetJsonRoot(string nameFile)
        {
            if (nameFile.EndsWith(".json") == false)
            {
                nameFile += ".json";
            }

            var rootPath = GetRootAssets();
            var tempPath = Path.Combine(rootPath, "Resources\\Json");
            if (Directory.Exists(tempPath) == false)
            {
                Directory.CreateDirectory(tempPath);
            }

            tempPath = Path.Combine(tempPath, nameFile);
            if (File.Exists(tempPath) == false)
            {
                throw new Exception("File does not exist");
            }

            return tempPath;
        }

        private static string GetRootAssets()
        {
            return Path.Combine(GetRootProject(), "Assets");
        }

        private static string GetRootProject()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}