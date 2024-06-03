using System.Collections.Generic;
using System.IO;

namespace kesera2.FBXOptionsManager
{
    public static class Utility
    {
        public static List<string> GetFBXFiles(string folderPath)
        {
            List<string> fbxFiles = new List<string>();
            string[] files = Directory.GetFiles(folderPath, "*.fbx", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                fbxFiles.Add(file);
            }
            return fbxFiles;
        }

        public static bool[] toggleArrayChecks(bool[] array, bool condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = condition;
            }

            return array;
        }
    }

}