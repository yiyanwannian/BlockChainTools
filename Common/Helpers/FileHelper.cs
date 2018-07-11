using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Common.Helpers
{
    /// <summary>
    /// Class FileHelper.
    /// </summary>
    public static class FileHelper
    {
        public static void CreateFile(string filePath, string text, Encoding encoding, bool isCreateWhenExist = false)
        {
            try
            {
                var isExist = IsExistFile(filePath);

                if (isExist && !isCreateWhenExist)
                {
                    return;
                }

                if (isExist && isCreateWhenExist)
                {
                    DeleteFile(filePath);
                }
                else
                {
                    string directoryPath = GetDirectoryFromFilePath(filePath);
                    CreateDirectory(directoryPath);

                    //Create File
                    FileInfo file = new FileInfo(filePath);
                    using (FileStream stream = file.Create())
                    {
                        using (StreamWriter writer = new StreamWriter(stream, encoding))
                        {
                            writer.Write(text);
                            writer.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static string GetDirectoryFromFilePath(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            DirectoryInfo directory = file.Directory;
            return directory.FullName;
        }

        public static List<FileInfo> GetFilesInDirectory(string filePath)
        {
            var fileInfos = new List<FileInfo>();
            if (!IsExistDirectory(filePath))
            {
                return fileInfos;
            }

            var files = new DirectoryInfo(filePath).GetFiles();

            if (!files.IsEmpty())
            {
                fileInfos = files.ToList();
            }

            return fileInfos;
        }

        public static List<string> GetFileNamesInDirectory(string filePath)
        {
            var fileNames = new List<string>();
            var fileInfos = GetFilesInDirectory(filePath);
            if (fileInfos.Any())
            {
                fileNames = fileInfos.Select(file => file.Name).ToList();
            }

            return fileNames;
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Loads the file to string.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string LoadFileToString(string path)
        {
            if (!IsExistFile(path))
            {
                throw new IOException($"Can not find file in { path }");
            }

            string content = string.Empty;

            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    content = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Load file failed!", ex);
            }

            return content;
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <param name="path">The path.</param>
        public static void WriteToFile(List<string> lines, string path)
        {
            if (lines.IsEmpty())
            {
                return;
            }

            File.WriteAllLines(path, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Appends to file.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="path">The path.</param>
        public static void AppendToFile(string line, string path)
        {
            if (!File.Exists(path) && string.IsNullOrEmpty(line))
            {
                return;
            }

            File.AppendAllText(path, line, Encoding.UTF8);
        }
    }
}
