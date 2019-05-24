using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : Manager<MapController>
{
    public GameObject OutdoorMap;
    public GameObject Floor1Map;
    public GameObject Floor2Map;

    public Animator playerMoveAnimator;
    public string[] mapNames;

    GameObject[] Maps;
    int nowMap;

    void Start()
    {
        Maps = new GameObject[] { OutdoorMap, Floor1Map, Floor2Map };
        nowMap = 0;
        GameManager.Instance.PlayerMoveFront.AddListener(PlayerMoveFront);
        GameManager.Instance.PlayerMoveBack.AddListener(PlayerMoveBack);
        GameManager.Instance.goToNextPoint.AddListener(GoToNextPoint);
        MoveController.Instance.InsCheckPoint.AddListener(InsCheckPoint);
    }

    void Update()
    {
    }

    public void nextMap()
    {
        if (nowMap >= 2) return;

        Maps[nowMap].SetActive(false);
        nowMap += 1;
        Maps[nowMap].SetActive(true);
        Debug.Log("Map ID" + nowMap);
        //change animation 
        playerMoveAnimator.Play(mapNames[nowMap]);
    }

    void GoToNextPoint(){
        //動画を再生 
        playerMoveAnimator.speed=1;

    }
    void InsCheckPoint(){
        //動画を停止
        playerMoveAnimator.speed=0;
    }

     void PlayerMoveBack(){
        //動画を再生
        playerMoveAnimator.speed=1;
    }

     void PlayerMoveFront(){
        //動画を停止
        playerMoveAnimator.speed=0;
    }
}
