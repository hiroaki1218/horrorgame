using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    [HideInInspector] public Menu _menu;
    public GameObject OpenDoorUI;
    public GameObject CloseDoorUI;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] float Volume;
    [SerializeField] private AudioClip OpenDoorSound;
    [SerializeField] private AudioClip CloseDoorSound;
    [SerializeField] private AudioClip RockSound;
    [SerializeField] private GameObject AnimeObject1;
    [SerializeField] private GameObject AnimeObject2;
    [SerializeField] private float AnimeTime;
    [SerializeField] private string opendoor = "SDoorOpen";
    [SerializeField] private string closedoor = "SDoorClose";
    [SerializeField] private string secondopendoor = "SDoorOpen";
    [SerializeField] private string secondclosedoor = "SDoorClose";

    public bool SecondDoorAnime;
    public bool rockSound;
    public bool OneWayDoor;
    public bool FirstMove;
    public bool KeyDoor;

    public bool isOpen;
    public bool Action;
    public bool usedKey;
    private bool OpenDoorText;
    private bool CloseDoorText;
    private bool once;
    public static Door instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
        isOpen = false;
        Action = false;
        OpenDoorText = true;
        CloseDoorText = false;
        usedKey = false;
        once = true;
    }

    //トリガーにプレイヤーが入ったとき
    void OnTriggerStay(Collider collision)
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone && !Memo.LookMemo)
        {
            if (collision.transform.tag == "Player")
            {
                if (OpenDoorText == true)
                {
                    OpenDoorUI.SetActive(true);
                }
                if (OpenDoorText == false)
                {
                    OpenDoorUI.SetActive(false);
                }

                if (CloseDoorText == true)
                {
                    CloseDoorUI.SetActive(true);
                }
                if (CloseDoorText == false)
                {
                    CloseDoorUI.SetActive(false);
                }
                //OpenDoorUI.SetActive(true);
                Action = true;

            }
        }
        else
        {
            OpenDoorUI.SetActive(false);
            CloseDoorUI.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
        Action = false;
    }

    void Update()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone)
        {
            if (OpenDoorText == true || CloseDoorText == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (isOpen == false && Action == true)
                    {
                        StartCoroutine(DoorOpenWait());
                        DoorOpenWait();
                    }

                    if (isOpen == true && Action == true && !OneWayDoor)
                    {
                        StartCoroutine(DoorCloseWait());
                        DoorCloseWait();
                    }
                }
            }
        }
        if (usedKey)
        {
            UseKeyAndOpen();
        }
        //家に入ったとき
        if (FirstMove && EnterHome.enterThehome &&once)
        {
            StartCoroutine(DoorCloseWait());
            once = false;
        }
    }


    public IEnumerator DoorOpenWait()
    {

        //OpenDoorUI.SetActive(false);
        OpenDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play(opendoor);
        if (SecondDoorAnime)
        {
            AnimeObject2.GetComponent<Animator>().Play(secondopendoor);
        }
        
        
        //Audio
        audiosource.volume = Volume;
        audiosource.clip = OpenDoorSound;
        audiosource.PlayOneShot(audiosource.clip);

        yield return new WaitForSeconds(AnimeTime);
        //CloseDoorUI.SetActive(true);
        if (!OneWayDoor)
        {
            CloseDoorText = true;
        }
        isOpen = true;
    }

    IEnumerator DoorCloseWait()
    {
        //CloseDoorUI.SetActive(false);
        CloseDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play(closedoor);
        if (SecondDoorAnime)
        {
             AnimeObject2.GetComponent<Animator>().Play(secondclosedoor);
        }

        //Audio
        audiosource.volume = Volume;
        audiosource.clip = CloseDoorSound;
        audiosource.PlayOneShot(audiosource.clip);
        if (rockSound)
        {
            yield return new WaitForSeconds(AnimeTime+1f);
            audiosource.clip = RockSound;
            audiosource.volume = 1;
            audiosource.PlayOneShot(audiosource.clip);
            yield return new WaitForSeconds(2);
            if (!OneWayDoor)
            {
                OpenDoorText = true;
            }
            isOpen = false;
        }
        else
        {
            yield return new WaitForSeconds(AnimeTime);
            //OpenDoorUI.SetActive(true);
            if (!OneWayDoor)
            {
                OpenDoorText = true;
            }
            isOpen = false;
        }
    }
    public void UseKeyAndOpen()
    {
        AnimeObject1.GetComponent<Animator>().Play(opendoor);
        if (SecondDoorAnime)
        {
            AnimeObject2.GetComponent<Animator>().Play(secondopendoor);
        }

        //Audio
        audiosource.volume = Volume;
        audiosource.clip = OpenDoorSound;
        audiosource.PlayOneShot(audiosource.clip);
        Inventory.canPushTab = false;
    }
}
