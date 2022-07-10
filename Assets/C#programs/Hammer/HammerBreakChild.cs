using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBreakChild : MonoBehaviour
{
    public static bool canBreak;
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _breakWoodSound;

    private void Start()
    {
        canBreak = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FPSHammer" && HammerAnimation.isHammerShake && canBreak)
        {
            HammerBreak.count++;
            HammerBreak.breaknumber = HammerBreak.count - 1;
            _audiosource.PlayOneShot(_breakWoodSound);
            this.gameObject.SetActive(false);
            canBreak = false;
            HammerBreak.instance.StartCoroutine("WaitBreak");
        }
    }
}
