using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Video;

public class SubSceneStartMove : MonoBehaviour
{
    public static bool SubSceneFirstMoving;
    [SerializeField] private FirstPersonAnime _fpcanim;
    [SerializeField] private FirstPersonControllerCustom FPC;
    [SerializeField] private CharacterController fps;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _chairanim;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject CrosshairUI;

    //Video
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private GameObject _videoimage;

    [SerializeField] private TutorialMovement _tmt;
    [SerializeField] private GameObject ConditionCanvas;

    //Audio
    public float timeOut;
    [SerializeField] private GameObject RayCube;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private float GrassVolume = 1.0f;
    [SerializeField] private float SandVolume = 1.0f;
    [SerializeField] private float StoneVolume = 1.0f;
    [SerializeField] private float WoodVolume = 1.0f;
    public bool randomizePitch;
    [SerializeField] float pitchRange = 0.1f;

    private bool rotating;
    private bool rotating2;
    private bool rotating3;
    private bool stopping;
    private bool once;
    private bool Once;
    private bool sitFinish;
    private bool secondmove;
    private void Awake()
    {
        _fpcanim.enabled = false;
        startCamera.enabled = true;
        FPC.enabled = false;
        fps.enabled = false;
        sitFinish = false;
    }
    private void Start()
    {
        SubSceneFirstMoving = true;
        StartCoroutine(FuncCoroutine());
        rotating = false;
        rotating2 = false;
        rotating3 = false;
        once = true;
        Once = true;
        stopping = false;
        CrosshairUI.SetActive(false);
        _tmt.enabled = false;
        ConditionCanvas.SetActive(false);
        _videoimage.SetActive(false);
        secondmove = false;
    }
    private void Update()
    {
        StartCoroutine(FPSAnime());
    }
    //人の動き
    IEnumerator FPSAnime()
    {
        if (!secondmove)
        {
            if (!SubSceneFirstTrigger.SubFirstTrigger)//まっすぐ歩く
            {
                _anim.Play("BasicMotions@Walk01 - Forwards");
                for (int walk = 0; walk < 20; walk++)
                {
                    _anim.gameObject.transform.Translate(0, 0, 0.001f);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                if (!rotating && once)                              ///# 左に回転する
                {
                    once = false;
                    for (int turn = 0; turn < 90; turn++)
                    {
                        _anim.gameObject.transform.Rotate(0, -1, 0);
                        yield return new WaitForSeconds(0.01f);
                    }
                    rotating = true;
                    once = true;
                }
                else if (!SubSceneSecondTrigger.SubSecondTrigger)
                {
                    _anim.Play("BasicMotions@Walk01 - Forwards");
                    for (int walk = 0; walk < 20; walk++)
                    {
                        _anim.gameObject.transform.Translate(0, 0, 0.001f);
                        yield return new WaitForSeconds(0.01f);
                    }
                }
                ///#end
                if (SubSceneSecondTrigger.SubSecondTrigger && !secondmove)              //少し前に歩く
                {
                    stopping = true;
                    if (!rotating2)                                     ///#一度止まる
                    {
                        _anim.Play("BasicMotions@Idle01");
                        yield return new WaitForSeconds(0.1f);
                        rotating2 = true;
                    }                                                   ///#end
                    else if(!secondmove)
                    {
                        _anim.Play("FPSSitDown");                       //椅子を見て持ってくる
                        yield return new WaitForSeconds(0.6f);
                        _chairanim.Play("ChairMove");
                        if (!rotating3 && once)                              ///# 左に回転する
                        {
                            once = false;
                            for (int turn = 0; turn < 92; turn++)
                            {
                                _anim.gameObject.transform.Rotate(0, -1, 0);
                                yield return new WaitForSeconds(0.01f);
                            }
                            rotating3 = true;
                            once = true;
                        }
                        yield return new WaitForSeconds(1.8f);
                        sitFinish = true;
                    }
                    if (once && sitFinish && !secondmove)                              //しゃがむ（トランスフォームy）
                    {
                        once = false;
                        for (int i = 0; i < 12; i++)
                        {
                            _anim.gameObject.transform.Translate(0, -0.02f, 0);
                            yield return new WaitForSeconds(0.01f);
                        }
                        yield return new WaitForSeconds(1);
                        _videoimage.SetActive(true);
                        _video.loopPointReached += LoopPointReached;
                        _video.Play();
                    }
                }
            }
        }
        else
        {
            _chairanim.Play("ChairMoveBack");
            if (Once)
            {
                _anim.Play("FPSStandUp");
                Once = false;
                yield return new WaitForSeconds(0.3f);
                if (!once)                              //しゃがむ（トランスフォームy）
                {
                    once = true;
                    for (int i = 0; i < 12; i++)
                    {
                        _anim.gameObject.transform.Translate(0, 0.02f, 0);
                        yield return new WaitForSeconds(0.01f);
                    }
                    for (int turn = 0; turn < 92; turn++)
                    {
                        _anim.gameObject.transform.Rotate(0, -1, 0);
                        yield return new WaitForSeconds(0.004f);
                    }
                    _tmt.enabled = true;
                    fps.enabled = true;
                    startCamera.enabled = false;
                    FPC.enabled = true;
                    _fpcanim.enabled = true;
                    ConditionCanvas.SetActive(true);
                    CrosshairUI.SetActive(true);
                    this.enabled = false;
                    SubSceneFirstMoving = false;
                }
            }
        }
    }
    // 動画再生完了時の処理
    public void LoopPointReached(VideoPlayer vp)
    {
        secondmove = true;
    }
    float[] slatmap = new float[0];
    RaycastHit hitInfo;
    void Aoudio()
    {
        if (Physics.Raycast(RayCube.transform.position, Vector3.down, out hitInfo, 1f))
        {
            string hitColliderTag = hitInfo.collider.tag;

            if (hitColliderTag == "Terrain")
            {
                // テレインデータ
                TerrainData terrainData = hitInfo.collider.gameObject.GetComponent<Terrain>().terrainData;

                // アルファマップ 
                float[,,] alphaMaps = terrainData.GetAlphamaps(Mathf.FloorToInt(hitInfo.textureCoord.x * terrainData.alphamapWidth), Mathf.FloorToInt(hitInfo.textureCoord.y * terrainData.alphamapHeight), 1, 1);


                int layerCount = terrainData.alphamapLayers; // テレインレイヤーの数

                // 三番目の配列を取り出す
                if (slatmap.Length == 0) slatmap = new float[layerCount];
                for (int i = 0; i < layerCount; i++)
                {
                    slatmap[i] = alphaMaps[0, 0, i];
                }

                // 最大値のインデックス
                int maxIndex = Array.IndexOf(slatmap, Mathf.Max(slatmap));


                // 足音を変える
                switch (maxIndex)
                {
                    case 0:
                        //草
                        if (randomizePitch)
                        {
                            m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                            int q = Random.Range(6, 8);
                            m_AudioSource.clip = m_FootstepSounds[q];
                            m_AudioSource.volume = GrassVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        }
                        else if (randomizePitch == false)
                        {
                            int j = Random.Range(6, 8);
                            m_AudioSource.clip = m_FootstepSounds[j];
                            m_AudioSource.volume = GrassVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            //Debug.Log("0000000");
                        }
                        break;
                    case 1:
                        //砂
                        if (randomizePitch)
                        {
                            m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                            int h = Random.Range(2, 4);
                            m_AudioSource.clip = m_FootstepSounds[h];
                            m_AudioSource.volume = SandVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        }
                        else if (randomizePitch == false)
                        {
                            int n = Random.Range(2, 4);
                            m_AudioSource.clip = m_FootstepSounds[n];
                            m_AudioSource.volume = SandVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            // Debug.Log("1111111");
                        }
                        break;
                    case 3:
                        //ない
                        if (randomizePitch)
                        {
                            m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                            int s = Random.Range(0, 2);
                            m_AudioSource.clip = m_FootstepSounds[s];
                            m_AudioSource.volume = WoodVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        }
                        else if (randomizePitch == false)
                        {
                            int y = Random.Range(0, 2);
                            m_AudioSource.clip = m_FootstepSounds[y];
                            m_AudioSource.volume = WoodVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            //Debug.Log("3333333");
                        }
                        break;
                    case 2:
                        //コンクリ
                        if (randomizePitch)
                        {
                            m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                            int c = Random.Range(4, 6);
                            m_AudioSource.clip = m_FootstepSounds[c];
                            m_AudioSource.volume = StoneVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        }
                        else if (randomizePitch == false)
                        {
                            int u = Random.Range(4, 6);
                            m_AudioSource.clip = m_FootstepSounds[u];
                            m_AudioSource.volume = StoneVolume;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            // Debug.Log("222222222");
                        }
                        break;
                    case 4:
                        //Debug.Log("eeeee");

                        break;
                        //default:
                        //床
                        //m_AudioSource.clip = m_FootstepSounds[0];
                        //m_AudioSource.volume = 1.0f;
                        //m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        //Debug.Log("ddddddd");
                        //break;
                }

            } //カーペット
            else if (hitColliderTag == "Carpet")
            {
                if (randomizePitch)
                {
                    m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                    int l = Random.Range(6, 8);
                    m_AudioSource.clip = m_FootstepSounds[l];
                    m_AudioSource.volume = GrassVolume;
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                }
                else if (randomizePitch == false)
                {
                    int j = Random.Range(6, 8);
                    m_AudioSource.clip = m_FootstepSounds[j];
                    m_AudioSource.volume = GrassVolume;
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                }
            }
            else
            {
                //床
                if (randomizePitch)
                {
                    m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                    int a = Random.Range(0, 2);
                    m_AudioSource.volume = 0.5f;
                    m_AudioSource.clip = m_FootstepSounds[a];
                    m_AudioSource.volume = WoodVolume;
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                }
                else if (randomizePitch == false)
                {
                    int n = Random.Range(0, 2);
                    m_AudioSource.clip = m_FootstepSounds[n];
                    m_AudioSource.volume = WoodVolume;
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                    //Debug.Log("uuuuuu");
                }
            }
        }
        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
    }
    IEnumerator FuncCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (!stopping)
            {
                Aoudio();
            }

            yield return new WaitForSeconds(timeOut);
        }
    }
}
