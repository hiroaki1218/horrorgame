using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrashLightTrigger : MonoBehaviour
{

    public GameObject CollectUI;
    public GameObject FrashLight;
    public GameObject FPSlight;

    private bool Action = false;
    public bool isCollect;

    // Start is called before the first frame update
    void Start()
    {
        CollectUI.SetActive(false);
        isCollect = false;
        FPSlight.SetActive(false);
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            CollectUI.SetActive(true);
            Action = true;
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        CollectUI.SetActive(false);
        Action = false;
    }


        // Update is called once per frame
    void Update()
    {
        if( Action == true )
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(FrashLight.gameObject);
                isCollect = true;
            }
        
        }

        if( isCollect == true)
        {
            CollectUI.SetActive(false);
            FPSlight.SetActive(true);
        }

    }
}
