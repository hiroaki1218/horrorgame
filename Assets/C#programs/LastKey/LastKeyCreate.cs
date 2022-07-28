using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastKeyCreate : MonoBehaviour
{
    [SerializeField] private GameObject _lastKey;
    public static LastKeyCreate instance;
    public int maxKeyPieceCount = 6;
    private int keyPieceCount;
    public bool canCreateLastKey;
    public bool action;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        action = false;
        canCreateLastKey = false;
        _lastKey.SetActive(false);
        keyPieceCount = 0;
    }
    public void CountUpKeyPiece()
    {
        keyPieceCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(keyPieceCount == maxKeyPieceCount)
            {
                canCreateLastKey = true;
            }
            else
            {
                canCreateLastKey = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canCreateLastKey = false;
    }

    public void OnClickCreateButton()
    {
        action = true;                         //- アニメーション凝らないなら直通でok
    }                                          //|
                                               //|
    private void Update()                      //|
    {                                          //|
        if (action)                            //|
        {                                      //|
            StartCoroutine(GenerateLastKey()); //|
        }                                      //| 
    }                                          //|
    IEnumerator GenerateLastKey()              //↓
    {
        yield return new WaitForSeconds(1);
        _lastKey.SetActive(true);
    }
}
