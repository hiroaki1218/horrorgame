using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{

	#region Singleton
	private GameObject fpc;
	private FirstPersonControllerCustom _fpc;

	private static FadeManager instance;

    public static FadeManager Instance {
		get {
			if (instance == null) {
				instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

				if (instance == null) {
					Debug.LogError(typeof(FadeManager) + "is nothing");
				}
			}

			return instance;
		}
	}

	#endregion Singleton

	/// <summary>
	/// デバッグモード .
	/// </summary>
	public bool DebugMode = true;
	/// <summary>フェード中の透明度</summary>
	private float fadeAlpha = 0;
	/// <summary>フェード中かどうか</summary>
	private bool isFading = false;
	/// <summary>フェード色</summary>
	public Color fadeColor = Color.black;
	private AsyncOperation async;
	[SerializeField] private Slider _slider;
	//[SerializeField] private GameObject Ui;
	bool Loading;
	public bool Subscene;
	public bool sub;
	[SerializeField] private GameObject FadeUI;


	public void Awake()
	{
		if (this != Instance) {
			Destroy(this.gameObject);
			return;
		}

		fpc = GameObject.Find("FPSController");
		_fpc = fpc.GetComponent<FirstPersonControllerCustom>();

		DontDestroyOnLoad(this.gameObject);
	}
	private void Start()
	{
		Loading = false;
        if (sub)
        {
			FadeUI.SetActive(false);
        }
	}
	public void OnGUI()
	{

		// Fade .
		if (this.isFading) {
			//色と透明度を更新して白テクスチャを描画 .
			this.fadeColor.a = this.fadeAlpha;
			GUI.color = this.fadeColor;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
		}

		if (this.DebugMode) {
			if (!this.isFading) {
				//Scene一覧を作成 .
				//(UnityEditor名前空間を使わないと自動取得できなかったので決めうちで作成) .
				List<string> scenes = new List<string>();
				scenes.Add("SampleScene");
				//scenes.Add ("SomeScene1");
				//scenes.Add ("SomeScene2");


				//Sceneが一つもない .
				if (scenes.Count == 0) {
					GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
					GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
					return;
				}


				GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
				GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + SceneManager.GetActiveScene().name);

				int i = 0;
				foreach (string sceneName in scenes) {
					if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level")) {
						LoadScene(sceneName, 1.0f);
					}
					GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
					i++;
				}
			}
		}



	}

	/// <summary>
	/// 画面遷移 .
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	public void LoadScene(string scene, float interval)
	{
		StartCoroutine(TransScene(scene, interval));
	}

	/// <summary>
	/// シーン遷移用コルーチン .
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	private IEnumerator TransScene(string scene, float interval)
	{
        if (sub)
        {
			_fpc.enabled = false;
		}

		
		//だんだん暗く .
		this.isFading = true;
		float time = 0;
		while (time <= interval)
		{
			this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
		if (sub)
		{
			sub = false;
			FadeUI.SetActive(true);
		}
		//シーン切替 .
		if (Subscene)
		{
			Debug.Log("Loafing");
			Subscene = false;

			async = SceneManager.LoadSceneAsync(scene);
			async.allowSceneActivation = false;

			//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
			while (async.progress < 0.9f)
			{
				_slider.value = async.progress;
				yield return null;
			}
			_slider.value = 1.0f;
			async.allowSceneActivation = true;
			yield return async;

		}

		//だんだん明るく .
		time = 0;
     	while (time <= interval)
		{
			this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}

		this.isFading = false;

	} 
}
