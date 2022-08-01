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
        if (thirdRank == null)
        {
            thirdRank = n;
            displayText.text = thirdRank;
        }
        else if(secondRank == null)
        {
            secondRank = n;
            displayText.text = thirdRank + secondRank;
        }
        else if(firstRank == null)
        {
            firstRank = n;
            displayText.text = thirdRank + secondRank + firstRank;
        }
    }
    void OnClickBackButton()
    {
        if (thirdRank == null)
        {
            
        }
        else if(secondRank == null)
        {
            thirdRank = null;
            displayText.text = thirdRank + secondRank + firstRank;
        }
        else if(firstRank == null)
        {
            secondRank= null;
            displayText.text = thirdRank + secondRank + firstRank;
        }
        else
        {
            firstRank = null;
            displayText.text = thirdRank + secondRank + firstRank;
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
            Debug.Log("不正解");
            //赤のUI
            yield return new WaitForSeconds(1);
            //赤のUI消す
        }
    }
    private void Update()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            //マウスカーソルの位置からカメラの画像を通してレイを飛ばす
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5))
            {
                if(hit.collider.tag == "Key1")
                {
                    OnClickNumButton("1");
                }
                else if(hit.collider.tag == "Key2")
                {
                    OnClickNumButton("2");
                }
                else if (hit.collider.tag == "Key3")
                {
                    OnClickNumButton("3");
                }
                else if (hit.collider.tag == "Key4")
                {
                    OnClickNumButton("4");
                }
                else if (hit.collider.tag == "Key5")
                {
                    OnClickNumButton("5");
                }
                else if (hit.collider.tag == "Key6")
                {
                    OnClickNumButton("6");
                }
                else if (hit.collider.tag == "Key7")
                {
                    OnClickNumButton("7");
                }
                else if (hit.collider.tag == "Key8")
                {
                    OnClickNumButton("8");
                }
                else if (hit.collider.tag == "Key9")
                {
                    OnClickNumButton("9");
                }
                else if (hit.collider.tag == "Key0")
                {
                    OnClickNumButton("0");
                }
                else if (hit.collider.tag == "KeyBack")
                {
                    OnClickBackButton();
                }
                else if (hit.collider.tag == "KeyEnter")
                {
                    OnClickEnterButton();
                }
            }
        }
    }
}
