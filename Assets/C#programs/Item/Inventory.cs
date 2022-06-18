using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InvUI;
    [SerializeField] private GameObject player;
    [SerializeField] private Text text;
    private bool Active;
    public static bool inventory;
    FirstPersonControllerCustom fpc;

    public void Start()
    {
        InvUI.SetActive(false);
        Active = false;
        inventory = false;
    }

    public void Update()
    {
        fpc = player.GetComponent<FirstPersonControllerCustom>();
        if (!Menu.pause && !PhoneAnimation.isLookPhone && !Memo.LookMemo && !Memo.exitMemo1)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Active = !Active;
                if (Active)
                {
                    InvUI.SetActive(true);
                    fpc.enabled = false;
                    inventory = true;
                    Cursor.visible = true;     // �J�[�\���\��
                    Cursor.lockState = CursorLockMode.None;     // �W�����[�h
                }
                else if (!Active)
                {
                    InvUI.SetActive(false);
                    fpc.enabled = true;
                    inventory = false;
                    Cursor.visible = false;     // �J�[�\����\��
                    Cursor.lockState = CursorLockMode.Locked;   // �����Ƀ��b�N
                }
            }
            
        }
        if (ItemBox.instance.CheckSelectItem(Items.Type.Memo1))
        {
            text.text = "����\n���͂��̊قɕ����߂��Ă��܂����B\n�������̏ꏊ����E�o���Ȃ����...�����ɏP���Ă��܂�...�B" +
                "\n��ɗ����l�̂��߂ɖ��ɗ��ł��낤�������c���Ă������B";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo2))
        {
            text.text = "����\n�x�b�h���������񂠂镔���ɓ�K�̌������邾�낤�B";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo3))
        {
            text.text = "����\n�������Ă���Ƃ���Ƀq���g�����邾�낤�B";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo4))
        {
            text.text = "����\n��K�̃x�����_�ɐH���̃J�M�����邾�낤�B";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo5))
        {
            text.text = "����\nOO�Ɍ��ւ̌�";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Memo6))
        {
            text.text = "����\nOO�ɖ�̌�";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Phone))
        {
            text.text = "�X�}�[�g�t�H��\n�߂��̊Ď��J�����̉f����������B";
        }
        else if (ItemBox.instance.CheckSelectItem(Items.Type.Flashlight))
        {
            text.text = "�����d��\n�o�b�e���[�̏���͌��������A���邢�B";
        }
        else
        {
            text.text = null;
        }
    }
}
