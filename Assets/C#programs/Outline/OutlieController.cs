using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlieController : MonoBehaviour
{
    [SerializeField] private GameObject gameobj;
    GameObject mainCamera;
    ItemTalk itemtalk;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("FirstPersonCharacter");
        itemtalk = mainCamera.GetComponent<ItemTalk>();
        outline = gameobj.GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemtalk.isHit == true && ItemTalk.objName == gameobj.name)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
