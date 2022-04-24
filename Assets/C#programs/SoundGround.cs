using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class SoundGround : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "OnWater")
        {
            Debug.Log("water");
        }
        //if(other.gameObject.tag == "Terrain")
        //{
            //Debug.Log("T");
        //}
    }
}