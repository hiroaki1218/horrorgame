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

    private FirstPersonControllerCustom playerController;

    private void Start()
    {
        playerController = GetComponent<FirstPersonControllerCustom>();
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
