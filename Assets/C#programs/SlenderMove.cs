using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class SlenderMove : MonoBehaviour
{
    [SerializeField] FirstPersonControllerCustom sc_fps;
    [SerializeField] Transform points;
    [SerializeField] Image gauge; //プログレスバー
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

        //レイが当たった時
        headToPlayerDir = Vector3.Normalize(headToPlayerDir);
        float dot = Vector3.Dot(headToPlayerDir, head.forward);

        if (dot > 0)
        {
            increaseAttention += 0.05f * dot;
            text.text = "\nA:見えている";

            //プレイヤーが走っていないとき
            if (sc_fps.GetIsWalking())
            {
                text.text = "\nP:走っていない";
                attention -= 0.008f; // 注意のレベルを下げる            
            }
            //プレイヤーが走っているとき
            else
            {
                text.text = "\nP:走っている";
                increaseAttention += 0.03f; // 注意のレベルを上げる
                increaseAttention *= Mathf.Clamp(3f / distanceFromPlayer, 0f, 1f);
            }
        }
        else
        {
            text.text = "\nA:見えていない";
        }

        // 追っていない状態
        if (state == 0)
        {
            if (attention <= 0f)
            {
                attention = 0f;
            }

            text.text += "\nA:巡回している";

            // 1以上のとき追っている状態にする
            if (attention >= 1f)
            {
                ///attention = 1f;
                state = 1;
                agent.destination = player.position; // プレイヤーの場所に行く
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);
            }
            // 目的地付近で次の目的地を設定
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 dest = GetDestinationRandomly();
                agent.destination = dest;
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);

            }

        }
        // 追っている状態
        else if (state == 1)
        {

            text.text += "\nA:追っている";
            agent.destination = player.position; // プレイヤーの場所に行く
                                                 // Destroy(sphere);
                                                 //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);

            // 1以上のとき1
            if (attention >= 1f)
            {
                ///attention = 1f;
            }
            // 見失っている状態にする
            else
            {
                state = 2;
            }
        }

        // 見失っている状態
        else if (state == 2)
        {
            text.text += "\nA:見失っている";

            // 1以上のとき追っている状態にする
            if (attention >= 1f)
            {
                ///attention = 1f;
                state = 1;
                agent.destination = player.position; // プレイヤーの場所に行く
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, player.position, spherePrefab.transform.rotation);
            }
            // 0.4で追ってない状態にする
            else if (attention <= 0.4f)
            {
                Vector3 dest = GetDestinationRandomly();
                agent.destination = dest;
                //Destroy(sphere);
                //sphere = Instantiate(spherePrefab, dest, spherePrefab.transform.rotation);
                state = 0;
            }
            // 目的地付近で次の目的地
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
        attention -= 0.007f; // ゲージを下げる
        attention = Mathf.Clamp(attention, 0f, 1f); // 0~1に固定
       
        gauge.fillAmount = attention; // ゲージに反映
    }

    // ランダムで目的地を返す
    Vector3 GetDestinationRandomly()
    {
        return points.GetChild(Random.Range(0, points.childCount)).transform.position;
    }
}
