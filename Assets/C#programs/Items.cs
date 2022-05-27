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
