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
        for(int i = 0; i<=SlotButton.Length; i++)
        {
            SlotButton[i].interactable = false;
        } 
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
        Inventory.k = position;
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

            //Inventory.instance.ItemCheckButton[i].interactable = false;
        }
        //クリックしたスロットの背景を黒にする
        slots[position].OnSelect();

        //Inventory.instance.ItemCheckButton[position].interactable = true;
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
