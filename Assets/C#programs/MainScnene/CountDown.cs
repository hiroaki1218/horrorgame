using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] float gameTime = 600.0f;
    [SerializeField] private GameObject countUI;
    [SerializeField] private Image slider;
    float maincount;
    float second;
    

    private void Start()
    {
        slider.fillAmount = 1;
        countUI.SetActive(true);
        maincount = gameTime;
    }
    private void Update()
    {
        if(maincount >= 0.0f)
        {
            maincount -= Time.deltaTime;
            second = maincount / gameTime;
            slider.fillAmount = second;
        }
        //Debug.Log(maincount);
    }
}
