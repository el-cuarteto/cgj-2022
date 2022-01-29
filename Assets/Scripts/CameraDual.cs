using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDual : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraOrigin;

    [SerializeField]
    private Transform _cameraTarget;

    private void Awake()
    {
        Debug.Assert( _cameraOrigin != null, $"Origin Camera Transform was null", this );
        Debug.Assert( _cameraTarget != null, $"Target Camera Transform was null", this );

        Debug.Assert(_cameraOrigin.parent != null, $"Origin Camera Parent Transform was null", this);
        Debug.Assert(_cameraTarget.parent != null, $"Target Camera Parent Transform was null", this);
    }

    private void LateUpdate()
    {
        _cameraTarget.localPosition = _cameraOrigin.localPosition;
        _cameraTarget.localRotation = _cameraOrigin.localRotation;
    }
}
