using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SlenderMove : MonoBehaviour
{
    [SerializeField] FirstPersonControllerCustom sc_fps;
    [SerializeField] Transform points;
    [SerializeField] Image gauge; //プログレスバー
    [SerializeField] Text text;
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] private float searchAngle = 110f;
    [SerializeField] private GameObject Slender;
    [SerializeField] private GameObject Player;

    private bool isLooking;
    NavMeshAgent agent;
    float attention;
    int state;

    // Start is called before the first frame update
    void Start()
    {
        agent = Slender.GetComponent<NavMeshAgent>();
        Vector3 dest = GetDestinationRandomly();
        agent.destination = dest;
        gauge.fillAmount = 0f;
        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attention);
        attention = Mathf.Clamp(attention, 0f, 1f);

        //見えてるとき注意度が上がる
        if (isLooking)
        {
            attention += 0.03f;
        }
        //見えてないときは注意度が少しずつ下がる
        else
        {
            attention -= 0.02f;
        }
        //注意度0.4以上の時、追いかける
        if(attention >= 0.4)
        {
            agent.destination = Player.transform.position;
        }
        // 目的地付近で次の目的地
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Vector3 dest = GetDestinationRandomly();
            agent.destination = dest;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //　主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //　サーチする角度内だったら発見
            if (angle <= searchAngle)
            {
                //Debug.Log("主人公発見: " + angle);
                isLooking = true;
            }
            else
            {
                if (Vector3.Distance(other.transform.position, transform.position) <= searchArea.radius * 0.5f)
                {
                    //Debug.Log("主人公を発見2");
                    isLooking = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("見えてない");
            isLooking = false;
        }
    }


    // ランダムで目的地を返す
    Vector3 GetDestinationRandomly()
    {
        return points.GetChild(Random.Range(0, points.childCount)).transform.position;
    }
}
