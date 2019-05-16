using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Manager<GameManager>
{




    public List<CheckPoint> checkPoints = new List<CheckPoint>();
    public UnityEvent goToNextPoint;

    private int nowCheckPoint = 0;

    // Start is called before the first frame update
    void Start() {
        nowCheckPoint = 0;
        goToNextPoint = new UnityEvent();
        goToNextPoint.AddListener(MoveController.Instance.MoveToNextPoint);
    }

    // Update is called once per frame

    void Update() {
        
    }

    public void nextCheckPoint() {
        //終点じゃなければ
        if(nowCheckPoint < checkPoints.Count - 1) {

            nowCheckPoint++;
            // マップ切替
            MapController.Instance.nextMap();
            //動画再生

            goToNextPoint.Invoke();
        } else {

            Debug.LogWarning("at finish point!");
        }
    }


    public void InsCheckPoint() {
        Instantiate(checkPoints[nowCheckPoint],Vector3.zero,Quaternion.identity);
        Debug.Log("enter check point"+nowCheckPoint);

    }

    public int NowCheckPoint()
    {
        return nowCheckPoint;
    }
}
