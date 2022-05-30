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
                    if(item.type == Items.Type.Flashlight)
                    {
                        fpsLight = true;
                    }
                    //�E�����I�u�W�F�N�g��Phone��������A����3�𖞂���
                    if(item.type == Items.Type.Phone)
                    {
                        collectPhone = true;
                    }
                }
                

                ThisItem.SetActive(false);
                isCollect = true;
            }

        }
        //Collect���ꂽ��UI������
        if (isCollect)
        {
            CollectUI.SetActive(false);
        }
    }
}