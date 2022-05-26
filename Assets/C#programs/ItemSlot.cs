using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Items item;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //空かどうか
    public bool IsEmpty()
    {
        if (item == null)
        {
            return true;
        }
        return false;
    }

    public void SetItem(Items item)
    {
        this.item = item;
        UpdateImage(item);
    }
    //アイテムを受け取ったら画像をスロットに表示
    void UpdateImage(Items item)
    {
        image.sprite = item.sprite;
    }
}
