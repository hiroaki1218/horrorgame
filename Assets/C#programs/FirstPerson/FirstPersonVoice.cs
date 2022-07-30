using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonVoice : MonoBehaviour
{
    [SerializeField] private GameObject GetItemUI;
    [SerializeField] private Text text;
    public static FirstPersonVoice instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GetItemUI.SetActive(false);
        text.text = null;
    }
    //アイテム拾ったとき
    public void ItemVoice(Items item)
    {
        StartCoroutine(GetItemVoice(item));
    }
    IEnumerator GetItemVoice(Items item)
    {
        GetItemUI.SetActive(true);
        text.text = item.itemname + "を拾った。";
        yield return new WaitForSeconds(1.3f);
        GetItemUI.SetActive(false);
        text.text = null;
    }
}
