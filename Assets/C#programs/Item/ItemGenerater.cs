using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerater : MonoBehaviour
{
    [SerializeField] ItemDateBase itemDateBase;

    public static ItemGenerater instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Items Spawn(Items.Type type)
    {
        //typeと一致するitemを生成して渡す
        foreach(Items item in itemDateBase.itemList)
        {
            if(item.type == type)
            {
                return new Items(item.type, item.sprite);
            }
        }
        return null;
    }
}
