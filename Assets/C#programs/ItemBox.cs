using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] ItemSlot[] slots = default;
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
    //PickupObjがクリックされたら、スロットにアイテムを入れる
    public void SetItem(Items item)
    {
        for(int i=0; i< slots.Length; i++)
        {
            ItemSlot slot = slots[i];
            if (slot.IsEmpty())
            {
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
    //鍵を選択してるかどうか判定
    public bool CheckSelectItem()
    {
        if(selectItem.type == Items.Type.Key)
        {
            return true;
        }
        return false;
    }
}
