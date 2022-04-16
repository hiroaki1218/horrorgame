using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlLighrFPS : MonoBehaviour
{

    [SerializeField] GameObject fpslight;

    public bool on = false;

    public void onSwitch()
    {
        if ( on == true)
        {
            fpslight.SetActive(true);
        }
        else if ( on == false)
        {
            fpslight.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onSwitch();
    }
}
