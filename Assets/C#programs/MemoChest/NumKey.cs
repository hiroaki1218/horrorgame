using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumKey : MonoBehaviour
{
    [SerializeField] private Text displayText;
    public string TrueThirdRank;
    public string TrueSecondRank;
    public string TrueFirstRank;

    private string firstRank;
    private string secondRank;
    private string thirdRank;

    private void Start()
    {
        firstRank = null;
        secondRank = null;
        thirdRank = null;
        displayText.text = thirdRank + secondRank + firstRank;
    }
    public void OnClickNumButton(string n)
    {
        displayText.text = thirdRank + secondRank + firstRank;
        if (firstRank == null)
        {
            firstRank = n;
        }
        else if(secondRank == null)
        {
            secondRank = n;
        }
        else if(thirdRank == null)
        {
            thirdRank = n;
        }
    }
    public void OnClickBackButton()
    {
        displayText.text = thirdRank + secondRank + firstRank;
        if (firstRank == null)
        {
            return;
        }
        else if(secondRank == null)
        {
            firstRank = null;
        }
        else if(thirdRank == null)
        {
            firstRank = null;
            firstRank = secondRank;
        }
        else
        {
            firstRank = null;
            firstRank = secondRank;
            secondRank = null;
            secondRank = thirdRank;
        }
    }
    public void OnClickEnterButton()
    {
        StartCoroutine(Check());
    }
    IEnumerator Check()
    {
        if(thirdRank == TrueThirdRank 
        && secondRank == TrueSecondRank
        && firstRank == TrueFirstRank)
        {
            Debug.Log("正解");
            //緑のUI
            yield return new WaitForSeconds(1);
            //緑のUI消す
        }
        else
        {
            //赤のUI
            yield return new WaitForSeconds(1);
            //赤のUI消す
        }
    }
}
