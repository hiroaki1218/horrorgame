using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
        Cursor.visible = false;     // カーソル非表示
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
