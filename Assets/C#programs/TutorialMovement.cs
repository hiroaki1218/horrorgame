using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TutorialMovement : MonoBehaviour
{
    public static bool FirstCondition;
    public static bool SecondCondition;
    public static bool ThirdCondition;
    public static bool FourthCondition;
    private bool one;
    private bool two;
    private bool three;
    private bool four;

    private void Start()
    {
        one = true;
        two = true;
        three = true;
        four = true;
        FirstCondition = false;
        SecondCondition = false;
        ThirdCondition = false;
        FourthCondition = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) && two)
        {
            Running();
            two = false;
        }

        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && one)
        {
            Walk();
            one = false;
        }

        if (PickupObj.collectPhone && three)
        {
            GetPhone();
            three = false;
        }

        //����4�̃h�A��AllCondition��true�̎�
        if (FirstCondition && SecondCondition && ThirdCondition && four)
        {
            GetOpenCondition();
            four = false;
        }
    }

    //����1�Fw�ŕ�������
    void Walk()
    {
        Debug.Log("����1");
        //�����I������瑖�邱�Ƃ��\�ɂȂ�(tutorial�̂݁j
        FirstCondition = true;
    }

    //����2�F��������
    void Running()
    {
        Debug.Log("����2");
        //����I�������E�����Ƃ��\�ɂȂ�
        SecondCondition = true;
    }

    //����3�F�X�}�z��������Ƃ�
    void GetPhone()
    {
        Debug.Log("����3");
        ThirdCondition = true;
    }

    //����4�F�h�A���J�����Ƃ�
    void GetOpenCondition()
    {
        Debug.Log("����4");
    }
}
