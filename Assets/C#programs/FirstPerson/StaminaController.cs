using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class StaminaController : MonoBehaviour
{
    [Header("Stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Regen Parameters")]
    [Range(0,50)][SerializeField] private float staminaDrain = 0.5f;
    [Range(0,50)][SerializeField] private float staminaRegen = 0.5f;

    [Header("Stamina Regen Parameters")]
    [SerializeField] private int slowedRunSpeed = 4;
    [SerializeField] private int normalRunSpeed = 8;

    [Header("Stamina Regen Parameters")]
    [SerializeField] private Image staminaProgressUI1;
    [SerializeField] private Image staminaProgressUI2;
    [SerializeField] private CanvasGroup sliderCamvasGroup;

    //吐息
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _breathsound;

    private FirstPersonControllerCustom playerController;
    private bool one;
    private void Start()
    {
        playerController = GetComponent<FirstPersonControllerCustom>();
        one = true;
    }

    private void Update()
    {
        if (!weAreSprinting)
        {
            if(playerStamina <= maxStamina - 0.01f)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1);

                if(playerStamina >= maxStamina)
                {
                    playerController.SetRunSpeed(normalRunSpeed);
                    sliderCamvasGroup.alpha = 0;
                    hasRegenerated = true;
                }
            }
        }
        //吐息
        if (!hasRegenerated)
        {
            _audiosource.clip = _breathsound;
            if (one)
            {
                one = false;
                _audiosource.PlayOneShot(_breathsound);
            }
        }
        else
        {
            _audiosource.clip = null;
            one = true;
        }
    }

    public void Sprinting()
    {
        if (hasRegenerated)
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            UpdateStamina(1);
            
            if(playerStamina <= 0)
            {
                hasRegenerated = false;
                playerController.SetRunSpeed(slowedRunSpeed);
                sliderCamvasGroup.alpha = 0;
            }
        }

        
    }

    void UpdateStamina(int value)
    {
        staminaProgressUI1.fillAmount = playerStamina / maxStamina;
        staminaProgressUI2.fillAmount = playerStamina / maxStamina;

        if (value == 0)
        {
            sliderCamvasGroup.alpha = 0;
        }
        else
        {
            sliderCamvasGroup.alpha = 1;
        }
    }
}
