﻿using System.IO;

namespace VillianSearcher.DAL.Helpers
{
    public static class IOHelper
    {
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static string ReadFromFile(string path)
        {
            if (!FileExists(path)) throw new FileNotFoundException($"File: {path} doesn't exists!");

            return File.ReadAllText(path);
        }

        public static void WriteToFile(string obj, string path)
        {
            if (!FileExists(path)) throw new FileNotFoundException($"File: {path} doesn't exists!");

            File.WriteAllText(path, obj);
        }

        public static void CreateFileIfNotExists(string path)
        {
            if (FileExists(path)) return;

            FileStream fs = File.Create(path);

            fs.Close();
            fs.Dispose();
        }

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (DirectoryExists(path)) return;

            Directory.CreateDirectory(path);
        }

        public static DirectoryInfo[] GetSubDirectories(string path)
        {
            if (!DirectoryExists(path)) return null;

            DirectoryInfo di = new DirectoryInfo(path);

            return di.GetDirectories();
        }

        public static FileInfo[] GetDirectoryFiles(string path)
        {
            if (!DirectoryExists(path)) return null;

            DirectoryInfo di = new DirectoryInfo(path);

            return di.GetFiles();
        }
    }
}
