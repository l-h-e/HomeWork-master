using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Manager<UIManager>
{
    public Graphic talkPanel;
    public CanvasGroup loadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        loadingPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TalkUIUpdata(string talk)
    {
        if (talkPanel.gameObject.activeSelf != true)
        {
            //TalkPanelを開く
            talkPanel.gameObject.SetActive(true);
        }
        //トークUIを更新する
        talkPanel.GetComponentInChildren<Text>().text = talk;
    }
    public void hideTalk()
    {
        //トークUIを隠す
        talkPanel.gameObject.SetActive(false);
    }

    public void LoadingFade()
    {
        loadingPanel.DOFade(0, 2).OnComplete(() =>
        {
            loadingPanel.gameObject.SetActive(false);
        });
    }
}
