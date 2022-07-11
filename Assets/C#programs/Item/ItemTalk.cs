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
            RaycastHit hit;  //Rayが当たったオブジェクトの情報が格納される

            //Rayが当たっていれば
            if (Physics.SphereCast(mainCamera.ViewportPointToRay(new Vector2(0.5f,0.5f)),0.3f,out hit,100f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    //Debug.Log("アイテム");
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
