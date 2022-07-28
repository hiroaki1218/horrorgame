using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Items item = null;
    GameObject empty;
    [SerializeField] private GameObject backPanel = default;
    Image image = default;
    //Image image = default;

    private void Start()
    {
        backPanel.SetActive(false);
    }
    private void Awake()
    {
        empty = transform.GetChild(1).gameObject;
        image = empty.GetComponent<Image>();
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
        image.sprite = item.sprite;
        //UpdateImage(item);
    }
    public void RemoveItem()
    {
        item = null;
        image.sprite = null;
        HideBackPanel();
    }

    public Items GetItem()
    {
        return item;
    }
    //アイテムを受け取ったら画像をスロットに表示
    //void UpdateImage(Items item)
    //{

    //}
    public void OnSelect()
    {
        backPanel.SetActive(true);
    }
    public void HideBackPanel()
    {
        backPanel.SetActive(false);
    }
}