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

    public static bool mainTotitle = false;

    void Awake()
    {
        if (mainTotitle)
        {
           VideoCanvas.SetActive(false);
        }
        else
        {
           //�r�f�I�Đ����̃L�����o�X�̕\���ݒ�
           VideoCanvas.SetActive(true);
           videoPlayer.loopPointReached += LoopPointReached;
           videoPlayer.Play();           
           MainCanvas.SetActive(false);
           LoadCanvas.SetActive(false);
        }

    }

    // Update is called once per frame
    public void LoopPointReached(VideoPlayer vp)
    {
        // �r�f�I�I����̏���
        VideoCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
