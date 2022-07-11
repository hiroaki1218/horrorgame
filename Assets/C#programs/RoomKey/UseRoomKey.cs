using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRoomKey : MonoBehaviour
{
    public static UseRoomKey instance;
    public bool canUseRoomKey;
    public bool Active;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canUseRoomKey = false;
        Active = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canUseRoomKey = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canUseRoomKey = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            //ドアを開ける処理
        }
    }
}
