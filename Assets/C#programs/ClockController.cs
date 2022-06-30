using System;   // DateTimeに必要
using System.Collections;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public GameObject hour;
    public GameObject minute;
    public GameObject second;
    float realtime;
    int clocktime;
    int minutes;

    void Start()
    {
        realtime = 0;
    }

    void Update()
    {
        if (!Menu.pause)
        {
            realtime += Time.deltaTime ;
            clocktime = (int)realtime;
            minutes = clocktime / 60;
            second.transform.eulerAngles = new Vector3(0, 0, -clocktime * 6);
            minute.transform.eulerAngles = new Vector3(0, 0, -minutes * 6);
        }   
    }
}
