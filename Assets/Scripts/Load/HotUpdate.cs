using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EventStruct;
using UnityEngine;
using UnityEngine.Networking;

public class HotUpdate : MonoBehaviour
{
    private static string _ip = "http://127.0.0.1:9501";
    private string _localAssetsUrl = Application.streamingAssetsPath + "/Assets/";
    private string _serverAssetsUrl = _ip + "/Assets/";
    private string _version = "Version.txt";
    
    private Dictionary<string, string> _localResVersion = new Dictionary<string, string>();
    private Dictionary<string, string> _serverResVersion = new Dictionary<string, string>();
    private List<string> _neededDownLoadList = new List<string>();

    private int _updateCount = 0;

    public bool isChecked = false;
    public bool isUpdated = false;
    
    
    private void Start()
    {
       
    }

    public void Check()
    {
       StartCoroutine( CheckAssetsUpdate());
    }

    public void UpdateAll()
    {
        UpdateAssets();
    }

    IEnumerator CheckAssetsUpdate()
   {
       //get server version file
       using (UnityWebRequest request = UnityWebRequest.Get(_serverAssetsUrl + _version))
       {
           yield return request.SendWebRequest();
           
           //prevent network error
           if (request.isNetworkError)
           {
               yield return null;
           }
           Debug.Log("DownLoad Server Version file:"+_serverAssetsUrl + _version);
           Debug.Log("Is Down:"+request.downloadHandler.text);
           if (request.downloadHandler.isDone)
           {
               Debug.Log("Is Down:"+request.downloadHandler.text);
               //Parse version file from server
               ParseVersionFile(request.downloadHandler.text, _serverResVersion);
           }
       }
       //get local version file
       using (UnityWebRequest request = UnityWebRequest.Get(_localAssetsUrl + _version))
       {
         
           yield return request.SendWebRequest();
           if (request.downloadHandler.isDone)
           {
               //Parse version file from server
               ParseVersionFile(request.downloadHandler.text, _localResVersion);
           }
       }
       
       //check and compare 
       CompareVersion();
   }

   private void CompareVersion()
   {
       foreach (var version in _serverResVersion)
       {
           string fileName = version.Key; //assetbundleName
           string serverMd5 = version.Value; // asset MD5

           //need add assets
           if (!_localResVersion.ContainsKey(fileName))
           {
               _neededDownLoadList.Add(fileName);
           }
           else
           {
               //assets that needs to be replace
               string localMd5;
               _localResVersion.TryGetValue(fileName, out localMd5);
               if (!serverMd5.Equals(localMd5))
               {
                   _neededDownLoadList.Add(fileName);
               }
           }
       }

       if (_neededDownLoadList.Count==0)
       {
           //no resource need load
           isUpdated = true;
       
       }
       Debug.Log("Needed Update count: "+_neededDownLoadList.Count);
       isChecked = true;
   }




   void UpdateAssets()
   {
       
       foreach (var neededDownLoad in _neededDownLoadList)
       {
           Debug.Log("Update: "+neededDownLoad);
           StartCoroutine(DownloadAssetBundleAndSave(_serverAssetsUrl,neededDownLoad,OnCompleteOneUpdate));
       }
   }

  
    IEnumerator DownloadAssetBundleAndSave(string url, string name, Action saveLocalComplate = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(url + name);
        yield return request.SendWebRequest();
        
        Debug.Log("DownLoad: "+url + name);
        if (request.downloadHandler.isDone)
        {
            SaveAssetBundle(name, request.downloadHandler.data, saveLocalComplate);
        }
    }

    void SaveAssetBundle(string fileName, byte[] bytes, Action saveLocalComplate = null)
    {
        string path =_localAssetsUrl + fileName;
        FileInfo fileInfo = new FileInfo(path);
        FileStream fs = fileInfo.Create();
        fs.Write(bytes, 0, bytes.Length);
        fs.Flush();  
        fs.Close();  
        fs.Dispose();
        if (saveLocalComplate != null)
        {
            saveLocalComplate();
        }
    }
    
    //preserve version.txt
    private void ParseVersionFile(string content, Dictionary<string, string> dict)
    {
        if (string.IsNullOrEmpty(content))
        {
            return;
        }
 
        string[] items = content.Split('\n');
        foreach (string item in items)
        {
            string str = item.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            string[] info = str.Split('|');
            
            //if info length equals 2 indicates parse sucessfully
            if (info.Length == 2)
            {
                dict.Add(info[0], info[1]);
            }
        }
    }


    void OnCompleteOneUpdate()
    {
        _updateCount++;
        if (_updateCount==_neededDownLoadList.Count)
        {
            //resource update complete
            StartCoroutine(DownloadAssetBundleAndSave(_serverAssetsUrl,_version,OnCompleteOneUpdate));
        }

        if (_updateCount==_neededDownLoadList.Count+1)
        {
            //all is already update
            isUpdated = true;
        }
        
    }
}
