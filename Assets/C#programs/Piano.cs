using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip PianoSound;
    private bool CanPushPiano;
    public static bool pianoPushed;

    private void Start()
    {
        CanPushPiano = false;
        pianoPushed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanPushPiano = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanPushPiano = false;
        }
    }

    private void Update()
    {
        if (CanPushPiano)
        {
            if (!Menu.pause)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(AnlockDoor());
                }
            }
        }
    }

    IEnumerator AnlockDoor()
    {
        _audioSource.PlayOneShot(PianoSound);
        yield return new WaitForSeconds(5f);
        pianoPushed = true;
    }
}
