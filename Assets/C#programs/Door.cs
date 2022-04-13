using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    public GameObject OpenDoorUI;
    public GameObject CloseDoorUI;
    public GameObject AnimeObject;
    
    public bool isOpen = false;
    public bool Action = false;
    public bool OpenDoorText = true;
    public bool CloseDoorText = false;

    void Start()
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
    }

    //トリガーにプレイヤーが入ったとき
    void OnTriggerStay(Collider collision)
    {
        

        if (collision.transform.tag == "Player")
        {
            if( OpenDoorText == true )
            { 
                OpenDoorUI.SetActive(true);
            }
            if( OpenDoorText == false)
            {
                OpenDoorUI.SetActive(false);
            }

            if( CloseDoorText == true )
            {
                CloseDoorUI.SetActive(true);
            }
            if( CloseDoorText == false)
            {
                CloseDoorUI.SetActive(false);
            }
            //OpenDoorUI.SetActive(true);
            Action = true;

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
        if( OpenDoorText == true || CloseDoorText == true) { 
           if (Input.GetKeyDown(KeyCode.E))
           {
            if ( isOpen == false && Action == true)
            {
                StartCoroutine(DoorOpenWait());
                DoorOpenWait();
            }

            if( isOpen == true && Action == true)
            {
                StartCoroutine(DoorCloseWait());
                DoorCloseWait();
            }
           }
        }
    }


    IEnumerator DoorOpenWait()
    {

        //OpenDoorUI.SetActive(false);
        OpenDoorText = false;
        AnimeObject.GetComponent<Animator>().Play("DoorOpen");
        
        yield return new WaitForSeconds(1);
        //CloseDoorUI.SetActive(true);
        CloseDoorText = true;
        isOpen = true;
    }

    IEnumerator DoorCloseWait()
    {
        //CloseDoorUI.SetActive(false);
        CloseDoorText = false;
        AnimeObject.GetComponent<Animator>().Play("DoorClose");
       
        yield return new WaitForSeconds(1);
        //OpenDoorUI.SetActive(true);
        OpenDoorText = true;
        isOpen = false;
    }
}
