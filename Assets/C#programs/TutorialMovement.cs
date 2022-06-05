using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMovement : MonoBehaviour
{
    [SerializeField] private GameObject conditionCanvas;
    [SerializeField] private GameObject doorconditonCanvas;

    [SerializeField] private GameObject WalkcheckMark;
    [SerializeField] private GameObject RuncheckMark;
    [SerializeField] private GameObject PhonecheckMark;
    [SerializeField] private GameObject DoorcheckMark;

    GameObject door;
    Door _door;

    private bool FirstCondition;
    private bool SecondCondition;
    private bool ThirdCondition;
    private bool FourthCondition;
    private bool one;
    private bool two;
    private bool three;
    private bool four;

    private void Awake()
    {
        conditionCanvas.SetActive(true);
        doorconditonCanvas.SetActive(false);
        WalkcheckMark.SetActive(false);
        RuncheckMark.SetActive(false);
        PhonecheckMark.SetActive(false);
        DoorcheckMark.SetActive(false);
    }
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

        door = GameObject.Find("SubSdoorTrigger");
        _door = door.GetComponent<Door>();
        _door.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!Inventory.inventory && !PhoneAnimation.isLookPhone)
        {
            //条件2：走った時
            if ((Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)) && two)
            {
                StartCoroutine("Running");
                two = false;
            }
            //条件1：wで歩いた時
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && one)
            {
                StartCoroutine("Walk");
                one = false;
            }
            //条件3：スマホを取ったとき
            if (PickupObj.collectPhone && three)
            {
                StartCoroutine("GetPhone");
                three = false;
            }

            //条件4のドアはAllConditionがtrueの時
            if (FirstCondition && SecondCondition && ThirdCondition)
            {
                StartCoroutine("GetOpenCondition");
            }
        }
        
    }

    //条件1：wで歩いた時
    IEnumerator Walk()
    {
        //Debug.Log("条件1");
        yield return new WaitForSeconds(0.5f);//0.5sの時差
        WalkcheckMark.SetActive(true); //チェックマークが見えるようにする。
        FirstCondition = true;　       //歩き終わったら走ることが可能になる(tutorialのみ）
    }

    //条件2：走った時
    IEnumerator Running()
    {
        //Debug.Log("条件2");
        yield return new WaitForSeconds(0.5f);//0.5sの時差
        RuncheckMark.SetActive(true);  //チェックマークが見えるようにする。
        SecondCondition = true;        //走り終わったら拾うことが可能になる
    }

    //条件3：スマホを取ったとき
    IEnumerator GetPhone()
    {
        //Debug.Log("条件3");
        yield return new WaitForSeconds(0.5f);//0.5sの時差
        PhonecheckMark.SetActive(true);//チェックマークが見えるようにする。
        ThirdCondition = true;         //スマホを拾うと条件３がクリアになる。
    }

    //条件4：ドアを開けたとき
    IEnumerator GetOpenCondition()
    {
        _door.gameObject.SetActive(true);
        //もしドアが開いたら、DoorcheckMark.SetActive(true)
        if (_door.isOpen)
        {
            DoorcheckMark.SetActive(true);
            yield return new WaitForSeconds(1.0f);//1sの時差
            doorconditonCanvas.SetActive(false);　//条件４：ドアのUI非表示
            SceneManager.LoadScene("MainScene");
        }
        else if(four)
        {
            //Debug.Log("条件4");
            yield return new WaitForSeconds(1.0f);　　//1sの時差
            conditionCanvas.SetActive(false); 　　　　//条件クリアしたら、条件1～3のUIを消す。
            doorconditonCanvas.SetActive(true);　　　 //条件４：ドアのUI表示
            four = false;
        }  
    }
}
