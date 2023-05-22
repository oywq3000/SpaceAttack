using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class AssetLoader : MonoBehaviour
{
    enum AssetType
    {
        Text,
        Clip,
        GameObject,
        Lua,
        Sprite
    }
    
    public static AssetLoader Instance;
    private string _localAssetsUrl = Application.streamingAssetsPath + "/Assets/";
    private Dictionary<string, string> _localResVersion = new Dictionary<string, string>();
    private string _version = "Version.txt";
    private Dictionary<string, AssetBundle> _assetBundles = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Object> _objectCache = new Dictionary<string, Object>();

    private Dictionary<AssetType, string> _typeInventory = new Dictionary<AssetType, string>();

    private void Awake()
    {
        Instance = this;
        // _typeInventory.Add();
        _typeInventory.Add(AssetType.Lua,"Lua");
        _typeInventory.Add(AssetType.Clip,"Clip");
        _typeInventory.Add(AssetType.GameObject,"GameObject");
        _typeInventory.Add(AssetType.Sprite,"Sprite");
        
    }

    // Start is called before the first frame update
    void Start()
    {
       
        
    }


    public void LoadAB(string abName)
    {
        if (!_assetBundles.ContainsKey(abName))
        {
            var loadFromFile = AssetBundle.LoadFromFile(_localAssetsUrl + abName);
            _assetBundles.Add(abName, loadFromFile);
        }
    }

    public IEnumerator LoadAllAB()
    {
        //load all assetsbundle
        using (UnityWebRequest request = UnityWebRequest.Get(_localAssetsUrl + _version))
        {
            yield return request.SendWebRequest();
            if (request.downloadHandler.isDone)
            {
                //Parse version file from server
                ParseVersionFile(request.downloadHandler.text, _localResVersion);
            }
        }


        foreach (var abName in _localResVersion)
        {
            if (!_assetBundles.ContainsKey(abName.Key))
            {
                var loadFromFile = AssetBundle.LoadFromFile(_localAssetsUrl + abName.Key);
                _assetBundles.Add(abName.Key, loadFromFile);
            }
        }
    }

    public void LoadAssets()
    {
    }

    public void DisposeAB(string abName, bool unloadAllLoadedObjects = false)
    {
        if (_assetBundles.ContainsKey(abName))
        {
            _assetBundles[abName].Unload(unloadAllLoadedObjects);
            _assetBundles.Remove(abName);
        }
    }

    public void DisposeAllAB(bool unloadAllLoadedObjects = false)
    {
        foreach (var assetBundle in _assetBundles)
        {
            assetBundle.Value.Unload(unloadAllLoadedObjects);
            _assetBundles.Remove(assetBundle.Key);
        }
    }

    public T LoadAsset<T>(string abName, string assetName) where T : UnityEngine.Object
    {
        if (_objectCache.TryGetValue(abName, out Object value))
        {
            return value as T;
        }
        else if (_assetBundles.TryGetValue(abName, out AssetBundle assetBundle))
        {
            return assetBundle.LoadAsset<T>(assetName);
        }
        else
            return null;
    }

    public TextAsset LoadLuaText(string luaName)
    {
        return LoadAsset<TextAsset>(_typeInventory[AssetType.Lua], luaName);
    }
    public AudioClip LoadClip(string clipName)
    {
        return LoadAsset<AudioClip>(_typeInventory[AssetType.Clip], clipName);
    }
    public Sprite LoadSprite(string spriteName)
    {
        return LoadAsset<Sprite>(_typeInventory[AssetType.Sprite], spriteName);
    }

    public GameObject LoadGameObject(string objectName)
    {
        return LoadAsset<GameObject>(_typeInventory[AssetType.GameObject], objectName);
    }

    // Update is called once per frame
    void Update()
    {
    }

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
}