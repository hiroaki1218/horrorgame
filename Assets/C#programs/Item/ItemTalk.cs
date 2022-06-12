using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTalk : MonoBehaviour
{
    [SerializeField]
    public GameObject itemUI;
    [SerializeField]
    public Camera mainCamera;
    public static string objName;
    public bool isHit;

    private void Start()
    {
        isHit = false;
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
                    //Debug.Log("�A�C�e��");
                    itemUI.SetActive(true);
                    objName = hit.transform.gameObject.name;
                    isHit = true;
                }
                else
                {
                    itemUI.SetActive(false);
                    isHit = false;
                }
           
            }
            
        }
    }
}
