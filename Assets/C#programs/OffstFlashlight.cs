using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffstFlashlight : MonoBehaviour
{
    //[SerializeField] private GameObject SubCamera;
    [SerializeField] private Light Fpslight;
    private Vector3 vectOffset;
    private GameObject goFollow;
    [SerializeField] private float speed = 5.0f;

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
    }

    private void Update()
    {
        if (PickupObj.fpsLight && isRemain)
        {
            Fpslight.enabled = true;
        }
        else
        {
            Fpslight.enabled = false;
        }
        
        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);

        //���鎞�Ԃ����X�Ɍ��炷&�d�r�c�ʂ̊m�F
        if (PickupObj.fpsLight)
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
    //�o�b�e���[�̊T�O
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
