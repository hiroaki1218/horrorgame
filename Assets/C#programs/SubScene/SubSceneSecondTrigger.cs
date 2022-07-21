using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSceneSecondTrigger : MonoBehaviour
{
    public static bool SubSecondTrigger;
    private bool once;
    private void Start()
    {
        SubSecondTrigger = false;
        once = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && once)
        {
            SubSecondTrigger = true;
            once = false;
        }
    }
}