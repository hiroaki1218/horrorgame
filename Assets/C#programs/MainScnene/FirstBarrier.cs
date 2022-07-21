using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBarrier : MonoBehaviour
{
    [SerializeField] private GameObject BarrierUI;
    private void Start()
    {
        BarrierUI.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            BarrierUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            BarrierUI.SetActive(false);
        }
    }
}
