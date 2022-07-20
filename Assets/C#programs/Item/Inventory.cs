using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InvUI;
    [SerializeField] private GameObject player;
    [SerializeField] private Text text;

    //アイテムを使用するかしないか
    [SerializeField] private GameObject UseItemUI;
    [SerializeField] private Text useItemText;
    [SerializeField] public Button[] ItemCheckButton;
    public static int k;
    public static int r;

    public static bool Active;
    public static bool inventory;
    public static bool canPushTab;
    public static Inventory instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        SlenderAttack.canCheckTabandItems = true;
        InvUI.SetActive(false);
        UseItemUI.SetActive(false);
        Active = false;
        inventory = false;
        useItemText.text = null;
        r = 0;
        canPushTab = false;
    }

    public void Update()
    {
        if (!Menu.pause && !PhoneAnimation.isLookPhone && !Memo.LookMemo && !Memo.exitMemo1 && !canPushTab && !SubSceneStartMove.SubSceneFirstMoving)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OnClickTab();
            }  
        }
        if (ItemBox.instance.CheckSelectItem(Items.Type.Memo1))
        {
            text.text = "メモ\n私はこの館に閉じ込められてしまった。\n早くこの場所から脱出しなければ...魔物に襲われてしまう...。" +
                "\n後に来た人のために役に立つであろうメモを残しておこう。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo2))
        {
            text.text = "メモ\n大広間の隣の部屋に二階の鍵があるだろう。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo3))
        {
            text.text = "メモ\n音が鳴っているところにヒントがあるだろう。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo4))
        {
            text.text = "メモ\n二階のベランダに食堂のカギがあるだろう。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo5))
        {
            text.text = "メモ\nOOに玄関の鍵";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo6))
        {
            text.text = "メモ\nOOに門の鍵";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Phone))
        {
            text.text = "スマートフォン\n近くの監視カメラの映像を見られる。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Flashlight))
        {
            text.text = "懐中電灯\nバッテリーの消費は激しいが、明るい。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Hammer))
        {
            text.text = "ハンマー\n木の板を破壊することができる。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.RoomKey))
        {
            text.text = "部屋の鍵\nどこかで使えないだろうか...。";
        }
        else
        {
            text.text = null;
            useItemText.text = null;
            UseItemUI.SetActive(false);
        }
    }
    public void OnClickTab()
    {
        if (SlenderAttack.canCheckTabandItems)
        {
            Active = !Active;
        }
        else
        {
            Active = false;
        }
        if (Active)
        {
            InvUI.SetActive(true);
            MouseLook.XSensitivity = 0;
            MouseLook.YSensitivity = 0;
            inventory = true;
            Cursor.visible = true;     // カーソル表示
            Cursor.lockState = CursorLockMode.None;     // 標準モード
        }
        else if (!Active)
        {
            InvUI.SetActive(false);
            MouseLook.XSensitivity = 0.5f;
            MouseLook.YSensitivity = 0.5f;
            inventory = false;
            Cursor.visible = false;     // カーソル非表示
            Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
            OnClickBackButton();
        }
    }
    //アイテムを使用するかどうか
    public void OnClickSomeItem()
    {
        if (ItemBox.instance.CheckSelectItem(Items.Type.Phone))
        {
            UseItemUI.SetActive(true);
            useItemText.text = "スマートフォンを使用しますか？";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Hammer))
        {
            UseItemUI.SetActive(true);
            useItemText.text = "ハンマーを使用しますか？";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.RoomKey))
        {
            UseItemUI.SetActive(true);
            useItemText.text = "部屋の鍵を使用しますか？";
        }
        else
        {
            UseItemUI.SetActive(false);
            useItemText.text = null;
        }
    }
    public void OnclickYesButton()
    {
        if (ItemBox.instance.CheckSelectItem(Items.Type.Phone))
        {
            canPushTab = true;
            OnClickBackButton();
            InvUI.SetActive(false);
            inventory = false;
            PhoneAnimation.instance.OnclickYesButton();
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Hammer))
        {
            canPushTab = true;
            OnClickTab();
            HammerAnimation.instance.OnClickYesButon();
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.RoomKey))
        {
            if (UseRoomKey.instance.canUseRoomKey)
            {
                OnClickTab();
                UseRoomKey.instance.Active = true;
            }
            else
            {
                OnClickTab();
                Debug.Log("ここでは使えない");
            }
        }
    }
    public void OnClickBackButton()
    {
        UseItemUI.SetActive(false);
    }
}
