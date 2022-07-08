using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreak : MonoBehaviour
{
    [SerializeField] private GameObject Parents;
    [SerializeField] private GameObject[] BreakObject;
    [SerializeField] private int MaxBreakObj;
    int count;
    int breaknumber;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        breaknumber = count - 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            count++;
            breaknumber = count - 1;
        }
        if (breaknumber >= 0)
        {
            if (count == MaxBreakObj)
            {
                Destroy(Parents);
            }
            else
            {
                BreakObject[breaknumber].SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
