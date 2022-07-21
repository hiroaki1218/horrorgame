using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLightManager : MonoBehaviour
{
    public static bool pickupFlashLight;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        pickupFlashLight = false;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(once && PickupObj.fpsLight)
        {
            pickupFlashLight = true;
            once = false;
        }
    }
}
