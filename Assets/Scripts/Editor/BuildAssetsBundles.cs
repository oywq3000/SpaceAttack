using System.IO;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Editor
{
    public class BuildAssetsBundles
    {
        [MenuItem("Assets/BuildAssetBundles")]
        static void BuildAB()
        {
            string localPath =Application.streamingAssetsPath +"/Assets";
            string serverPath = "WebServer/Assets";
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
       
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
           
            
           // BuildPipeline.BuildAssetBundles(localPath, BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows64);
            BuildPipeline.BuildAssetBundles(serverPath,BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
          //  CreateFileList(localPath);
            CreateFileList(serverPath);
        }
        
      
        
        
        
        
        /// <summary>
        /// Create FileList
        /// </summary>
        static void CreateFileList(string outPath)
        {
            string filePath = outPath + "\\Version.txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            StreamWriter streamWriter = new StreamWriter(filePath);

            string[] files = Directory.GetFiles(outPath);
            for (int i = 0; i < files.Length; i++)
            {
                string tmpfilePath = files[i];
                if (tmpfilePath.EndsWith(".manifest"))
                {
                    tmpfilePath.Replace("\\", "");
                    var substring = tmpfilePath.Substring(outPath.Length+1);
                    var fileName = substring.Substring(0, substring.Length - 9);
                    streamWriter.WriteLine(fileName+ "|" + GetFileCRC(tmpfilePath));
                }
              
            }
            streamWriter.Close();
            streamWriter.Dispose();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// get file's MD5
        /// </summary>
        static System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        static string GetFileCRC(string filePath)
        {
           
            var readLines = File.ReadAllLines(filePath);
            var strings = readLines[1].Split(':');
            if (strings.Length>1)
            {
               return strings[1].Substring(1);
            }
            return null;
        }
    }
}