using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTrigger : MonoBehaviour
{
    [SerializeField]
    public bool open = false;
    [SerializeField]
    public bool close = false;

    private bool One = true;
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
