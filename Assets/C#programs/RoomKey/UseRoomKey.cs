using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRoomKey : MonoBehaviour
{
    [SerializeField] private GameObject door;
    Door _door;
    public static UseRoomKey instance;
    public bool canUseRoomKey;
    public bool Active;
    private bool once;
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
        _door = door.GetComponent<Door>();
        once = true;
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
        if (Active && once)
        {
            _door.StartCoroutine(_door.DoorOpenWait());
            once = false;
        }
    }
}
