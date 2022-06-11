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
    [SerializeField] Image gauge; //�v���O���X�o�[
    [SerializeField] Text text;
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] private float searchAngle = 110f;
    [SerializeField] private GameObject Slender;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;

    private bool isLooking;
    NavMeshAgent agent;
    float attention;
    int state;
    private RaycastHit[] _raycastHits = new RaycastHit[12];

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(true);
        agent = Slender.GetComponent<NavMeshAgent>();
        Slender.GetComponent<Animator>().Play("Walk");
        Vector3 dest = GetDestinationRandomly();
        agent.destination = dest;
        agent.speed = 3f;
        gauge.fillAmount = 0f;
        isLooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attention);
        attention = Mathf.Clamp(attention, 0f, 1f);
        gauge.fillAmount = attention;

        //�����Ă�Ƃ����ӓx���オ��
        if (isLooking)
        {
            attention += 0.08f;
        }
        //�����ĂȂ��Ƃ��͒��ӓx��������������
        else
        {
            attention -= 0.001f;
        }
        //���ӓx0.4�ȏ�̎��A�ǂ�������
        if(attention >= 0.2)
        {
            agent.destination = Player.transform.position;
            Slender.GetComponent<Animator>().Play("Run");
            agent.speed = 4.5f;
        }
        // �ړI�n�t�߂Ŏ��̖ړI�n
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Vector3 dest = GetDestinationRandomly();
            agent.destination = dest;
            Slender.GetComponent<Animator>().Play("Walk");
            agent.speed = 3f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //�@��l���̕���
            var playerDirection = other.transform.position - transform.position;
            //�@�G�̑O������̎�l���̕���
            var angle = Vector3.Angle(transform.forward, playerDirection);

            var positionDiff = other.transform.position - transform.position;
            var distans = positionDiff.magnitude;
            var direction = positionDiff.normalized;
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distans);
            Debug.Log("hitCount: " + hitCount);
            if(hitCount == 3)
            {
                //�@�T�[�`����p�x���������甭��
                if (angle <= searchAngle)
                {
                    //Debug.Log("��l������: " + angle);
                    isLooking = true;
                }
                else
                {
                    if (Vector3.Distance(other.transform.position, transform.position) <= searchArea.radius * 0.5f)
                    {
                        //Debug.Log("��l���𔭌�2");
                        isLooking = true;
                    }
                }
            }
            else
            {
                attention -= 0.002f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("�����ĂȂ�");
            isLooking = false;
        }
    }


    // �����_���ŖړI�n��Ԃ�
    Vector3 GetDestinationRandomly()
    {
        return points.GetChild(Random.Range(0, points.childCount)).transform.position;
    }
}
