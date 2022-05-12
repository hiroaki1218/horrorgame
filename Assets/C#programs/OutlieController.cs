using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlieController : MonoBehaviour
{
    GameObject mainCamera;
    ItemTalk itemtalk;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        mainCamera = GameObject.Find("FirstPersonCharacter");
        itemtalk = mainCamera.GetComponent<ItemTalk>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemtalk.isHit == true)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }
}
