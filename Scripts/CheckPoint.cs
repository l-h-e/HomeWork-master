using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterLove.StateMachine;

public class CheckPoint : MonoBehaviour
{
    
    public enum CheckPointStates
    {
        prepare,
        TalkBeforeBattle,
        Battle,
        TalkAffteBattle
    }
    StateMachine<CheckPointStates> checkPointFSM;

    [SerializeField]
    string[] talkBeforeBattle;//バトル開始前の話
    [SerializeField]
    string[] talkAffteBattle;
    public GameObject[] EnemyGroup;//ドローンがここにある
    Button mahoujin;
    IEnumerator talkBeforeIenu;
    IEnumerator talkAffteIenu;

    public GameObject shooter;
    // Start is called before the first frame update
    private int nowEnemyNum;

    void Awake()
    {
        checkPointFSM = StateMachine<CheckPointStates>.Initialize(this);
        shooter=GameObject.Find("Shooter");
        checkPointFSM.ChangeState(CheckPointStates.TalkBeforeBattle);
    }
    void Start()
    {
        
        mahoujin = GetComponentInChildren<Button>(true);
        mahoujin.onClick.AddListener(() => debugCheck());

        talkAffteIenu = talkAffteBattle.GetEnumerator();
        nowEnemyNum = EnemyGroup.Length;

    }

    void TalkBeforeBattle_Enter()
    {
        shooter.SetActive(false);
        talkBeforeIenu = talkBeforeBattle.GetEnumerator();
        if (talkBeforeIenu.MoveNext())
            {//話はまだ終わっていない,UIに更新させて
                UIManager.Instance.TalkUIUpdata(talkBeforeIenu.Current as string);
            }

    }
    void TalkBeforeBattle_Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (talkBeforeIenu.MoveNext())
            {//話はまだ終わっていない,UIに更新させて
                UIManager.Instance.TalkUIUpdata(talkBeforeIenu.Current as string);
            }
            else
            {//話がおわり、バトル状態に入る

                checkPointFSM.ChangeState(CheckPointStates.Battle);
            }
        }
    }
    void TalkBeforeBattle_Exit()
    {
        shooter.SetActive(true);
        UIManager.Instance.hideTalk();
    }

    void Battle_Enter()
    {
        //初始化战斗场景
        foreach (GameObject x in EnemyGroup)
        {
            x.SetActive(true);
        }
    }
    void debugCheck()
    {

        Debug.Log("Point Clear!");
        GameManager.Instance.nextCheckPoint();
        Destroy(this.gameObject);

    }

    public void KilledEnemy()
    {
        nowEnemyNum -= 1;
        if (nowEnemyNum <= 0)
        {
            Debug.Log("Point Clear!");
            checkPointFSM.ChangeState(CheckPointStates.TalkAffteBattle);

        }
    }


    void TalkAffteBattle_Enter()
    {
        
        talkAffteIenu = talkAffteBattle.GetEnumerator();
        if (talkAffteIenu.MoveNext())
            {//話はまだ終わっていない,UIに更新させて
                UIManager.Instance.TalkUIUpdata(talkAffteIenu.Current as string);
            }

    }
    void TalkAffteBattle_Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (talkAffteIenu.MoveNext())
            {//話はまだ終わっていない,UIに更新させて
                UIManager.Instance.TalkUIUpdata(talkAffteIenu.Current as string);
            }
            else
            {//話がおわり、バトル状態に入る

                checkPointFSM.ChangeState(CheckPointStates.prepare);
            }
        }
    }
    void TalkAffteBattle_Exit()
    {
        
        UIManager.Instance.hideTalk();
        GameManager.Instance.nextCheckPoint();
        Destroy(this.gameObject);
    }



}
