using System.Collections;
using System.Collections.Generic;
using EventStruct;
using Script.Utility;
using UnityEngine;

public class ShipBoxController : MonoBehaviour
{

    private int _currentCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.RegisterEvent<SwitchInfo>(OnChangeShip)
            .UnRegisterOnDestroy(gameObject);
        
        InitDisplay(0);
    }

    void InitDisplay(int order)
    {
        transform.GetChild(order).gameObject.SetActive(true);
    }

    void OnChangeShip(SwitchInfo switchInfo)
    {
        Debug.Log("fds");
        bool isLift;
        if (switchInfo.switchButton == SwitchButton.Left)
        {
            _currentCount--;
            isLift = true;
        }
        else
        {
            _currentCount++;
            isLift = false;
        }

        if (isLift)
        {
            transform.GetChild(_currentCount+1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(_currentCount-1).gameObject.SetActive(false);
        }
        
        
        if (_currentCount<0)
        {
            _currentCount = transform.childCount-1;
        }

        if (_currentCount>=transform.childCount)
        {
            _currentCount = 0;
        }

        Debug.Log("Current Index:"+_currentCount);
        transform.GetChild(_currentCount).gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
