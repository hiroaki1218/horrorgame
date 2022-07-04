using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PhoneAnimation : MonoBehaviour
{
    [SerializeField] private GameObject SmartPhone;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera PhoneCamera;
    [SerializeField] private Camera TrainCamera;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject CrosshairUI;
    [SerializeField] private GameObject FirstPerson;
    [SerializeField] private GameObject CameraChangeButton;
    [SerializeField] private GameObject ExitButton;
    FirstPersonControllerCustom fpc;
    GameObject player;
    public static bool Active;
    private bool OntoOff;
    private bool finish1;
    private bool finish2;
    //private bool isBrank;
    private bool cameraBack;
    public static bool isLookPhone;
    public static bool FlashLightEnabled;
    Animator _anim;

    public static PhoneAnimation instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        TrainCamera.enabled = false;
        SmartPhone.SetActive(false);
    }
    void Start()
    {
        player = GameObject.Find("FPSController");
        fpc = player.GetComponent<FirstPersonControllerCustom>();
        PhoneCamera.enabled = false;
        OntoOff = false;
        finish1 = true;
        finish2 = true;
        //isBrank = false;
        cameraBack = true;
        isLookPhone = false;
        FlashLightEnabled = false;
        CameraChangeButton.SetActive(false);
        ExitButton.SetActive(false);
        _anim = FirstPerson.GetComponent<Animator>();
    }

    void Update()
    {
        StartProcessing();
    }

    public void OnclickExitButton()
    {
        Active = false;
        StartCoroutine("FPSenabled");
        Cursor.lockState = CursorLockMode.Locked;     // 標準モード
        Cursor.visible = false;    // カーソル表示
    }
    public void OnclickYesButton()
    {
        if (SlenderAttack.canCheckTabandItems)
        {
            Active = true;
            StartCoroutine("ButtonDilay");
            CrosshairUI.SetActive(false);
            fpc.enabled = false;
            isLookPhone = true;
            FlashLightEnabled = true;
            Cursor.lockState = CursorLockMode.None;     // 標準モード
            Cursor.visible = true;    // カーソル表示
        }
        else
        {
            return;
        }
    }
    public void StartProcessing()
    {
        if(!Inventory.inventory && !Menu.pause && !Memo.LookMemo && !Memo.exitMemo1)
        {
            if (finish1 && finish2)
            {

            }
            StartCoroutine("OnOffPhone");
        }   
    }
    IEnumerator OnOffPhone()
    {
        Vector3 direction = target1.transform.position - PhoneCamera.transform.position;
        if (!Active)
        {
            if (!OntoOff)
            {
                mainCamera.enabled = true;
                PhoneCamera.enabled = false;
                PhoneCamera.transform.rotation = mainCamera.transform.rotation;
            }
            else
            {
                finish2 = false;
                cameraBack = true;  
                SmartPhone.GetComponent<Animator>().Play("PhoneOff");
                _anim.Play("FPSPhoneOff");
                yield return new WaitForSeconds(0.37f);
                SmartPhone.SetActive(false);
                yield return new WaitForSeconds(0.63f);
                if (cameraBack)
                {
                    PhoneCamera.transform.position = mainCamera.transform.position;
                    PhoneCamera.transform.rotation = Quaternion.Slerp(PhoneCamera.transform.rotation, mainCamera.transform.rotation, 5 * Time.deltaTime);
                }
                else
                {
                    yield return new WaitForSeconds(0.8f);
                    PhoneCamera.transform.position = TrainCamera.transform.position;
                }
                yield return new WaitForSeconds(1f);
                cameraBack = false;
                OntoOff = false;
                finish2 = true;
            }
        }
        else if (Active)
        {
            finish1 = false;
            mainCamera.enabled = false;
            PhoneCamera.enabled = true;   

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            PhoneCamera.transform.rotation = Quaternion.Slerp(PhoneCamera.transform.rotation, targetRotation, 5 * Time.deltaTime);

            SmartPhone.SetActive(true);
            SmartPhone.GetComponent<Animator>().Play("PhoneOn");
            _anim.Play("FPSPhoneOn");

            yield return new WaitForSeconds(1.3f);
            OntoOff = true;
            finish1 = true;
        }
    }
    IEnumerator FPSenabled()
    {
        CameraChangeButton.SetActive(false);
        ExitButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        FlashLightEnabled = false;
        yield return new WaitForSeconds(2.0f);
        fpc.enabled = true;
        isLookPhone = false;
        CrosshairUI.SetActive(true);
    }

    IEnumerator ButtonDilay()
    {
        yield return new WaitForSeconds(1.3f);
        Inventory.canPushTab = false;
        CameraChangeButton.SetActive(true);
        ExitButton.SetActive(true);
    }

}
