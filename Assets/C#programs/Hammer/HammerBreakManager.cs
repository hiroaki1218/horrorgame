using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreakManager : MonoBehaviour
{
    public static HammerBreakManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    IEnumerator WaitBreak()
    {
        yield return new WaitForSeconds(0.9f);
        HammerBreakChild.canBreak = true;
    }
}
