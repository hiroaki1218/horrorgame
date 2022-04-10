using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]

public class ResumeButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var ResumeButton = GetComponent<Button>();
        ResumeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TitleScene");
        });

    }

}
