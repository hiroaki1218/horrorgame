using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Memo : MonoBehaviour
{
    [SerializeField] private GameObject FPCHeadMesh;
    [SerializeField] private GameObject CrosshairUI;
    [SerializeField] private GameObject MemoUI;
    [SerializeField] private Text massage;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera subCamera;
    [SerializeField] private Camera trainCamera;
    [SerializeField] private GameObject target;

    [SerializeField] private GameObject firstBarrier;
    [SerializeField] private GameObject _memo2;
    [SerializeField] private GameObject Hammer;
    [SerializeField] private GameObject RoomKey;
    [SerializeField] private Collider _pianocollider;
    [SerializeField] private GameObject _memo4;

    public static bool LookMemo;
    public static bool Memo1;
    public static bool Memo2;
    public static bool Memo3;
    public static bool Memo4;
    public static bool Memo5;
    public static bool Memo6;
    public static bool exitMemo1;
    public static bool SlenderCanMove;
    private bool looked;
    private bool One;
    private bool Once;
    GameObject player;
    FirstPersonControllerCustom _fpc;

    public static Memo instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        LookMemo = false;
        player = GameObject.Find("FPSController");
        _fpc = player.GetComponent<FirstPersonControllerCustom>();
        Memo1 = false;
        Memo2 = false;
        Memo3 = false;
        Memo4 = false;
        Memo5 = false;
        Memo6 = false;
        exitMemo1 = false;
        SlenderCanMove = false;
        looked = false;
        One = true;
        Once = true;
        MemoUI.SetActive(false);
        subCamera.enabled = false;
        _memo2.SetActive(false);
        Hammer.SetActive(false);
        RoomKey.SetActive(false);///falseに変更忘れず
        _pianocollider.enabled = false;
        _memo4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Memo1)
        {
            HammerAnimation.canShakewithMemo = false;
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            //Time.timeScale = 0;
            massage.text = "私はこの館に閉じ込められてしまった。\n早くこの場所から脱出しなければ...魔物に襲われてしまう...。" +
                "\n後に来た人のために役に立つであろうメモを残しておこう。";
            MemoUI.SetActive(true);
        }
        else if (Memo2)
        {
            HammerAnimation.canShakewithMemo = false;
            LookMemo = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
            _fpc.enabled = false;
            CrosshairUI.SetActive(false);
            Time.timeScale = 0;
            massage.text = "大広間の隣の部屋に二階の鍵があるだろう。";
            MemoUI.SetActive(true);
        }
        else if (Memo3)
        {
            HammerAnimation.canShakewithMemo = false;
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
            HammerAnimation.canShakewithMemo = false;
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
            HammerAnimation.canShakewithMemo = false;
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
            HammerAnimation.canShakewithMemo = false;
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

        //もしMemo1を見終わったら、カメラを動かす。
        if (exitMemo1)
        {
            StartCoroutine("CameraMove");
        }
        else
        {
            subCamera.transform.rotation = mainCamera.transform.rotation;
            subCamera.transform.position = trainCamera.transform.position;
        }
        //もしピアノを弾き終わったら、メモ４見える
        if (Piano.pianoPushed && Once)
        {
            _memo4.SetActive(true);
            Once = false;
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
            exitMemo1 = true;
            _fpc.enabled = false;
            firstBarrier.SetActive(false);
            SlenderMove.instance.SlenderFirstMove();

            _memo2.SetActive(true);
        }
        if (Memo2)
        {
            _fpc.enabled = true;
            SlenderMove.instance.slenderfalse = true;
            Hammer.SetActive(true);
            Memo2 = false;
        }
        if (Memo3)
        {
            _fpc.enabled = true;
            Memo3 = false;
            _pianocollider.enabled = true;
        }
        if (Memo4)
        {
            _fpc.enabled = true;
            Memo4 = false;
            SlenderCanMove = true;
            RoomKey.SetActive(true);
        }
        if (Memo5)
        {
            _fpc.enabled = true;
            Memo5 = false;
        }
        if (Memo6)
        {
            _fpc.enabled = true;
            Memo6 = false;
        }
        
        //_fpc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;     // 固定モード
        Cursor.visible = false;    // カーソル非表示
        StartCoroutine(CanItemUse());
    }
    IEnumerator CanItemUse()
    {
        yield return new WaitForSeconds(1);
        HammerAnimation.canShakewithMemo = true;
    }
    IEnumerator CameraMove()
    {
        Vector3 direction = target.transform.position - subCamera.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        mainCamera.enabled = false;
        subCamera.enabled = true;
        if (!looked)
        {
            FPCHeadMesh.SetActive(false);
            subCamera.transform.rotation = Quaternion.Slerp(subCamera.transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
        else
        {
            subCamera.transform.position = mainCamera.transform.position;
            subCamera.transform.rotation = Quaternion.Slerp(subCamera.transform.rotation, mainCamera.transform.rotation, 5 * Time.deltaTime);
        }
        yield return new WaitForSeconds(3.4f);
        looked = true;
        yield return new WaitForSeconds(0.5f);
        if (One)
        {
            One = false;
            _fpc.enabled = true;
            mainCamera.enabled = true;
            subCamera.enabled = false;
            FPCHeadMesh.SetActive(true);
        }
        exitMemo1 = false;
    }
}
