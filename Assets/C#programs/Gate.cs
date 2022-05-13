using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Gate : MonoBehaviour
{
    FPSTrigger fpstrigger;
    GameObject FPS;
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

    public bool SecondAnime;
    public bool rockSound;
    private bool isOpen = false;
    private bool Action = false;
    private bool One = true;
    //private bool OpenDoorText = true;
    //private bool CloseDoorText = false;

    void Start()
    {
        FPS = GameObject.Find("Trigger");
        fpstrigger = FPS.GetComponent<FPSTrigger>();
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
    }
    
    //private void OnTriggerExit(Collider collision)
    //{
        //OpenDoorUI.SetActive(false);
        //CloseDoorUI.SetActive(false);
        //Action = false;
    //}

    void Update()
    {
        //DoorOpen
         if (fpstrigger.open == true && isOpen == false)
         {
            OpenDoorUI.SetActive(true);
              
         }
         if(isOpen == true)
         {
            OpenDoorUI.SetActive(false);  
         }

         //DoorClose
         if(fpstrigger.close == true && isOpen == true)
         {
            if (One)
            {
               StartCoroutine("DoorCloseWait");
                One = false;
            }
            
         }

         if (Input.GetKeyDown(KeyCode.F))
         {
             if (isOpen == false && fpstrigger.open == true)
             {
                    DoorOpenWait();
             }

         }
        
    }


    void DoorOpenWait()
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

        //yield return new WaitForSeconds(AnimeTime);
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
            //OpenDoorText = true;
            isOpen = false;
        }
        else
        {
            yield return new WaitForSeconds(AnimeTime);
            //OpenDoorUI.SetActive(true);
            //OpenDoorText = true;
            isOpen = false;
        }
    }

}

