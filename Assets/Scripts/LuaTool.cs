using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

namespace Script
{
    public class LuaTool
    {
        private LuaEnv _luaEnv;

        public void DoString(string str)
        {
            if (_luaEnv is null)
            {
                _luaEnv = new LuaEnv();
            }

            _luaEnv.AddLoader(MyLoader);

            _luaEnv.DoString($"require '{str}'");
        }

        private byte[] MyLoader(ref string filePath)
        {
            //  Debug.Log(filePath);
            // return System.Text.Encoding.UTF8.GetBytes(filePath);

            string path = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Lua\\" + filePath + ".lua";

            //transform to binary file 
            //  return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));

#if UNITY_EDITOR
            return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
#else
          return System.Text.Encoding.UTF8.GetBytes(AssetLoader.Instance.LoadLuaText(filePath).text.ToString());
#endif
        }

        public void Dispose()
        {
            _luaEnv.Dispose();
        }
    }
}