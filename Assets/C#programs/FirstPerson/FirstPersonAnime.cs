using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonAnime : MonoBehaviour
{
    [SerializeField] private GameObject FirstPerson;
    Animator _anim;
    FirstPersonControllerCustom _fpc;
    GameObject fpc;
    // Start is called before the first frame update
    void Start()
    {
        fpc = GameObject.Find("FPSController");
        _fpc = fpc.GetComponent<FirstPersonControllerCustom>();
        _anim = FirstPerson.GetComponent<Animator>();
        _anim.Play("BasicMotions@Idle01");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("FPSAnime");
    }
    IEnumerator FPSAnime()
    {
        if (PhoneAnimation.isLookPhone)
        {
           
        }
        else if (Menu.pause || Memo.exitMemo1 || Memo.LookMemo)
        {
            _anim.Play("BasicMotions@Idle01");
        }
        else if (_fpc.m_IsStopping)
        {
            yield return new WaitForSeconds(0f);
            _anim.Play("BasicMotions@Idle01");
        }
        else if (_fpc.m_IsWalking)
        {
            _anim.Play("BasicMotions@Walk01 - Forwards");
        }
        else if (_fpc.m_IsRunning)
        {
            _anim.Play("BasicMotions@Run01 - Forwards");
        }
    }
}
