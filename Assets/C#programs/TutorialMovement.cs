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

        //条件4のドアはAllConditionがtrueの時
        if (FirstCondition && SecondCondition && ThirdCondition && four)
        {
            GetOpenCondition();
            four = false;
        }
    }

    //条件1：wで歩いた時
    void Walk()
    {
        Debug.Log("条件1");
        //歩き終わったら走ることが可能になる(tutorialのみ）
        FirstCondition = true;
    }

    //条件2：走った時
    void Running()
    {
        Debug.Log("条件2");
        //走り終わったら拾うことが可能になる
        SecondCondition = true;
    }

    //条件3：スマホを取ったとき
    void GetPhone()
    {
        Debug.Log("条件3");
        ThirdCondition = true;
    }

    //条件4：ドアを開けたとき
    void GetOpenCondition()
    {
        Debug.Log("条件4");
    }
}
