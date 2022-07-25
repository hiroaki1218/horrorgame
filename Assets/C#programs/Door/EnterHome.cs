using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHome : MonoBehaviour
{
    public static bool enterThehome;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enterThehome = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enterThehome = false;
        }
    }
}
