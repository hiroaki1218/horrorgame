using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;
using System;

public class CharacterAni : MonoBehaviour
{
    [SerializeField] private FirstPersonAnime _fpcanim;
    [SerializeField] private FirstPersonControllerCustom FPC;
    [SerializeField] private CharacterController fps;
    [SerializeField] private Animator _anim;
    [SerializeField] private Camera startCamera;
    [SerializeField] private GameObject CrosshairUI;

    //Audio
    [SerializeField] private GameObject RayCube;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private float GrassVolume = 1.0f;
    [SerializeField] private float SandVolume = 1.0f;
    [SerializeField] private float StoneVolume = 1.0f;
    [SerializeField] private float WoodVolume = 1.0f;
    public bool randomizePitch;
    [SerializeField] float pitchRange = 0.1f;

    public static bool isFirstAnim;

    private bool firstWalk;
    private bool finishfirstStop;
    private bool isfinishWhileStop;
    private bool once;
    private bool rotating1;
    private bool finishsecondStop;
    private bool secondWalk;
    private bool isfinishWhileSecondFinish;

    private void Start()
    {
        _anim.Play("BasicMotions@Walk01 - Forwards");
        StartCoroutine(FuncCoroutine());
        _fpcanim.enabled = false;
        startCamera.enabled = true;
        FPC.enabled = false;
        fps.enabled = false;
        CrosshairUI.SetActive(false);
        isFirstAnim = true;//終わったらfalse;

        firstWalk = true;
        finishfirstStop = false;
        isfinishWhileStop = false;
        once = true;
        rotating1 = false;
        finishsecondStop = false;
        secondWalk = false;
        isfinishWhileSecondFinish = false;
    }

    private void Update()
    {
        StartCoroutine(FPSAni());
    }

    IEnumerator FPSAni()
    {
        if (!AniTrigger.enter && firstWalk)
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
            firstWalk = false;
            if (!finishfirstStop)
            {
                _anim.Play("BasicMotions@Idle01");
                isfinishWhileStop = true;
            }
            
            yield return new WaitForSeconds(6.3f);

            finishfirstStop = true;
            isfinishWhileStop = false;

            if (!FPSTrigger.close)
            {
                _anim.Play("BasicMotions@Walk01 - Forwards");
                for (int walk = 0; walk < 20; walk++)
                {
                    _anim.gameObject.transform.Translate(0, 0, 0.0015f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else
            {
                if (!rotating1)
                {
                    _anim.Play("BasicMotions@Idle01");
                }

                if (!rotating1 && once)                              
                {
                    once = false;
                    for (int turn = 0; turn < 90; turn++)
                    {
                        _anim.gameObject.transform.Rotate(0, -2, 0);
                       yield return new WaitForSeconds(0.0000001f);
                    }
                    rotating1 = true;
                    once = true;
                }
            }
        }
        if (!AniTrigger.enter && rotating1)
        {
            if (!isfinishWhileSecondFinish)
            {
                _anim.Play("BasicMotions@Walk01 - Forwards");
            }
            
            yield return new WaitForSeconds(0.8f);
            secondWalk = true;
            for (int walk = 0; walk < 20; walk++)
            {
                _anim.gameObject.transform.Translate(0, 0, 0.0027f);
                yield return new WaitForSeconds(0.001f);
            }
        }
        else if(secondWalk)
        {
            yield return new WaitForSeconds(1);
            if (!isfinishWhileSecondFinish)
            {
                _anim.Play("BasicMotions@Idle01");
            }
            isfinishWhileSecondFinish = true;

            yield return new WaitForSeconds(2);
            if (once)
            {
                CrosshairUI.SetActive(true);
            }
            _anim.gameObject.transform.rotation = FPC.gameObject.transform.rotation;
            _anim.gameObject.transform.localPosition = new Vector3(-0.004f, -1.143f, -0.1f);
            isFirstAnim = false;
            fps.enabled = true;
            startCamera.enabled = false;
            FPC.enabled = true;
            _fpcanim.enabled = true;
        }
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
            
            if(!isfinishWhileStop && once && !isfinishWhileSecondFinish)
            {
                Aoudio();
            }
            if (secondWalk)
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.4f);
            }
            
        }
    }
}
