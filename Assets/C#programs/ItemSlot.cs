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

    //�󂩂ǂ���
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
    //�A�C�e�����󂯎������摜���X���b�g�ɕ\��
    void UpdateImage(Items item)
    {
        image.sprite = item.sprite;
    }
}
