using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] ItemSlot[] slots;
    public static ItemBox instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    //PickupObjがクリックされたら、スロットにアイテムを入れる
    public void SetItem(Items item)
    {
        foreach(ItemSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                break;
            }
        }
        
        
        //Debug.Log(item.type);
    }

}
