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
        image.sprite = item.sprite;
        //UpdateImage(item);
    }

    public Items GetItem()
    {
        return item;
    }
    //�A�C�e�����󂯎������摜���X���b�g�ɕ\��
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
