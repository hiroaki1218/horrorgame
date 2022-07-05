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
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //もしメモを見ていたら、バツボタンを押した時と同じようにする
            if (Memo.Memo1 || Memo.Memo2 || Memo.Memo3 || Memo.Memo4 || Memo.Memo5 || Memo.Memo6)
            {
                Memo.instance.OnButton();
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
