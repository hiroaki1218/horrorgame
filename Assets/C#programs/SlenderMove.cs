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

        //�����Ă�Ƃ����ӓx���オ��
        if (isLooking)
        {
            attention += 0.03f;
        }
        //�����ĂȂ��Ƃ��͒��ӓx��������������
        else
        {
            attention -= 0.02f;
        }
        //���ӓx0.4�ȏ�̎��A�ǂ�������
        if(attention >= 0.4)
        {
            agent.destination = Player.transform.position;
        }
        // �ړI�n�t�߂Ŏ��̖ړI�n
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
            //�@��l���̕���
            var playerDirection = other.transform.position - transform.position;
            //�@�G�̑O������̎�l���̕���
            var angle = Vector3.Angle(transform.forward, playerDirection);
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
