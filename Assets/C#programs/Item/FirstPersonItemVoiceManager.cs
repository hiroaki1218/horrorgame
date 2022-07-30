using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonItemVoiceManager : MonoBehaviour
{
    public static FirstPersonItemVoiceManager instance;
    private int count;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        count = 0;
    }
    public IEnumerator ItemVoice(Items item)
    {
        count++;
        if(count == 2)
        {
            yield return new WaitForSeconds(1.5f);
        }
        else if(count == 3)
        {
            yield return new WaitForSeconds(3.0f);
        }
        else if (count == 4)
        {
            yield return new WaitForSeconds(4.5f);
        }
        else if (count == 5)
        {
            yield return new WaitForSeconds(6.0f);
        }
        FirstPersonVoice.instance.ItemVoice(item);
        yield return new WaitForSeconds(1.5f);
        count--;
    }
}
