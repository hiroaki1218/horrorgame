using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject SmartPhone;
    private void Awake()
    {
        SmartPhone.SetActive(false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (PickupObj.collectPhone)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SmartPhone.SetActive(true);
            }
        }
    }
}
