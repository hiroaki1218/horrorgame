using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject FirstPerson;
    [SerializeField] private GameObject FPSHammer;
    GameObject _fpc;
    FirstPersonControllerCustom fpc;
    Animator _anim;
    public static HammerAnimation instance;
    public bool Action;
    private bool canShake;
    private bool canClick;
    public static bool isHammerShake;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Action = false;
        canShake = false;
        canClick = true;
        isHammerShake = false;
        FPSHammer.SetActive(false);
        _anim = FirstPerson.GetComponent<Animator>();
        _fpc = GameObject.Find("FPSController");
        fpc = _fpc.GetComponent<FirstPersonControllerCustom>();
    }
    public void OnClickYesButon()
    {
        Action = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Action && canClick && Input.GetMouseButton(1))
        {
            Action = false;
            Inventory.canPushTab = false;
        }
        ChangeAction();
        StartCoroutine("HammerShake");
    }
    void ChangeAction()
    {
        if (Action)
        {
            FPSHammer.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                if (canClick)
                {
                    canShake = true;
                }
            }
        }
        else
        {
            FPSHammer.SetActive(false);
        }
    }
    IEnumerator HammerShake() 
    {
        if (canShake && Action && !isHammerShake && !Menu.pause)
        {
            canClick = false;
            isHammerShake = true;
            _anim.Play("FPSHammerShake");
            yield return new WaitForSeconds(1f);
            isHammerShake = false;
            canClick = true;
            canShake = false;
        }
        else if(Action && !canShake)
        {
            if (fpc.m_IsStopping)
            {
                MouseLook.MaximumX = 57f;
                _anim.Play("BasicMotions@Idle01");
            }
            else if (fpc.m_IsWalking)
            {
                MouseLook.MaximumX = 57f;
                _anim.Play("BasicMotions@Walk01 - Forwards");
            }
            else if (fpc.m_IsRunning)
            {
                MouseLook.MaximumX = 47f;
                _anim.Play("BasicMotions@Run01 - Forwards");
            }
           
        }
    }
}
