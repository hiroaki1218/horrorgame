using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject SmartPhone;
    private bool Active;
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
                Active = !Active;

                if (Active)
                {
                    SmartPhone.SetActive(true);
                    SmartPhone.GetComponent<Animator>().Play("PhoneOn");
                }
                else if (!Active)
                {
                    SmartPhone.GetComponent<Animator>().Play("PhoneOff");
                    StartCoroutine("OffPhone");
                }
            }

        }
    }
    IEnumerator OffPhone()
    {
        //êîïbå„Ç…è¡Ç¶ÇÈÅiíxâÑÅj
        yield return new WaitForSeconds(0.37f);
        SmartPhone.SetActive(false);
    }
}
