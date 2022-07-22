using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LensAnimation : MonoBehaviour
{
    [SerializeField] private GameObject FpsLens;
    [SerializeField] private Animator _anim;
    [SerializeField] private FirstPersonControllerCustom _fpc;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera subCamera;
    [SerializeField] private Camera TrainCamera;
    [SerializeField] private Camera LensCamera;
    [SerializeField] private float CameraRotateTime;
    [SerializeField] private GameObject target;
    private bool Active;
    private bool OntoOff;
    private bool finish1;
    private bool finish2;
    private bool once;
    private bool Once;
    private bool cameraBack;
    private bool canClickMouseButton;
    public static bool isLookLens;
    public static bool FlashLightenabled;
    public static LensAnimation instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        TrainCamera.enabled = false;
    }

    private void Start()
    {
        Active = false;
        OntoOff = false;
        finish1 = true;
        finish2 = true;
        cameraBack = true;
        canClickMouseButton = false;
        subCamera.enabled = false;
        once = true;
        Once = true;
        isLookLens = false;
        FlashLightenabled = false;
        FpsLens.SetActive(false);
        LensCamera.enabled = false;
    }
    //タブで使うボタン押したとき
    public void OnClickYesButton()                   
    {
        Active = true;
        _fpc.enabled = false;     //動けない
        isLookLens = true;
        FlashLightenabled = true;
    }

    //右クリかなんかでレンズの使用をやめたとき
    public void OnClickExitButton()
    {
        Active = false;
        StartCoroutine(CanMoveWait());
    }

    //カメラが元の位置に戻ってから動ける
    IEnumerator CanMoveWait()
    {
        yield return new WaitForSeconds(0.8f);
        FlashLightenabled = false;
        yield return new WaitForSeconds(CameraRotateTime);
        _fpc.enabled = true;      //動ける
        isLookLens = false;
        Inventory.canPushTab = false;
    }

    private void Update()
    {
        //右クリしたら、レンズの使用をやめる
        if (canClickMouseButton)
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnClickExitButton();
                canClickMouseButton = false;
            }
        } 
        StartCoroutine(Main());  
    }
    //使う時と使うのをやめたときの処理（メインの処理）
    IEnumerator Main()
    {
        Vector3 direction = target.transform.position - subCamera.transform.position;
        if (!Active)
        {
            canClickMouseButton = false;
            if (!OntoOff)
            {
                if (once)
                {
                    mainCamera.enabled = true;
                    subCamera.enabled = false;
                    once = false;
                }
                subCamera.transform.rotation = mainCamera.transform.rotation;
            }
            else
            {
                finish2 = false;
                cameraBack = true;
                _anim.Play("LensDown");
                yield return new WaitForSeconds(0.37f);
                FpsLens.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                if (cameraBack)
                {
                    subCamera.transform.position = mainCamera.transform.position;
                    subCamera.transform.rotation = Quaternion.Slerp(subCamera.transform.rotation, mainCamera.transform.rotation, 5 * Time.deltaTime);
                }
                else
                {
                    yield return new WaitForSeconds(0.8f);
                    subCamera.transform.position = TrainCamera.transform.position;
                }
                yield return new WaitForSeconds(0.2f);
                cameraBack = false;
                OntoOff = false;
                finish2 = true;
                once = true;
                Once = true;
            }
        }
        else if (Active)
        {
            finish1 = false;
            mainCamera.enabled = false;
            subCamera.enabled = true;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            subCamera.transform.rotation = Quaternion.Slerp(subCamera.transform.rotation, targetRotation, 5 * Time.deltaTime);

            FpsLens.SetActive(true);
            _anim.Play("LensUP");

            yield return new WaitForSeconds(1.3f);
            OntoOff = true;
            finish1 = true;
            if (Once)
            {
                canClickMouseButton = true;
                Once = false;
            }
        }
    }

}
