using System.Collections.Generic;
using System.IO;

namespace kesera2.FBXOptionsManager
{
    public static class Utility
    {
        private const string FbxExtensionFilter = "*.fbx";

        public static List<string> GetFBXFiles(string folderPath)
        {
            var fbxFilePaths = new List<string>();
            foreach (var file in Directory.GetFiles(folderPath, FbxExtensionFilter, SearchOption.AllDirectories))
                fbxFilePaths.Add(file);
            return fbxFilePaths;
        }

        public static bool[] ToggleArrayChecks(bool[] array, bool condition)
        {
            for (var i = 0; i < array.Length; i++) array[i] = condition;

            return array;
        }

        public static string ToLabelName(string input)
        {
            var result = "";
            var prevUpper = false;

            foreach (var c in input)
                if (char.IsUpper(c))
                {
                    if (prevUpper || result.Length == 0)
                    {
                        result += c;
                    }
                    else
                    {
                        result += " " + c;
                        prevUpper = true;
                    }
                }
                else
                {
                    result += c;
                    prevUpper = false;
                }

            return result;
        }
    }
}