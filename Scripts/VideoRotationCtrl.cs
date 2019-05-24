using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoRotationCtrl : MonoBehaviour
{
    Animator rotationAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rotationAnimator=GetComponent<Animator>();
        MoveController.Instance.InsCheckPoint.AddListener(pauseVideo);
        GameManager.Instance.goToNextPoint.AddListener(playVideo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pauseVideo(){
        rotationAnimator.speed=0;
    }
    void playVideo(){
        rotationAnimator.speed=1;
    }
}
