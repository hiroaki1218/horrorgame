using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InvUI;
    [SerializeField] private GameObject player;
    private bool Active;
    public static bool inventory;
    FirstPersonControllerCustom fpc;

    public void Start()
    {
        InvUI.SetActive(false);
        Active = false;
        inventory = false;
    }

    public void Update()
    {
        fpc = player.GetComponent<FirstPersonControllerCustom>();
        if (!Menu.pause)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Active = !Active;
                if (Active)
                {
                    InvUI.SetActive(true);
                    fpc.enabled = false;
                    inventory = true;
                    Cursor.visible = true;     // カーソル表示
                    Cursor.lockState = CursorLockMode.None;     // 標準モード
                }
                else if (!Active)
                {
                    InvUI.SetActive(false);
                    fpc.enabled = true;
                    inventory = false;
                    Cursor.visible = false;     // カーソル非表示
                    Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
                }
            }
            
        }
        
    }
}
