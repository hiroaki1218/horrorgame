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
    private bool Active;
    public static bool inventory;
    FirstPersonControllerCustom fpc;

    public void Start()
    {
        InvUI.SetActive(false);
        Active = false;
        inventory = false;
    }

    public void Update()
    {
        fpc = player.GetComponent<FirstPersonControllerCustom>();
        if (!Menu.pause && !PhoneAnimation.isLookPhone && !Memo.LookMemo && !Memo.exitMemo1)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Active = !Active;
                if (Active)
                {
                    InvUI.SetActive(true);
                    fpc.enabled = false;
                    inventory = true;
                    Cursor.visible = true;     // カーソル表示
                    Cursor.lockState = CursorLockMode.None;     // 標準モード
                }
                else if (!Active)
                {
                    InvUI.SetActive(false);
                    fpc.enabled = true;
                    inventory = false;
                    Cursor.visible = false;     // カーソル非表示
                    Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
                }
            }
            
        }
        if (ItemBox.instance.CheckSelectItem(Items.Type.Memo1))
        {
            text.text = "メモ\n私はこの館に閉じ込められてしまった。\n早くこの場所から脱出しなければ...魔物に襲われてしまう...。" +
                "\n後に来た人のために役に立つであろうメモを残しておこう。";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo2))
        {
            text.text = "メモ\nベッドがたくさんある部屋に二階の鍵があるだろう。";
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
        else
        {
            text.text = null;
        }
    }
}
