using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacer : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _target;

    private void Awake()
    {
        Debug.Assert(_cameraTransform != null, "Camera Transform was null", this);
        Debug.Assert(_target != null, "Camera Target was null", this);
    }

    private void LateUpdate()
    {
        _cameraTransform.position = _target.position;
        _cameraTransform.rotation = _target.rotation;
    }
}
