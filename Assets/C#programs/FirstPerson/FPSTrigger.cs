using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTrigger : MonoBehaviour
{
    [SerializeField]
    public static bool open;
    [SerializeField]
    public static bool close;
    [SerializeField]
    private GameObject CloseTigger;

    private bool One;

    private void Start()
    {
        open = false;
        close = false;
        One = true;
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "DoorOpenTrigger")
        {
            open = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DoorCloseTrigger")
        {
            if (One)
            {
               close = true;
                Destroy(CloseTigger);
                One = false;
            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        open = false;
        close = false;
    }
}
