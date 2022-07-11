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
    public static bool timeIsUP;

    void Start()
    {
        realtime = 0;
        timeIsUP = false;
    }

    void Update()
    {
        if (!Menu.pause && minutes != 10)
        {
            realtime += Time.deltaTime ;
            clocktime = (int)realtime;
            minutes = clocktime / 60;
            second.transform.eulerAngles = new Vector3(0, 0, -clocktime * 6);
            minute.transform.eulerAngles = new Vector3(0, 0, -minutes * 6);
        }   
        if(minutes == 10)
        {
            timeIsUP = true;
        }
    }
}
