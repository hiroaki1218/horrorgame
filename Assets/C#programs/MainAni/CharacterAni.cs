using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CharacterAni : MonoBehaviour
{
    [SerializeField] private FirstPersonAnime _fpcanim;
    [SerializeField] private FirstPersonControllerCustom FPC;
    [SerializeField] private CharacterController fps;
    [SerializeField] private Animator _anim;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject CrosshairUI;

    private void Start()
    {
        _fpcanim.enabled = false;
        startCamera.enabled = true;
        FPC.enabled = false;
        fps.enabled = false;
        CrosshairUI.SetActive(false);
    }

    private void Update()
    {
        StartCoroutine(FPSAni());
    }

    IEnumerator FPSAni()
    {
        if (!AniTrigger.enter)
        {
            _anim.Play("BasicMotions@Walk01 - Forwards");
            for (int walk = 0; walk < 20; walk++)
            {
                _anim.gameObject.transform.Translate(0, 0, 0.001f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

}
