using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Items
{
    //public GameObject item;
    //public string expalianText;

    //�񋓌^�F��ނ��`
    public enum Type
    {
        Key,
        Flashlight,
    }

    //ItemType��錾
    public Type type;
    //Item�摜��錾
    public Sprite sprite;

    public Items(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }
}
