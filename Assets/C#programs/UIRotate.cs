
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UIRotate : MonoBehaviour
{
    void LateUpdate()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone && !LensAnimation.isLookLens)
        {
            //　カメラと同じ向きに設定
            transform.rotation = Camera.main.transform.rotation;
        }
        else if(Camera.main == null)
        {
            return;
        }
        else
        {
            return;
        }
    }
}