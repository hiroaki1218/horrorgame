
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UIRotate : MonoBehaviour
{
    void LateUpdate()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone)
        {
            //�@�J�����Ɠ��������ɐݒ�
            transform.rotation = Camera.main.transform.rotation;
        }
        else
        {
            return;
        }
    }
}