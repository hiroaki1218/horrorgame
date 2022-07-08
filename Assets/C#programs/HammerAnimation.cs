using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HammerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject FirstPerson;
    Animator _anim;
    FirstPersonControllerCustom _fpc;
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
        _anim = FirstPerson.GetComponent<Animator>();
        _fpc = FirstPerson.GetComponent<FirstPersonControllerCustom>();
    }
    void OnClickYesButon()
    {
        Action = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Action && canClick && Input.GetMouseButton(1))
        {
            Action = false;
        }
        ChangeAction();
        StartCoroutine("HammerShake");
    }
    void ChangeAction()
    {
        if (Action)
        {
            if (Input.GetMouseButton(0))
            {
                if (canClick)
                {
                    canShake = true;
                }
            }
        }
    }
    IEnumerator HammerShake() 
    {
        if (canShake)
        {
            canClick = false;
            isHammerShake = true;
            _anim.Play("FPSHammerShake");
            yield return new WaitForSeconds(0.5f);
            isHammerShake = false;
            canClick = true;
            canShake = false;
        }
        else if(Action && !canShake)
        {
            if (_fpc.m_IsStopping)
            {
                _anim.Play("FPSHammerHaving(Idle)");
            }
            else if (_fpc.m_IsWalking)
            {
                _anim.Play("FPSHammerHaving(Walk)");
            }
            else if (_fpc.m_IsRunning)
            {
                _anim.Play("FPSHammerHaving(Run)");
            }
           
        }
    }
}
