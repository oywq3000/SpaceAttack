using System.Collections;
using System.Collections.Generic;
using EventStruct;
using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region Button Event Register

        transform.Find("StartPanel/LiftSwitchBtn").GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                Debug.Log("Lift");
                GameManager.Instance.SendEvent(new SwitchInfo()
                {
                    switchButton = SwitchButton.Left
                });
            });

        transform.Find("StartPanel/RightSwitchBtn").GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                GameManager.Instance.SendEvent(new SwitchInfo()
                {
                    switchButton = SwitchButton.Right
                });
            });

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
    }
}