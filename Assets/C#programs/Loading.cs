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

		 var async = SceneManager.LoadSceneAsync("MainScene");

		 async.allowSceneActivation = false;


		//�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
		float p = 0f;
		for(int i = 0; i < 200; i++)
		{
			p = (float)(i / 70) * 100f;
			slider.value = p;
			yield return null;
		} 

		 async.allowSceneActivation = true;
		
	}



}
