using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraposition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera phoneCamera;
    Quaternion defaultCameraRot;
    float timer = 0;

    // Use this for initialization
    void Start()
    {
        phoneCamera.enabled = false;
        defaultCameraRot = transform.localRotation; // cameraparent‚Å‚Í‚È‚­©g‚Ì‰ñ“]‚ğ•Û‘¶
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow)) transform.Rotate(-1.2f, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Rotate(1.2f, 0, 0);

        if (Input.GetKey(KeyCode.RightShift))
        {
            mainCamera.enabled = false;
            phoneCamera.enabled = true;
            timer = 0.5f;
        }
        else
        {
            mainCamera.enabled = true;
            phoneCamera.enabled = false;
            phoneCamera.transform.rotation = mainCamera.transform.rotation;
        }
            

        //½Ñ°½Ş‚É¶Ò×‚ğ–ß‚·
        if (timer > 0)
        {
            // cameraparent.transform‚Ì‘ã‚í‚è‚É©g‚Ìtransform‚ğ‰ñ“]
            transform.localRotation = Quaternion.Slerp(transform.localRotation, defaultCameraRot, Time.deltaTime * 10);

            timer -= Time.deltaTime;
        }
    }
}
