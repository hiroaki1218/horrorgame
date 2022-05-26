using System;
using UnityEngine;

// �V�[�������s���Ȃ��Ă��J�������[�N�����f�����悤�AExecuteInEditMode��t�^
[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    /// <summary> �J�����̃p�����[�^ </summary>
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

    // ��ʑ̂Ȃǂ̈ړ��X�V���ς񂾌�ɃJ�������X�V�������̂ŁALateUpdate���g��
    private void LateUpdate()
    {
        if (_parent == null || _child == null || _camera == null)
        {
            return;
        }

        // �p�����[�^���e��I�u�W�F�N�g�ɔ��f
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