using System;
using UnityEngine;

// シーンを実行しなくてもカメラワークが反映されるよう、ExecuteInEditModeを付与
[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    /// <summary> カメラのパラメータ </summary>
    [Serializable]
    public class Parameter
    {
        public Vector3 position;
        public Vector3 angles = new Vector3(10f, 0f, 0f);
        public float distance = 7f;
        public float fieldOfView = 45f;
        public Vector3 offsetPosition = new Vector3(0f, 1f, 0f);
        public Vector3 offsetAngles;
    }

    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Transform _child;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Parameter _parameter;

    // 被写体などの移動更新が済んだ後にカメラを更新したいので、LateUpdateを使う
    private void LateUpdate()
    {
        if (_parent == null || _child == null || _camera == null)
        {
            return;
        }

        // パラメータを各種オブジェクトに反映
        _parent.position = _parameter.position;
        _parent.eulerAngles = _parameter.angles;

        var childPos = _child.localPosition;
        childPos.z = -_parameter.distance;
        _child.localPosition = childPos;

        _camera.fieldOfView = _parameter.fieldOfView;
        _camera.transform.localPosition = _parameter.offsetPosition;
        _camera.transform.localEulerAngles = _parameter.offsetAngles;
    }
}