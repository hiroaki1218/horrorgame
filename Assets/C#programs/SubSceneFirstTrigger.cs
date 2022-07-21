using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSceneFirstTrigger : MonoBehaviour
{
    public static bool SubFirstTrigger;
    private bool once;
    private void Start()
    {
        once = true;
        SubFirstTrigger = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && once)
        {
            SubFirstTrigger = true;
            once = false;
        }
    }
}
