using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MonsterLove.StateMachine;
using System.Linq;

public class GameManager : Manager<GameManager>
{
    enum GameState
    {
        Prepare,
        Moving,
        InPoint
    }
    public PlayerController playerController;
    StateMachine<GameState> gameFSM;
    public List<CheckPoint> checkPoints = new List<CheckPoint>();
    public UnityEvent goToNextPoint= new UnityEvent();
   
    public UnityEvent PlayerMoveBack = new UnityEvent();
    public UnityEvent PlayerMoveFront = new UnityEvent();

    public int[] MapChangePoint=new int[2];//どこでマップを変える
    public bool initIsMoving;
    public bool isMoving
    {
        get ;
        private set;
    }

    private int nowCheckPoint = 0;

    // Start is called before the first frame update

    new void Awake()
    {
        base.Awake();
        gameFSM = StateMachine<GameState>.Initialize(this);
        gameFSM.ChangeState(GameState.Prepare);

    }

    void Start()
    {
        isMoving=initIsMoving;
        nowCheckPoint = 0;
        goToNextPoint.AddListener(MoveController.Instance.MoveToNextPoint);
        PlayerMoveFront.AddListener(MoveController.Instance.PlayerMoveFront);
        PlayerMoveBack.AddListener(MoveController.Instance.PlayerMoveBack);

    }

    // Update is called once per frame

    void Update()
    {

    }

    public void nextCheckPoint()
    {
        //終点じゃなければ
        if (nowCheckPoint < checkPoints.Count - 1)
        {
            gameFSM.ChangeState(GameState.Moving);

        }
        else
        {

            Debug.LogWarning("at finish point!");
        }
    }

    void Moving_Enter()
    {
        
        isMoving = true;
        nowCheckPoint++;
        // マップ切替
        if(MapChangePoint.Contains(nowCheckPoint)){
            MapController.Instance.nextMap();
            Debug.Log("changedMap");
        }
        
        

        goToNextPoint.Invoke();
    }
    void Moving_Update()
    {
      //  if (!playerController.isPlayerFront())
      //  {
          //  PlayerMoveBack.Invoke();
       // }
       // else
       // {
          //  PlayerMoveFront.Invoke();
       // }
    }

    void InPoint_Enter()
    {
        isMoving = false;
        MoveController.Instance.InsCheckPoint.Invoke();
        Debug.Log(nowCheckPoint);
        Instantiate(checkPoints[nowCheckPoint], Vector3.zero, Quaternion.identity);
        Debug.Log("enter check point" + nowCheckPoint);
    }
    public void InsCheckPoint()
    {
        gameFSM.ChangeState(GameState.InPoint);
    }
    public int NowCheckPoint()
    {
        return nowCheckPoint;
    }


}
