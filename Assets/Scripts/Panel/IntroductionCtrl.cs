using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventStruct;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionCtrl : MonoBehaviour
{
    public CanvasGroup startUIGroup;

    public Text processing;

    private HotUpdate _hotUpdate;
    // Start is called before the first frame update
    void Start()
    {
        _hotUpdate = GetComponent<HotUpdate>();
        
        
        startUIGroup.DOFade(1, 2).onComplete+= () =>
        {
            StartCoroutine(Procssing());
        };
    }

    IEnumerator Procssing()
    {
        processing.gameObject.SetActive(true);
        processing.text = "正在检测更新...";
        _hotUpdate.Check();
        yield return new WaitForSeconds(0.5f);
        yield return _hotUpdate.isChecked;
        processing.text = "正在下载资源...";
        yield return new WaitForSeconds(0.5f);
        _hotUpdate.UpdateAll();
        yield return _hotUpdate.isUpdated;
        processing.text = "更新完成...";
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.SendEvent(new OnUpdated());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
