using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Button))]

public class Loading : MonoBehaviour
{
	//　非同期動作で使用するAsyncOperation
	//private AsyncOperation async;
	//　シーンロード中に表示するUI画面
	[SerializeField]
	private GameObject loadUI;
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider slider;
	private AsyncOperation async;
	

    public void Start()
    {
		loadUI.SetActive(false);
		var button = GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
			loadUI.SetActive(true);
			StartCoroutine(LoadData());
		});


	}


	IEnumerator LoadData()
	{
		async = SceneManager.LoadSceneAsync("MainScene");

		async.allowSceneActivation = false;


		//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while (async.progress < 0.9f)
		{
			slider.value = async.progress;
			yield return null;
		}
		slider.value = 1.0f;
		async.allowSceneActivation = true;
		yield return async;

		async.allowSceneActivation = true;

	}



}
