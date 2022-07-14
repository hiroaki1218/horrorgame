using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderAttack : MonoBehaviour
{
    public static bool canCheckTabandItems;
    void Start()
    {
        canCheckTabandItems = true;
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) //プレイヤーがトリガーに入ったとき
    {
        if(other.tag == "Player")
        {
            if (!SlenderMove.instance.secoundmove)
            {
                canCheckTabandItems = false;
                //もしスマホを開いていたら、バツボタンを押したときと同じようにActiveをfalseにする
                if (PhoneAnimation.Active)
                {
                    PhoneAnimation.instance.OnclickExitButton();
                }
                //もしtabを開いていたら、Activeをfalseにする
                if (Inventory.Active)
                {
                    Inventory.instance.OnClickTab();
                }
            }
            else
            {
                canCheckTabandItems = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canCheckTabandItems = true;
        }
    }
}
