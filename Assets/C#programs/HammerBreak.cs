using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreak : MonoBehaviour
{
    [SerializeField] private GameObject Parents;
    [SerializeField] private int MaxBreakObj;
    public static int count;
    public static int breaknumber;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        breaknumber = count - 1;
    }
    void Update()
    { 
        if (breaknumber >= 0)
        {
            if (count == MaxBreakObj)
            {
                Destroy(Parents);
            }
        }
    }
}
