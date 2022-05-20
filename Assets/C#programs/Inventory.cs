using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InvUI;
    private bool Active;

    public void Start()
    {
        InvUI.SetActive(false);
        Active = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Active = !Active;
            Debug.Log("Tab");

            if (Active == true)
            {
                Debug.Log("Active");
                InvUI.SetActive(true);
            }
            else if (!Active)
            {
                Debug.Log("NotActive");
                InvUI.SetActive(false);
            }
        }
    }
}
