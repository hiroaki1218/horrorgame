using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Items
{
    //public GameObject item;
    //public string expalianText;

    //列挙型：種類を定義
    public enum Type
    {
        Key,
        RoomKey,
        Flashlight,
        Phone,
        Battery,
        Hammer,
        Memo1,
        Memo2,
        Memo3,
        Memo4,
        Memo5,
        Memo6,
        Lens,
    }

    //ItemTypeを宣言
    public Type type;
    //Item画像を宣言
    public Sprite sprite;

    public Items(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }
}
