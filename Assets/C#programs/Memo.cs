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
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "私はこの館に閉じ込められてしまった。\n早くこの場所から脱出しなければ...魔物に襲われてしまう...。" +
                "\n後に来た人のために役に立つであろうメモを残しておこう。";
            MemoUI.SetActive(true);
        }
        else if (Memo2)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "ベッドがたくさんある部屋に二階の鍵があるだろう。";
            MemoUI.SetActive(true);
        }
        else if (Memo3)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "音が鳴っているところにヒントがあるだろう。";
            MemoUI.SetActive(true);
        }
        else if (Memo4)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "二階のベランダに食堂のカギがあるだろう。";
            MemoUI.SetActive(true);
        }
        else if (Memo5)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "OOに玄関の鍵";
            MemoUI.SetActive(true);
        }
        else if (Memo6)
        {
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "OOに門の鍵";
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
        Cursor.lockState = CursorLockMode.Locked;     // 固定モード
        Cursor.visible = false;    // カーソル非表示
    }
}
