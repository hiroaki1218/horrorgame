using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Gate : MonoBehaviour
{
    public GameObject OpenDoorUI;
    public GameObject CloseDoorUI;
    [SerializeField] private AudioSource Sound;
    [SerializeField] float Volume;
    [SerializeField] private AudioClip OpenDoorSound;
    [SerializeField] private AudioClip CloseDoorSound;
    [SerializeField] private AudioClip RockSound;
    [SerializeField] private GameObject AnimeObject1;
    [SerializeField] private GameObject AnimeObject2;
    [SerializeField] private float AnimeTime;
    [SerializeField] private GameObject OpenWaitBox;

    public bool SecondAnime;
    public bool rockSound;
    private bool isOpen;
    private bool Action;
    private bool One;
    private bool once;
    //private bool OpenDoorText = true;
    //private bool CloseDoorText = false;

    void Start()
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
        once = true;
        isOpen = false;
        Action = false;
        One = true;
    }
    
    //private void OnTriggerExit(Collider collision)
    //{
        //OpenDoorUI.SetActive(false);
        //CloseDoorUI.SetActive(false);
        //Action = false;
    //}

    void Update()
    {
        if(!Inventory.inventory && !Menu.pause && !PhoneAnimation.isLookPhone && !Memo.LookMemo)
        {
            //DoorOpen
            if (FPSTrigger.open == true && isOpen == false)
            {
                OpenDoorUI.SetActive(true);

            }
            if (isOpen == true || FPSTrigger.open == false)
            {
                OpenDoorUI.SetActive(false);
            }

            //DoorClose
            if (FPSTrigger.close == true && isOpen == true)
            {
                if (One)
                {
                    StartCoroutine("DoorCloseWait");
                    One = false;
                }

            }

            
            //ドアを自動で開ける処理
            if (isOpen == false && FPSTrigger.open == true && CharacterAni.isFirstAnim && once)
            {
                StartCoroutine("DoorOpenWait");
                once = false;
            }

            
        }
    }


    IEnumerator DoorOpenWait()
    {
        isOpen = true;
        //OpenDoorUI.SetActive(false);
        //OpenDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play("DoorOpen");
        if (SecondAnime)
        {
            AnimeObject2.GetComponent<Animator>().Play("BDoorOpen");
        }

        //Audio
        Sound.volume = Volume;
        Sound.clip = OpenDoorSound;
        Sound.PlayOneShot(Sound.clip);

        yield return new WaitForSeconds(AnimeTime);
        Sound.clip = null;
        Destroy(OpenWaitBox);
        //CloseDoorUI.SetActive(true);
        //CloseDoorText = true;
    }

    IEnumerator DoorCloseWait()
    {
        //CloseDoorUI.SetActive(false);
        //CloseDoorText = false;
        AnimeObject1.GetComponent<Animator>().Play("DoorClose");
        if (SecondAnime)
        {
            AnimeObject2.GetComponent<Animator>().Play("BDoorClose");
        }

        //Audio
        Sound.volume = Volume;
        Sound.clip = CloseDoorSound;
        Sound.PlayOneShot(Sound.clip);
        if (rockSound)
        {
            yield return new WaitForSeconds(AnimeTime + 1f);
            Sound.clip = RockSound;
            Sound.volume = 1;
            Sound.PlayOneShot(Sound.clip);
            yield return new WaitForSeconds(2);
            Sound.clip = null;
            //OpenDoorText = true;
            isOpen = false;
        }
        else
        {
            yield return new WaitForSeconds(AnimeTime);
            Sound.clip = null;
            //OpenDoorUI.SetActive(true);
            //OpenDoorText = true;
            isOpen = false;
        }
    }

}

