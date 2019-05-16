using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterLove.StateMachine;

public class CheckPoint : MonoBehaviour
{
    public enum CheckPointStates
    {
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
    IEnumerator talkAfterIenu;
    // Start is called before the first frame update

    void Awake()
    {
        checkPointFSM = StateMachine<CheckPointStates>.Initialize(this);
        checkPointFSM.ChangeState(CheckPointStates.TalkBeforeBattle);
    }
    void Start()
    {
        mahoujin = GetComponentInChildren<Button>(true);
        mahoujin.onClick.AddListener(() => debugCheck());

        talkAfterIenu = talkAffteBattle.GetEnumerator();

    }

    void TalkBeforeBattle_Enter()
    {
        talkBeforeIenu = talkBeforeBattle.GetEnumerator();

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
}
