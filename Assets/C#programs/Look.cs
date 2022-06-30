using System;
using UnityEngine;
using System.Collections;

public class Look : MonoBehaviour
{
    [SerializeField] private GameObject FPSc;
    float wantRotation;
    public float turnTime = 1.0f;

    void Start()
    {
        wantRotation = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        // 左右のキー入力でキャラクターを90度旋回する
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(wantRotation);
            wantRotation -= 90f;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            wantRotation += 90f;
        }

        Quaternion want = Quaternion.AngleAxis(wantRotation, new Vector3(0, 1, 0));

        FPSc.transform.rotation = Quaternion.Lerp(FPSc.transform.rotation, want, Time.deltaTime / turnTime);
    }
}
