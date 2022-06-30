
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UIRotate : MonoBehaviour
{
    void LateUpdate()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone)
        {
            //　カメラと同じ向きに設定
            transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            return;
        }
    }
}