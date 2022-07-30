using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoChest : MonoBehaviour
{
    public static MemoChest instance;

    [SerializeField] private GameObject Memo6;
    [SerializeField] private GameObject HomeKey;
    public bool canClickButton;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Memo6.SetActive(false);
        HomeKey.SetActive(false);
        canClickButton = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canClickButton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canClickButton = false;
        }
    }
}
