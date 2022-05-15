using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffstFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject SubCamera;
    private Vector3 vectOffset;
    private GameObject goFollow;
    [SerializeField] private float speed = 5.0f;

    private void Start()
    {
        goFollow = Camera.main.gameObject;
        vectOffset = transform.position - goFollow.transform.position;
    }

    private void Update()
    {
        if (!LookAtTarget.seconding)
        {
            transform.position = goFollow.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
        }
        else
        {
            transform.position = SubCamera.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, SubCamera.transform.rotation, speed * Time.deltaTime);
        }
    }
}
