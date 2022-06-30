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

    public bool SecondDoorAnime;
    public bool rockSound;
    public bool isOpen;
    private bool Action;
    private bool OpenDoorText;
    private bool CloseDoorText;

    void Start()
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
        isOpen = false;
        Action = false;
        OpenDoorText = true;
        CloseDoorText = false;
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

                    if (isOpen == true && Action == true)
                    {
                        StartCoroutine(DoorCloseWait());
                        DoorCloseWait();
                    }
                }
            }
        }
        
    }


    IEnumerator DoorOpenWait()
    {

        //OpenDoorUI.SetActive(false);
        OpenDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play("SDoorOpen");
        if (SecondDoorAnime)
        {
            AnimeObject2.GetComponent<Animator>().Play("SDoorOpen");
        }
        
        
        //Audio
        audiosource.volume = Volume;
        audiosource.clip = OpenDoorSound;
        audiosource.PlayOneShot(audiosource.clip);

        yield return new WaitForSeconds(AnimeTime);
        //CloseDoorUI.SetActive(true);
        CloseDoorText = true;
        isOpen = true;
        

    }

    IEnumerator DoorCloseWait()
    {
        //CloseDoorUI.SetActive(false);
        CloseDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play("SDoorClose");
        if (SecondDoorAnime)
        {
             AnimeObject2.GetComponent<Animator>().Play("SDoorClose");
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
            OpenDoorText = true;
            isOpen = false;
        }
        else
        {
            yield return new WaitForSeconds(AnimeTime);
            //OpenDoorUI.SetActive(true);
            OpenDoorText = true;
            isOpen = false;
        }
    }
   
}
