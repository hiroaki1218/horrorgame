using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreakChild : MonoBehaviour
{
    public int breakNum;
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _breakWoodSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FPSHammer" && HammerAnimation.isHammerShake && breakNum == HammerBreak.count)
        {
            HammerBreak.count++;
            HammerBreak.breaknumber = HammerBreak.count - 1;
            _audiosource.PlayOneShot(_breakWoodSound);
            this.gameObject.SetActive(false);
        }
    }
}
