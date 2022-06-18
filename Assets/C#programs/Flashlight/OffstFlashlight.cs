using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffstFlashlight : MonoBehaviour
{
    //[SerializeField] private GameObject SubCamera;
    [SerializeField] private Light Fpslight;
    private Vector3 vectOffset;
    private GameObject goFollow;
    [SerializeField] private float speed = 5.0f;

    [SerializeField] private GameObject FlashlightUI;
    [SerializeField] private GameObject SubCamera;
    [SerializeField] private GameObject PhoneCamera;
    [SerializeField] private Image bar;
    [SerializeField] private float maxglowTime = 100.0f;
    [SerializeField] private float Glowingtime = 100.0f;
    [SerializeField] private float decrease = 2.0f;
    [SerializeField] private float batteryRecovery = 20.0f;
    [HideInInspector] public bool isRemain;
    

    public static OffstFlashlight instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        isRemain = true;
        goFollow = Camera.main.gameObject;
        vectOffset = transform.position - goFollow.transform.position;
        Fpslight.enabled = false;
        FlashlightUI.SetActive(false);
    }

    private void Update()
    {
        bar.fillAmount = Glowingtime / maxglowTime;
        
        if (PickupObj.fpsLight && isRemain && !PhoneAnimation.FlashLightEnabled)
        {
             FlashlightUI.SetActive(true);
             Fpslight.enabled = true;
        }
        else
        {
             FlashlightUI.SetActive(false);
             Fpslight.enabled = false;
        }


        if (Memo.exitMemo1)
        {
            transform.position = goFollow.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, SubCamera.transform.rotation, speed * Time.deltaTime);
        }
        else if (!PhoneAnimation.FlashLightEnabled)
        {
            transform.position = goFollow.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, PhoneCamera.transform.rotation, speed * Time.deltaTime);
        }
        else
        {
            transform.position = goFollow.transform.position + vectOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
        }
        

        //光る時間を徐々に減らす&電池残量の確認
        if (PickupObj.fpsLight && !PhoneAnimation.isLookPhone)
        {
            if(Glowingtime >= 0)
            {
                Glowingtime -= decrease * Time.deltaTime;
                isRemain = true;
            }
            else
            {
                isRemain = false;
            }
        }
        
    }
    //バッテリーの概念
    public void SetBattery()
    {
        if(Glowingtime <= maxglowTime - 0.01f)
        {
            if((Glowingtime += batteryRecovery) > maxglowTime)
            {
                Glowingtime = maxglowTime;
            }
        }
    }
}
