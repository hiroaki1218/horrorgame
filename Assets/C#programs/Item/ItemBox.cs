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
    //PickupObj���N���b�N���ꂽ��A�X���b�g�ɃA�C�e��������
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

    //�X���b�g���N���b�N�����Ƃ�
    public void OnSlotClick(int position)
    {
        //�I�������Ƃ���ɃA�C�e�����Ȃ������牽�����Ȃ�
        if (slots[position].IsEmpty())
        {
            return;
        }
        //��x���ׂĔ��ɂ���
        for(int i=0; i<slots.Length; i++)
        {
            //slots[i]�̔w�i���Ȃ���
            slots[i].HideBackPanel();
        }
        //�N���b�N�����X���b�g�̔w�i�����ɂ���
        slots[position].OnSelect();
        //�I���A�C�e��
        selectItem = slots[position].GetItem();
    }
    //����I�����Ă邩�ǂ�������
    public bool CheckSelectItem()
    {
        if(selectItem.type == Items.Type.Key)
        {
            return true;
        }
        return false;
    }
}
