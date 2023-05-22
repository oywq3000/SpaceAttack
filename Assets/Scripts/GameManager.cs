using System;
using System.Collections;
using System.Collections.Generic;
using EventStruct;
using Script;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

[Hotfix]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private IEventHolder _eventHolder;
  
    [HideInInspector] public LuaTool luaTool;


    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        #region Init

        _eventHolder = new EventHolder();
        luaTool = new LuaTool();

        #endregion


        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterEvent<OnUpdated>(AfterUpdate);
    }

    void AfterUpdate(OnUpdated e)
    {
        AssetLoader.Instance.LoadAB("lua");
        
        //start hotfix
        luaTool.DoString("Main");

        //entry start scene
        SceneManager.LoadScene("Start");
    }
    

    public IUnRegister RegisterEvent<T>(Action<T> action) where T : new()
    {
        return _eventHolder.Register<T>(action);
    }

    public void UnRegisterEvent<T>(Action<T> action) where T : new()
    {
        _eventHolder.UnRegister<T>(action);
    }

    public void SendEvent<T>(T t) where T : new()
    {
        _eventHolder.Send(t);
    }

    public void SendEvent<T>() where T : new()
    {
        _eventHolder.Send<T>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}