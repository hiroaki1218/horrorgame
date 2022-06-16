using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Memo : MonoBehaviour
{
    [SerializeField] private GameObject CrosshairUI;
    [SerializeField] private GameObject MemoUI;
    [SerializeField] private Text massage;
    public static bool LookMemo;
    public static bool Memo1;
    public static bool Memo2;
    public static bool Memo3;
    public static bool Memo4;
    public static bool Memo5;
    public static bool Memo6;
    GameObject player;
    FirstPersonControllerCustom _fpc;
    void Start()
    {
        LookMemo = false;
        player = GameObject.Find("FPSController");
        _fpc = player.GetComponent<FirstPersonControllerCustom>();
        Memo1 = false;
        Memo2 = false;
        MemoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Memo1)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "���͂��̊قɕ����߂��Ă��܂����B\n�������̏ꏊ����E�o���Ȃ����...�����ɏP���Ă��܂�...�B" +
                "\n��ɗ����l�̂��߂ɖ��ɗ��ł��낤�������c���Ă������B";
            MemoUI.SetActive(true);
        }
        else if (Memo2)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "�x�b�h���������񂠂镔���ɓ�K�̌������邾�낤�B";
            MemoUI.SetActive(true);
        }
        else if (Memo3)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "�������Ă���Ƃ���Ƀq���g�����邾�낤�B";
            MemoUI.SetActive(true);
        }
        else if (Memo4)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "��K�̃x�����_�ɐH���̃J�M�����邾�낤�B";
            MemoUI.SetActive(true);
        }
        else if (Memo5)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "OO�Ɍ��ւ̌�";
            MemoUI.SetActive(true);
        }
        else if (Memo6)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // �W�����[�h
            Cursor.visible = true;    // �J�[�\���\��
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "OO�ɖ�̌�";
            MemoUI.SetActive(true);
        }
        else
        {
            massage.text = null;
            MemoUI.SetActive(false);
        }
    }
    public void OnButton()
    {
        CrosshairUI.SetActive(true);
        LookMemo = false;
        Time.timeScale = 1;
        if (Memo1)
        {
            Memo1 = false;
            SlenderMove.instance.SlenderFirstMove();
        }
        Memo2 = false;
        Memo3 = false;
        Memo4 = false;
        Memo5 = false;
        Memo6 = false;
        _fpc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;     // �Œ胂�[�h
        Cursor.visible = false;    // �J�[�\����\��
    }
}
