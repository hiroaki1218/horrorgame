using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera SubCamera;
    public GameObject maincamera;
    public GameObject subcamera;
    public GameObject player;
    public Transform target;
    public float speed = 5f;
    [SerializeField] private float FirstRotateTime = 9.3f;
    [SerializeField] private float SecondRotateTime = 1f;

    private GameObject goFollow;
    private Vector3 vectOffset;
    private bool finish1;
    public static bool finish2;
    public static bool seconding;

    private void Start()
    {
        seconding = false;
        finish1 = false;
        finish2 = false;
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

        goFollow = maincamera;
        vectOffset = transform.position - goFollow.transform.position;

        FirstPersonControllerCustom fpc = player.GetComponent<FirstPersonControllerCustom>();
        fpc.enabled = false;
        Vector3 direction = target.position - transform.position;
        Quaternion rotation1 = Quaternion.LookRotation(direction);
        if (!finish1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation1, speed * Time.deltaTime);
        }
        yield return new WaitForSeconds(FirstRotateTime);

        if (!finish2)
        {
            transform.position = goFollow.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
        }

        yield return new WaitForSeconds(SecondRotateTime);

        seconding = false;
        subcamera.SetActive(false);
        MainCamera.enabled = true;
        fpc.enabled = true;
    }
    IEnumerator Count()
    {
        yield return new WaitForSeconds(FirstRotateTime);
        finish1 = true;
        yield return new WaitForSeconds(SecondRotateTime);
        finish2 = true;
    }
}
