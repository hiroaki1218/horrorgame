using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderHead : MonoBehaviour
{
    [SerializeField] private GameObject slenderHead;
    [SerializeField] private GameObject slender;
    // Start is called before the first frame update
    void Start()
    {
        slenderHead.transform.rotation = slender.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        slenderHead.transform.rotation = slender.transform.rotation;
    }
}
