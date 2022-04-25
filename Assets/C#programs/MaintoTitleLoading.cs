using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Button))]

public class MaintoTitleLoading : MonoBehaviour
{
	//�@�񓯊�����Ŏg�p����AsyncOperation
	//private AsyncOperation async;
	//�@�V�[�����[�h���ɕ\������UI���
	[SerializeField]
	private GameObject loadUI;
	//�@�ǂݍ��ݗ���\������X���C�_�[
	[SerializeField]
	private Slider slider;


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

		var async = SceneManager.LoadSceneAsync("TitleScene");

		async.allowSceneActivation = false;


		//�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
		float p = 0f;
		for (int i = 0; i < 500; i++)
		{
			p = (float)(i / 70) * 1f;
			slider.value = p / 5;
			yield return null;
		}

		async.allowSceneActivation = true;

	}



}