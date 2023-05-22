using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace Script
{
    public class LuaTool
    {
        private  LuaEnv _luaEnv;

        public  void DoString(string str)
        {
            if (_luaEnv is null)
            {
                _luaEnv =new LuaEnv();
            }
            
            _luaEnv.AddLoader(MyLoader);
       
            _luaEnv.DoString($"require '{str}'");
        
        }
        private  byte[] MyLoader(ref string filePath)
        {
            //  Debug.Log(filePath);
            // return System.Text.Encoding.UTF8.GetBytes(filePath);

            string path = Directory.GetCurrentDirectory()+"\\Assets\\Script\\Lua\\"+filePath+".lua";
            
            //transform to binary file 
            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
        }


        public  Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
        void  LoadAB(string resName, string filePath)
        {
            AssetBundle ab = AssetBundle.LoadFromFile("AssetBundles/"+filePath);
            GameObject go = ab.LoadAsset<GameObject>(resName);
            _prefabs.Add(resName,go);
        }

      public  GameObject GetAB(string name)
        {
            if (_prefabs.TryGetValue(name,out GameObject gb))
            {
                return gb;
            }
            else
            {
                LoadAB(name, "AssetBundles/");
                return GetAB(name);
            }
        }
        public  void Dispose()
        {
            _luaEnv.Dispose();
        }
        
    }
}