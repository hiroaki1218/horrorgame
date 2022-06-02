using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class SlenderMove : MonoBehaviour
{
    [SerializeField] FirstPersonControllerCustom sc_fps;
    [SerializeField] Transform points;
    [SerializeField] Image gauge; //�v���O���X�o�[
    [SerializeField] Text text;
    [SerializeField] GameObject SlenderHead;
   //[SerializeField] GameObject spherePrefab;

    GameObject sphere;
    Transform head;
    Transform player;
    Transform playerHead;
    NavMeshAgent agent;
    float attention;
    int state;

    // Start is called before the first frame update
    void Start()
    {
        //fpsplayer = GameObject.Find("FPSController");
        //sc_fps = fpsplayer.GetComponent<FirstPersonControllerCustom>();
        player = sc_fps.transform;
        head = SlenderHead.transform;
        playerHead = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();

        Vector3 dest = GetDestinationRandomly();
        agent.destination = dest;
        //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);

        gauge.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headToPlayerDir = playerHead.position - head.position;
        float distanceFromPlayer = Vector3.Magnitude(headToPlayerDir);
        float increaseAttention = 0f;

        //���C������������
        headToPlayerDir = Vector3.Normalize(headToPlayerDir);
        float dot = Vector3.Dot(headToPlayerDir, head.forward);

        if (dot > 0)
        {
            increaseAttention += 0.05f * dot;
            text.text = "\nA:�����Ă���";

            //�v���C���[�������Ă��Ȃ��Ƃ�
            if (sc_fps.GetIsWalking())
            {
                text.text = "\nP:�����Ă��Ȃ�";
                attention -= 0.008f; // ���ӂ̃��x����������            
            }
            //�v���C���[�������Ă���Ƃ�
            else
            {
                text.text = "\nP:�����Ă���";
                increaseAttention += 0.03f; // ���ӂ̃��x�����グ��
                increaseAttention *= Mathf.Clamp(3f / distanceFromPlayer, 0f, 1f);
            }
        }
        else
        {
            text.text = "\nA:�����Ă��Ȃ�";
        }

        // �ǂ��Ă��Ȃ����
        if (state == 0)
        {
            if (attention <= 0f)
            {
                attention = 0f;
            }

            text.text += "\nA:���񂵂Ă���";

            // 1�ȏ�̂Ƃ��ǂ��Ă����Ԃɂ���
            if (attention >= 1f)
            {
                ///attention = 1f;
                state = 1;
                agent.destination = player.position; // �v���C���[�̏ꏊ�ɍs��
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);
            }
            // �ړI�n�t�߂Ŏ��̖ړI�n��ݒ�
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 dest = GetDestinationRandomly();
                agent.destination = dest;
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);

            }

        }
        // �ǂ��Ă�����
        else if (state == 1)
        {

            text.text += "\nA:�ǂ��Ă���";
            agent.destination = player.position; // �v���C���[�̏ꏊ�ɍs��
                                                 // Destroy(sphere);
                                                 //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);

            // 1�ȏ�̂Ƃ�1
            if (attention >= 1f)
            {
                ///attention = 1f;
            }
            // �������Ă����Ԃɂ���
            else
            {
                state = 2;
            }
        }

        // �������Ă�����
        else if (state == 2)
        {
            text.text += "\nA:�������Ă���";

            // 1�ȏ�̂Ƃ��ǂ��Ă����Ԃɂ���
            if (attention >= 1f)
            {
                ///attention = 1f;
                state = 1;
                agent.destination = player.position; // �v���C���[�̏ꏊ�ɍs��
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);
            }
            // 0.4�Œǂ��ĂȂ���Ԃɂ���
            else if (attention <= 0.4f)
            {
                Vector3 dest = GetDestinationRandomly();
                agent.destination = dest;
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);
                state = 0;
            }
            // �ړI�n�t�߂Ŏ��̖ړI�n
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 dest = GetDestinationRandomly();
                agent.destination = dest;
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);
                state = 0;
            }
        }
            attention += increaseAttention;
        attention -= 0.007f; // �Q�[�W��������
        attention = Mathf.Clamp(attention, 0f, 1f); // 0~1�ɌŒ�
       
        gauge.fillAmount = attention; // �Q�[�W�ɔ��f
    }

    // �����_���ŖړI�n��Ԃ�
    Vector3 GetDestinationRandomly()
    {
        return points.GetChild(Random.Range(0, points.childCount)).transform.position;
    }
}
