using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Button))]

public class Loading : MonoBehaviour
{
	//�@�񓯊�����Ŏg�p����AsyncOperation
	//private AsyncOperation async;
	//�@�V�[�����[�h���ɕ\������UI���
	[SerializeField]
	private GameObject loadUI;
	//�@�ǂݍ��ݗ���\������X���C�_�[
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


		//�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
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
