using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    [SerializeField] ItemSlot[] slots = default;
    [SerializeField] Button[] SlotButton;
    public static ItemBox instance;

    Items selectItem;
    Items getItem;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        SlotButton[0].interactable = false;
        SlotButton[1].interactable = false;
        SlotButton[2].interactable = false;
        SlotButton[3].interactable = false;
        SlotButton[4].interactable = false;
        SlotButton[5].interactable = false;
    }
    //PickupObjがクリックされたら、スロットにアイテムを入れる
    public void SetItem(Items item)
    {
        for(int i=0; i< slots.Length; i++)
        {
            ItemSlot slot = slots[i];
            if (slot.IsEmpty())
            {
                SlotButton[i].interactable = true;
                slot.SetItem(item);
                break;
            }
        }
    }

    //スロットをクリックしたとき
    public void OnSlotClick(int position)
    {
        //選択したところにアイテムがなかったら何もしない
        if (slots[position].IsEmpty())
        { 
            return;
        }
        //一度すべて白にする
        for(int i=0; i<slots.Length; i++)
        {
            //slots[i]の背景をなくす
            slots[i].HideBackPanel();
        }
        //クリックしたスロットの背景を黒にする
        slots[position].OnSelect();
        //選択アイテム
        selectItem = slots[position].GetItem();
    }
    //Itemを選択してるかどうか判定
    public bool CheckSelectItem(Items.Type useItemType)
    {
        if(selectItem == null)
        {
            return false;
        }
        if(selectItem.type == useItemType)
        {
            return true;
        }
        return false;
    }
}
