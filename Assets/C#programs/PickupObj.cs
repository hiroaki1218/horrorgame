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

    // Start is called before the first frame update
    void Start()
    {
        fpsLight = false;
        isCollect = false;
        Action = false;
        //itemTypeに応じてitemを生成する
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
                //ItemSetActive(false)
                ItemBox.instance.SetItem(item);
                //拾ったオブジェクトがFlashlightだったら、FPSのライトをオンにする
                if(item.type == Items.Type.Flashlight)
                {
                    fpsLight = true;
                }
                ThisItem.SetActive(false);
                isCollect = true;
            }

        }
        //CollectされたらUIを消す
        if (isCollect)
        {
            CollectUI.SetActive(false);
        }
    }
}