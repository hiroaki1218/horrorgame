using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   // �����Ƀ��b�N
        Cursor.visible = false;     // �J�[�\����\��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
