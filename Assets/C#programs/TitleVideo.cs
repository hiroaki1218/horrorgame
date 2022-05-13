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

    void Start()
    {
        //ビデオ再生時のキャンバスの表示設定
        MainCanvas.SetActive(false);
        VideoCanvas.SetActive(true);
        LoadCanvas.SetActive(false);
        videoPlayer.loopPointReached += LoopPointReached;
        videoPlayer.Play();
    }

    // Update is called once per frame
    public void LoopPointReached(VideoPlayer vp)
    {
        // ビデオ終了後の処理
        VideoCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
