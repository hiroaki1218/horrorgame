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
           
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        OpenDoorUI.SetActive(false);
        CloseDoorUI.SetActive(false);
    }

    void Update()
    {
        if( OpenDoorText == true || CloseDoorText == true ) { 
           if (Input.GetKeyDown(KeyCode.E))
           {
            if ( isOpen == false )
            {
                StartCoroutine(DoorOpenWait());
                DoorOpenWait();
            }

            if( isOpen == true )
            {
                StartCoroutine(DoorCloseWait());
                DoorCloseWait();
            }
           }
        }
    }


    IEnumerator DoorOpenWait()
    {
       
        OpenDoorText = false;
        AnimeObject.GetComponent<Animator>().Play("DoorOpen");
        
        yield return new WaitForSeconds(1);
        CloseDoorText = true;
        isOpen = true;
    }

    IEnumerator DoorCloseWait()
    { 
        CloseDoorText = false;
        AnimeObject.GetComponent<Animator>().Play("DoorClose");
       
        yield return new WaitForSeconds(1);
        OpenDoorText = true;
        isOpen = false;
    }
}
