using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera SubCamera;
    [SerializeField] private GameObject FPSchr;
    public GameObject maincamera;
    public GameObject subcamera;
    public GameObject player;
    public Transform target;
    public float speed = 5f;
    [SerializeField] private float FirstRotateTime = 9.3f;
    [SerializeField] private float SecondRotateTime = 1f;

    private GameObject goFollow;
    private Vector3 vectOffset;
    private Vector3 tmp;
    private bool finish1;
    public static bool seconding;

    private void Start()
    {
        seconding = false;
        finish1 = false;
        SubCamera.enabled = false;
    }
    private void Update()
    {
        if (FPSTrigger.close)
        {
            StartCoroutine("CameraMove");
            StartCoroutine("Count");
        }
    }

    IEnumerator CameraMove()
    {
        seconding = true;
        SubCamera.enabled = true;
        MainCamera.enabled = false;
        
        FirstPersonControllerCustom fpc = player.GetComponent<FirstPersonControllerCustom>();
        fpc.enabled = false;
        Vector3 direction = target.position - transform.position;
        Quaternion rotation1 = Quaternion.LookRotation(direction);

        if (!finish1)
        {
            subcamera.transform.rotation = Quaternion.Lerp(subcamera.transform.rotation, rotation1, speed * Time.deltaTime);

            //FPSchr.transform.rotation = Quaternion.Slerp(FPSchr.transform.rotation, rotation1, speed * Time.deltaTime);
        }
        if (finish1)
        {
            Vector3 direction2 = target.position - transform.position;
            Quaternion rotation2 = Quaternion.LookRotation(-direction2);
            yield return new WaitForSeconds(1);
            subcamera.transform.rotation = Quaternion.Lerp(transform.rotation, rotation2, speed * Time.deltaTime);
            subcamera.transform.rotation = maincamera.transform.rotation;
            subcamera.SetActive(false);
            MainCamera.enabled = true;
            fpc.enabled = true;
            seconding = false;
        }  
    }
    IEnumerator Count()
    {
        yield return new WaitForSeconds(FirstRotateTime);
        finish1 = true;
    }
}
