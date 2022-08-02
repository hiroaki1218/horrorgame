using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleVideo : MonoBehaviour
{
    [SerializeField] 
    VideoPlayer videoPlayer;
    [SerializeField]
    private GameObject MainCanvas;
    [SerializeField]
    private GameObject VideoCanvas;
    [SerializeField]
    private GameObject LoadCanvas;
    [SerializeField]
    private GameObject WaitCanvas;


    void Awake()
    {
        //ビデオ再生時のキャンバスの表示設定
        VideoCanvas.SetActive(true);
        videoPlayer.loopPointReached += LoopPointReached;
        videoPlayer.Play();           
        MainCanvas.SetActive(false);
        LoadCanvas.SetActive(false);
    }

    // Update is called once per frame
    public void LoopPointReached(VideoPlayer vp)
    {
        // ビデオ終了後の処理
        VideoCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
