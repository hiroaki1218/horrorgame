using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Items
{
    //public GameObject item;
    //public string expalianText;

    //—ñ‹“Œ^Fí—Ş‚ğ’è‹`
    public enum Type
    {
        Key,
        Flashlight,
        Phone,
        Battery,
        Memo1,
        Memo2,
        Memo3,
        Memo4,
        Memo5,
        Memo6,
    }

    //ItemType‚ğéŒ¾
    public Type type;
    //Item‰æ‘œ‚ğéŒ¾
    public Sprite sprite;

    public Items(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }
}
