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
            //����2�F��������
            if ((Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)) && two)
            {
                StartCoroutine("Running");
                two = false;
            }
            //����1�Fw�ŕ�������
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && one)
            {
                StartCoroutine("Walk");
                one = false;
            }
            //����3�F�X�}�z��������Ƃ�
            if (PickupObj.collectPhone && three)
            {
                StartCoroutine("GetPhone");
                three = false;
            }

            //����4�̃h�A��AllCondition��true�̎�
            if (FirstCondition && SecondCondition && ThirdCondition)
            {
                StartCoroutine("GetOpenCondition");
            }
        }
        
    }

    //����1�Fw�ŕ�������
    IEnumerator Walk()
    {
        //Debug.Log("����1");
        yield return new WaitForSeconds(0.5f);//0.5s�̎���
        WalkcheckMark.SetActive(true); //�`�F�b�N�}�[�N��������悤�ɂ���B
        FirstCondition = true;�@       //�����I������瑖�邱�Ƃ��\�ɂȂ�(tutorial�̂݁j
    }

    //����2�F��������
    IEnumerator Running()
    {
        //Debug.Log("����2");
        yield return new WaitForSeconds(0.5f);//0.5s�̎���
        RuncheckMark.SetActive(true);  //�`�F�b�N�}�[�N��������悤�ɂ���B
        SecondCondition = true;        //����I�������E�����Ƃ��\�ɂȂ�
    }

    //����3�F�X�}�z��������Ƃ�
    IEnumerator GetPhone()
    {
        //Debug.Log("����3");
        yield return new WaitForSeconds(0.5f);//0.5s�̎���
        PhonecheckMark.SetActive(true);//�`�F�b�N�}�[�N��������悤�ɂ���B
        ThirdCondition = true;         //�X�}�z���E���Ə����R���N���A�ɂȂ�B
    }

    //����4�F�h�A���J�����Ƃ�
    IEnumerator GetOpenCondition()
    {
        _door.gameObject.SetActive(true);
        //�����h�A���J������ADoorcheckMark.SetActive(true)
        if (_door.isOpen)
        {
            DoorcheckMark.SetActive(true);
            yield return new WaitForSeconds(1.0f);//1s�̎���
            doorconditonCanvas.SetActive(false);�@//�����S�F�h�A��UI��\��
            SceneManager.LoadScene("MainScene");
        }
        else if(four)
        {
            //Debug.Log("����4");
            yield return new WaitForSeconds(1.0f);�@�@//1s�̎���
            conditionCanvas.SetActive(false); �@�@�@�@//�����N���A������A����1�`3��UI�������B
            doorconditonCanvas.SetActive(true);�@�@�@ //�����S�F�h�A��UI�\��
            four = false;
        }  
    }
}
