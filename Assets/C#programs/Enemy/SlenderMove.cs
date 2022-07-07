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
    [SerializeField] private GameObject Slender;
    [SerializeField] private GameObject SlenderMesh;
    [SerializeField] private Collider SlenderCollider;
    [SerializeField] private GameObject AudioSource;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;

    //Sound
    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _LookSound;
    private bool canLookSound;

    private AudioSource ads;
    private bool isLooking;
    private bool firstMove;
    NavMeshAgent agent;
    float attention;
    int state;
    private RaycastHit[] _raycastHits = new RaycastHit[12];

    public static SlenderMove instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Slender.SetActive(false);
        UI.SetActive(true);
        agent = Slender.GetComponent<NavMeshAgent>();
        Slender.GetComponent<Animator>().Play("Walk");
        agent.speed = 3f;
        gauge.fillAmount = 0f;
        isLooking = false;
        firstMove = true;
        ads = AudioSource.GetComponent<AudioSource>();
        //Audio
        canLookSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        attention = Mathf.Clamp(attention, 0f, 1f);
        gauge.fillAmount = attention;
        if (!firstMove)
        {
            Slender.SetActive(true);
            ads.enabled = true;
            ads.pitch = attention * 2;

            //時間が無くなったとき（10分になったとき）
            if (ClockController.timeIsUP)
            {
                agent.destination = Player.transform.position;
                agent.speed = 13f;
            }
            else
            {
                //見えてるとき注意度が上がる
                if (isLooking)
                {
                    attention += 0.06f;
                }
                //見えてないときは注意度が少しずつ下がる
                else
                {
                    if (!Memo.Memo1 && !Memo.Memo2 && !Memo.Memo3 && !Memo.Memo4 && !Memo.Memo5 && !Memo.Memo6 && !Menu.pause)
                    {
                        attention -= 0.003f;
                    }
                }
                //注意度0.2以上の時、追いかける
                if (attention >= 0.2)
                {
                    //Audio
                    if (canLookSound)
                    {
                        _audiosource.PlayOneShot(_LookSound);
                        canLookSound = false;
                    }

                    agent.destination = Player.transform.position;
                    Slender.GetComponent<Animator>().Play("Run");
                    agent.speed = 6f;
                }
                // 目的地付近で次の目的地
                else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    Vector3 dest = GetDestinationRandomly();
                    agent.destination = dest;
                    Slender.GetComponent<Animator>().Play("Walk");
                    agent.speed = 3f;
                }
                else
                {
                    canLookSound = true;
                }
            } 
        }
    }

    //最初の動き
    public void SlenderFirstMove()
    {
        Slender.SetActive(true);
        ads.enabled = false;
        StartCoroutine("SFirstMove"); 
    }

    IEnumerator SFirstMove()
    {
        Slender.GetComponent<Animator>().Play("Walk");
        agent.speed = 3f;
        Vector3 dest = points.GetChild(5).transform.position;
        agent.destination = dest;
        yield return new WaitForSeconds(2.7f);
        SlenderMesh.SetActive(false);
        SlenderCollider.enabled = false;
        yield return new WaitForSeconds(1.3f);
        agent.speed = 15f;
        dest = points.GetChild(8).transform.position;
        agent.destination = dest;
        yield return new WaitForSeconds(25);
        SlenderMesh.SetActive(true);
        SlenderCollider.enabled = true;
        firstMove = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var positionDiff = other.transform.position - transform.position;
            var distans = positionDiff.magnitude;
            var direction = positionDiff.normalized;
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distans);
            Debug.Log(hitCount);
            if (hitCount == 2)
            {
                if (SlenderSearch.onAngle)
                {
                    isLooking = true;
                }
                else
                {
                    isLooking = false;
                }
            }
            else
            {
                if(!Memo.Memo1 && !Memo.Memo2 && !Memo.Memo3 && !Memo.Memo4 && !Memo.Memo5 && !Memo.Memo6 && !Menu.pause)
                {
                    attention -= 0.002f;
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
