using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTrigger : MonoBehaviour
{
    public static bool enter;

    private void Start()
    {
        enter = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enter = false;
        }
    }
}
