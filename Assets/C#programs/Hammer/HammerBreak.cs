using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreak : MonoBehaviour
{
    [SerializeField] private GameObject Parents;
    [SerializeField] private int MaxBreakObj;
    public int count;
    public int breaknumber;
    public static HammerBreak instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
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
                Destroy(this.Parents);
            }
        }
    }
    IEnumerator WaitBreak()
    {
        yield return new WaitForSeconds(0.9f);
        HammerBreakChild.canBreak = true;
    }
}
