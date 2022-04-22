using UnityEngine;
using System.Collections;
// 追加
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Menu : MonoBehaviour {

    public GameObject player;
    public GameObject OnPanel, OnUnPanel;
    public GameObject BuckButton;
    public AudioSource audioSource1;
    public AudioSource audioSouse2;
    [SerializeField] private AudioClip b1;

    //雷の音　(Clip)
    private float rainvolume = 0.4f;

    private bool pauseGame = false;
    
    void Start()
    {
        OnUnPause();
        button();
        audioSource1.Play();
        //雷の音　(Clip)
        audioSouse2.PlayOneShot(b1, rainvolume);
    }  

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }    
    }

    public void OnPause()
    {   
        OnPanel.SetActive(true);        // PanelMenuをtrueにする
        OnUnPanel.SetActive(false);     // PanelEscをfalseにする
        audioSource1.Pause();
        audioSouse2.Stop();
        Time.timeScale = 0;
        pauseGame = true;
        FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        fpc.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;     // 標準モード
        Cursor.visible = true;    // カーソル表示
    }

    public void OnUnPause()
    {   
        
        OnPanel.SetActive(false);       // PanelMenuをfalseにする
        OnUnPanel.SetActive(true);      // PanelEscをtrueにする
        audioSource1.Play();
        //雷の音　(Clip)
        audioSouse2.PlayOneShot(b1, rainvolume);
        Time.timeScale = 1;
        pauseGame = false;
        FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        fpc.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
        Cursor.visible = false;     // カーソル非表示
    }

    public void button(){

    var Clicked = BuckButton.GetComponent<Button>();
        Clicked.onClick.AddListener(buck);
    }

    public void buck(){
        OnUnPause();
    }

}