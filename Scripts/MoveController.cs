using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class MoveController : Manager<MoveController>
{
    public float[] secondOfCheckPoint;
    VideoPlayer videoPlayer;
    public string fileName;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url=System.IO.Path.Combine (Application.streamingAssetsPath,fileName);
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (videoPlayer.frame >= SecondToFrame(secondOfCheckPoint[GameManager.Instance.NowCheckPoint()])&&videoPlayer.isPlaying)
        {
            //時間が次のポイントの時間を超えるとビデオが止まる
            videoPlayer.Pause();//新しいポイントに入って一旦止まって
            GameManager.Instance.InsCheckPoint();//新しいポイントを初期化する
        }
    }

    public void MoveToNextPoint()
    {
        if (videoPlayer.isPrepared)
        {
            Debug.Log("Moving");
            videoPlayer.Play();
            //動画を再生
        }
        else
        {
            Debug.LogError("hasnt Prepared");
        }
    }

    float SecondToFrame(float second)
    {
        return second * videoPlayer.frameRate;
    }

void PrepareCompleted(VideoPlayer vp)
    {
        
        vp.prepareCompleted -= PrepareCompleted;
        vp.Play();
        vp.Pause(); 
        UIManager.Instance.LoadingFade();
    }
}
