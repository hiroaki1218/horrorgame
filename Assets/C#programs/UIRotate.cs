
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UIRotate : MonoBehaviour
{
    void LateUpdate()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone)
        {
            //@ƒJƒƒ‰‚Æ“¯‚¶Œü‚«‚Éİ’è
            transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            return;
        }
    }
}