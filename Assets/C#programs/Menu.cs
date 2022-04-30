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
    public GameObject RetryButton;
    [SerializeField] private GameObject RetryLoadUI;
    [SerializeField] private Slider RetrySlider;
    public GameObject ResumeButton;
    [SerializeField] private GameObject ResumeLoadUI;
    [SerializeField] private Slider ResumeSlider;
    public AudioSource audioSource1;
    public AudioSource audioSouse2;
    [SerializeField] float rainvolume = 0.2f;
    //雷の音　(Clip)
    //private float rainvolume = 0.2f;
    private AsyncOperation async;
    private bool gamePause;
    private bool isLoading = false;
    //private bool esc1 = false;
    
    void Start()
    {
        OnUnPause();
        buckbutton();
        retrybutton();
        resumebutton();
        audioSource1.Play();
        //雷の音　(Clip)
        audioSouse2.volume = rainvolume;
        audioSouse2.Play();
        //Retryロード処理
        RetryLoadUI.SetActive(false);
        //Resumeロード処理
        ResumeLoadUI.SetActive(false);
    }  

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause = !gamePause;

            if ( gamePause == true )
            {
                OnPause();
            }
            else if( isLoading == false)
            {
                OnUnPause();
                Debug.Log("now pause");
            }
        }    
    }

    public void OnPause()
    {   
        OnPanel.SetActive(true);        // PanelMenuをtrueにする
        OnUnPanel.SetActive(false);     // PanelEscをfalseにする
        audioSource1.Pause();
        audioSouse2.Pause();
        Time.timeScale = 0;
        gamePause = true;
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
        audioSouse2.Play();
        Time.timeScale = 1;
        gamePause = false;
        FirstPersonController fpc = player.GetComponent<FirstPersonController>();
        fpc.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
        Cursor.visible = false;     // カーソル非表示
    }

    public void buckbutton(){

        var Clicked = BuckButton.GetComponent<Button>();
        Clicked.onClick.AddListener(buck);
    }

    //BuckBUtton
    public void buck(){

        OnUnPause();
    }

    //RetryButton
    public void retrybutton()
    { 
        var Clicked = RetryButton.GetComponent<Button>();
        Clicked.onClick.AddListener(retry);
    }

    public void retry()
    { 
        RetryLoadUI.SetActive(true);
        StartCoroutine("RetryLoad");
    }
    IEnumerator RetryLoad()  
    {
        isLoading = true;
       // yield return new WaitForSeconds(1);
         async = SceneManager.LoadSceneAsync("MainScene");

         async.allowSceneActivation = false;

         //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
         while ( async.progress < 0.9f )
         {
            RetrySlider.value = async.progress;
            yield return null;
         }
        RetrySlider.value = 1.0f;
        async.allowSceneActivation = true;
        yield return async;
        isLoading = false;

    }

    //ResumeButton
    public void resumebutton()
    {
        var Clicked = ResumeButton.GetComponent<Button>();
        Clicked.onClick.AddListener(resume);

    }
    public void resume()
    {
        ResumeLoadUI.SetActive(true);
        StartCoroutine("ResumeLoad");
    }

    IEnumerator ResumeLoad()
    {
        isLoading = true;
        async = SceneManager.LoadSceneAsync("TitleScene");

        async.allowSceneActivation = false;


        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (async.progress < 0.9f)
        {
            RetrySlider.value = async.progress;
            yield return null;
        }
        RetrySlider.value = 1.0f;
        async.allowSceneActivation = true;
        yield return async;

        async.allowSceneActivation = true;
        isLoading = false;

    }
}