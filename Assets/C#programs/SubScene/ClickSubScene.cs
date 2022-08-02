using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickSubScene : MonoBehaviour
{
	[SerializeField]
	private GameObject loadUI;
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider slider;
	private AsyncOperation async;
	public static bool pushed;

    private void Start()
    {
		pushed = false;
    }
    public void OnClickSubSceneButton()
    {
        if (!pushed)
        {
			StartCoroutine(Load());
		}
	}
	IEnumerator Load()
    {
		pushed = true;
		async = SceneManager.LoadSceneAsync("SubScene");

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
		pushed = false;
	}
}
