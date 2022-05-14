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
    [SerializeField] public AudioSource SlenderAudioSource;
    [SerializeField] public AudioSource FPSAudioSource;
    [SerializeField] public AudioSource SEAudioSource;
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
        SlenderAudioSource.Play();
        SEAudioSource.Play();
        //雷の音　(Clip)
        FPSAudioSource.volume = rainvolume;
        FPSAudioSource.Play();
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
            }
        }    
    }

    public void OnPause()
    {   
        OnPanel.SetActive(true);        // PanelMenuをtrueにする
        OnUnPanel.SetActive(false);     // PanelEscをfalseにする
        SlenderAudioSource.Pause();
        FPSAudioSource.Pause();
        SEAudioSource.Pause();
        Time.timeScale = 0;
        gamePause = true;
        FirstPersonControllerCustom fpc = player.GetComponent<FirstPersonControllerCustom>();
        fpc.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;     // 標準モード
        Cursor.visible = true;    // カーソル表示
    }

    public void OnUnPause()
    {   
        
        OnPanel.SetActive(false);       // PanelMenuをfalseにする
        OnUnPanel.SetActive(true);      // PanelEscをtrueにする
        SlenderAudioSource.Play();
        SEAudioSource.Play();
        //雷の音　(Clip)
        FPSAudioSource.Play();
        Time.timeScale = 1;
        gamePause = false;
        FirstPersonControllerCustom fpc = player.GetComponent<FirstPersonControllerCustom>();
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
        TitleVideo.mainTotitle = true;
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