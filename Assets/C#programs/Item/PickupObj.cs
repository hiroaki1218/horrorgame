using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    [SerializeField] private GameObject CollectUI;
    [SerializeField] private GameObject ThisItem;
    [SerializeField] Items.Type itemType;
    Items item;

    private bool Action;
    public bool isCollect;
    public static bool fpsLight;
    public static bool collectPhone;
    [SerializeField] public bool MainScene;
    private bool once;

    // Start is called before the first frame update
    void Start()
    {
        collectPhone = false;
        fpsLight = false;
        isCollect = false;
        Action = false;
        CollectUI.SetActive(false);
        //itemTypeに応じてitemを生成する
        item = ItemGenerater.instance.Spawn(itemType);
        if (MainScene)
        {
            StartSetItem();
        }
        once = true;
    }

    void StartSetItem()
    {
        item.type = Items.Type.Phone;
        ItemBox.instance.SetItem(item);
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            CollectUI.SetActive(true);
            Action = true;
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        CollectUI.SetActive(false);
        Action = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Action == true)
        {
            if(!Inventory.inventory && !Menu.pause && !Memo.LookMemo)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (itemType == Items.Type.Battery)
                    {
                        OffstFlashlight.instance.SetBattery();
                    }
                    else
                    {
                        //ItemSetActive(false)
                        ItemBox.instance.SetItem(item);
                        //拾ったオブジェクトがFlashlightだったら、FPSのライトをオンにする
                        if (item.type == Items.Type.Flashlight && once) 
                        {
                            fpsLight = true;
                            once = false;
                        }
                        //拾ったオブジェクトがPhoneだったら、条件3を満たす
                        if (item.type == Items.Type.Phone)
                        {
                            collectPhone = true;
                        }
                        //拾ったオブジェクトがMemo1だったら、Memo1を出す
                        if(item.type == Items.Type.Memo1)
                        {
                            Memo.Memo1 = true;
                        }
                        //拾ったオブジェクトがMemo2だったら、Memo2を出す
                        if (item.type == Items.Type.Memo2)
                        {
                            Memo.Memo2 = true;
                        }
                        //拾ったオブジェクトがMemo3だったら、Memo3を出す
                        if (item.type == Items.Type.Memo3)
                        {
                            Memo.Memo3 = true;
                        }
                        //拾ったオブジェクトがMemo4だったら、Memo4を出す
                        if (item.type == Items.Type.Memo4)
                        {
                            Memo.Memo4 = true;
                        }
                        //拾ったオブジェクトがMemo5だったら、Memo5を出す
                        if (item.type == Items.Type.Memo5)
                        {
                            Memo.Memo5 = true;
                        }
                        //拾ったオブジェクトがMemo6だったら、Memo6を出す
                        if (item.type == Items.Type.Memo6)
                        {
                            Memo.Memo6 = true;
                        }
                    }


                    ThisItem.SetActive(false);
                    isCollect = true;
                }
            }
        }
        //CollectされたらUIを消す
        if (isCollect)
        {
            CollectUI.SetActive(false);
        }
    }
}