using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhoneCamera : MonoBehaviour
{
    [SerializeField] private Camera Camera1;
    [SerializeField] private Camera Camera2;
    [SerializeField] private Camera Camera3;
    int change;

    void Start()
    {
        change = 0;
        //Å‰‚ÍƒJƒƒ‰‚P‚¾‚¯‚ª—LŒø
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;
    }

    void Update()
    {
        if(change == 0)
        {
            Camera1.enabled = true;
            Camera2.enabled = false;
            Camera3.enabled = false;
        }

        if(change == 1)
        {
            Camera1.enabled = false;
            Camera2.enabled = true;
            Camera3.enabled = false;
        }

        if(change == 2)
        {
            Camera1.enabled = false;
            Camera2.enabled = false;
            Camera3.enabled = true;
        }

        if(change == 3)
        {
            change = 0;
        }
    }

    public void Onclick()
    {
        Debug.Log("aaaaaa");
        change = change + 1;
    }

}
