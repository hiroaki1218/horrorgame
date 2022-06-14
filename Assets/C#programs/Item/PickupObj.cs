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
    public bool MainScene;

    // Start is called before the first frame update
    void Start()
    {
        collectPhone = false;
        fpsLight = false;
        isCollect = false;
        Action = false;
        CollectUI.SetActive(false);
        //itemType�ɉ�����item�𐶐�����
        item = ItemGenerater.instance.Spawn(itemType);
        if (MainScene)
        {
            StartSetItem();
        }
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
                        //�E�����I�u�W�F�N�g��Flashlight��������AFPS�̃��C�g���I���ɂ���
                        if (item.type == Items.Type.Flashlight)
                        {
                            fpsLight = true;
                        }
                        //�E�����I�u�W�F�N�g��Phone��������A����3�𖞂���
                        if (item.type == Items.Type.Phone)
                        {
                            collectPhone = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo1��������AMemo1���o��
                        if(item.type == Items.Type.Memo1)
                        {
                            Memo.Memo1 = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo2��������AMemo2���o��
                        if (item.type == Items.Type.Memo2)
                        {
                            Memo.Memo2 = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo3��������AMemo3���o��
                        if (item.type == Items.Type.Memo3)
                        {
                            Memo.Memo3 = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo4��������AMemo4���o��
                        if (item.type == Items.Type.Memo4)
                        {
                            Memo.Memo4 = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo5��������AMemo5���o��
                        if (item.type == Items.Type.Memo5)
                        {
                            Memo.Memo5 = true;
                        }
                        //�E�����I�u�W�F�N�g��Memo6��������AMemo6���o��
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
        //Collect���ꂽ��UI������
        if (isCollect)
        {
            CollectUI.SetActive(false);
        }
    }
}