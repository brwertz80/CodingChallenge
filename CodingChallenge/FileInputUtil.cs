using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodingChallenge
{
    public static class FileInputUtil
    {

        /// <summary>
        /// Returns a fileinfo with the full path of the requested file
        /// </summary>
        /// <param name="directory">A subdirectory</param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FileInfo GetFileInfo(string directory, string file)
        {
            var rootDir = AppDomain.CurrentDomain.BaseDirectory;
            return new FileInfo(Path.Combine(rootDir, directory, file));
        }

        //public static DirectoryInfo GetRootDirectory()
        //{
        //    var currentDir = 
        //        currentDir = Directory.GetParent(currentDir).FullName.TrimEnd('\\');
        //    }
        //    return new DirectoryInfo(currentDir).Parent;
        //}

        //public static DirectoryInfo GetSubDirectory(string directory, string subDirectory)
        //{
        //    var currentDir = GetRootDirectory().FullName;
        //    return new DirectoryInfo(Path.Combine(currentDir, directory, subDirectory));
        //}
    }
}
