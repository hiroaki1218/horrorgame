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
        //�r�f�I�Đ����̃L�����o�X�̕\���ݒ�
        MainCanvas.SetActive(false);
        VideoCanvas.SetActive(true);
        LoadCanvas.SetActive(false);
        videoPlayer.loopPointReached += LoopPointReached;
        videoPlayer.Play();
    }

    // Update is called once per frame
    public void LoopPointReached(VideoPlayer vp)
    {
        // �r�f�I�I����̏���
        VideoCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
