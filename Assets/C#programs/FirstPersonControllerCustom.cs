using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonControllerCustom : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;

        [SerializeField] private bool m_IsRunning;

        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        [SerializeField] bool randomizePitch = true;
        [SerializeField] float pitchRange = 0.1f;
        

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;
        public GameObject RayCube;
        //public GameObject WaterTrigger;
        [HideInInspector] public StaminaController _staminaController;

        // Use this for initialization
        private void Start()
        {
            _staminaController = GetComponent<StaminaController>();
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);
            Application.targetFrameRate = 60;
            //WaterTrigger.GetComponent<EventTrigger>();
        }

        public void SetRunSpeed(float speed)
        {
            m_RunSpeed = speed;
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        //��������������������
        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            //int n = Random.Range(1, m_FootstepSounds.Length);
            // m_AudioSource.clip = m_FootstepSounds[0];
            // m_AudioSource.PlayOneShot(m_AudioSource.clip);
                   Aoudio();
            // move picked sound to index 0 so it's not picked next time
            //m_FootstepSounds[0] = m_FootstepSounds[0];
            //m_FootstepSounds[0] = m_AudioSource.clip;
        }

        //public void OnTriggerStay(Collider other)
        //{
            //if(other.gameObject.tag == "OnWater")
            //{
                //Debug.Log("water");
            //}
        //}


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        //�����_���s�b�`
        

        float[] slatmap = new float[0];
        RaycastHit hitInfo;
        void Aoudio()
        {
            //ray����
            //Ray ray = new Ray(transform.position,transform.forward);
            //Vector3 rayPosition = RayCube.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
            if (Physics.Raycast(RayCube.transform.position,Vector3.down, out hitInfo,1f))
            {
                string hitColliderTag = hitInfo.collider.tag;
                    if(hitColliderTag == "OnWater")
                    {
                       if (randomizePitch) {
                    
                        m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                        int w = Random.Range(8, 10);
                        m_AudioSource.clip = m_FootstepSounds[w];
                        m_AudioSource.PlayOneShot(m_AudioSource.clip);
                        }
                       else if(randomizePitch == false){ 
                       //Debug.Log("OnWater");
                       int p = Random.Range(8, 10);
                       m_AudioSource.clip = m_FootstepSounds[p];
                       m_AudioSource.volume = 1.0f;
                       m_AudioSource.PlayOneShot(m_AudioSource.clip);
                       }
                    }

               
                
                 else if (hitColliderTag == "Terrain")
                {
                    // �e���C���f�[�^
                    TerrainData terrainData = hitInfo.collider.gameObject.GetComponent<Terrain>().terrainData;

                    // �A���t�@�}�b�v 
                    float[,,] alphaMaps = terrainData.GetAlphamaps(Mathf.FloorToInt(hitInfo.textureCoord.x * terrainData.alphamapWidth), Mathf.FloorToInt(hitInfo.textureCoord.y * terrainData.alphamapHeight), 1, 1);


                    int layerCount = terrainData.alphamapLayers; // �e���C�����C���[�̐�

                    // �O�Ԗڂ̔z������o��
                    if (slatmap.Length == 0) slatmap = new float[layerCount];
                    for (int i = 0; i < layerCount; i++)
                    {
                        slatmap[i] = alphaMaps[0, 0, i];
                    }

                    // �ő�l�̃C���f�b�N�X
                    int maxIndex = Array.IndexOf(slatmap, Mathf.Max(slatmap));

                    
                    // ������ς���
                    switch (maxIndex)
                    {
                        case 0:
                            //��
                            if (randomizePitch)
                            {
                                m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                                int q = Random.Range(6, 8);
                                m_AudioSource.clip = m_FootstepSounds[q];
                                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            }
                            else if(randomizePitch == false) { 
                            int j = Random.Range(6, 8);
                            m_AudioSource.clip = m_FootstepSounds[j];
                            m_AudioSource.volume = 1.0f;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            //Debug.Log("0000000");
                            }
                            break;
                        case 1:
                            //��
                            if (randomizePitch)
                            {
                                m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                                int h = Random.Range(2, 4);
                                m_AudioSource.clip = m_FootstepSounds[h];
                                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            }
                            else if (randomizePitch == false) { 
                            int n = Random.Range(2,4);
                            m_AudioSource.clip = m_FootstepSounds[n];
                            m_AudioSource.volume = 1.0f;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                           // Debug.Log("1111111");
                            }
                            break;
                        case 3:
                            //�Ȃ�
                            if (randomizePitch)
                            {
                                m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                                int s = Random.Range(0, 2);
                                m_AudioSource.clip = m_FootstepSounds[s];
                                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            }
                            else if (randomizePitch == false) { 
                            int y = Random.Range(0,2);
                            m_AudioSource.clip = m_FootstepSounds[y];
                            m_AudioSource.volume = 1.0f;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            //Debug.Log("3333333");
                            }
                            break;
                        case 2:
                            //�R���N��
                            if (randomizePitch)
                            {
                                m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                                int c = Random.Range(4, 6);
                                m_AudioSource.clip = m_FootstepSounds[c];
                                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            }
                            else if (randomizePitch == false) { 
                            int u = Random.Range(4,6);
                            m_AudioSource.clip = m_FootstepSounds[u];
                            m_AudioSource.volume = 1.0f;
                            m_AudioSource.PlayOneShot(m_AudioSource.clip);
                           // Debug.Log("222222222");
                            }
                            break;
                        case 4:
                            //Debug.Log("eeeee");

                            break;
                        //default:
                            //��
                            //m_AudioSource.clip = m_FootstepSounds[0];
                            //m_AudioSource.volume = 1.0f;
                            //m_AudioSource.PlayOneShot(m_AudioSource.clip);
                            //Debug.Log("ddddddd");
                            //break;
                    }
                    
                }
                else
                {
                    //��
                    if (randomizePitch)
                    {
                        m_AudioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                        int a = Random.Range(0, 2);
                        m_AudioSource.volume = 0.5f;
                        m_AudioSource.clip = m_FootstepSounds[a];
                        m_AudioSource.PlayOneShot(m_AudioSource.clip);
                    }
                    else if (randomizePitch == false) { 
                    int n = Random.Range(0,2);
                    m_AudioSource.clip = m_FootstepSounds[n];
                    m_AudioSource.volume = 0.5f;
                    m_AudioSource.PlayOneShot(m_AudioSource.clip);
                    //Debug.Log("uuuuuu");
                    }
                }
            }
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
        }
         

private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running

            //m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
            if (_staminaController.hasRegenerated)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    m_IsRunning = true;
                    m_IsWalking = false;
                }
                else
                {
                    m_IsRunning = false;
                    m_IsWalking = true;
                }
                
            }
            else
            {
                m_IsRunning = false;
                m_IsWalking = true;
            }


#endif
            if (m_IsWalking)
            {
                _staminaController.weAreSprinting = false;
            }

            if (m_IsRunning && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                if(_staminaController.playerStamina > 0)
                {
                    _staminaController.weAreSprinting = true;
                    _staminaController.Sprinting();
                }
                else
                {
                    //m_IsWalking = true;
                    m_IsRunning = false;
                }
                        
            }

            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }
 
    }


   
}
