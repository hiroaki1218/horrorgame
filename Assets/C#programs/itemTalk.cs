using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTalk : MonoBehaviour
{
    [SerializeField]
    private GameObject itemUI;
    [SerializeField]
    private Camera mainCamera;

    private void Start()
    {
        itemUI.SetActive(false);
        mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera)
        {
            RaycastHit hit;  //Ray�����������I�u�W�F�N�g�̏�񂪊i�[�����

            //Ray���������Ă����
            if (Physics.Raycast(mainCamera.ViewportPointToRay(new Vector2(0.5f,0.5f)),out hit,100.0f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    Debug.Log("�A�C�e��");
                    itemUI.SetActive(true);
                }
                else
                {
                    itemUI.SetActive(false);
                }
           
            }
            
        }
    }
}
