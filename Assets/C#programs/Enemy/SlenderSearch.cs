using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderSearch : MonoBehaviour
{
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] private float searchAngle = 110f;
    public static bool onAngle;
    // Start is called before the first frame update
    void Start()
    {
        onAngle = false;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            //　主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //　サーチする角度内だったら発見
            if (angle <= searchAngle)
            {
                //Debug.Log("主人公発見: " + angle);
                onAngle = true;
            }
            else
            {
                if (Vector3.Distance(other.transform.position, transform.position) <= searchArea.radius * 0.5f)
                {
                    //Debug.Log("主人公を発見2");
                    onAngle = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            onAngle = false;
        }
    }
}
