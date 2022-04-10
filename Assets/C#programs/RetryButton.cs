using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]

public class RetryButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var RetryButton = GetComponent<Button>();
        RetryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainScene");
        });

    }

}
