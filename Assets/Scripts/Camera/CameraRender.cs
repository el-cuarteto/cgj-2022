using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRender : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraOrigin;

    [SerializeField]
    private Camera _cameraTarget;

    [SerializeField]
    private Material _finalCameraRenderMat;

    private int _screenWidth = -1;
    private int _screenHeight = -1;

    [SerializeField]
    [Range(0f, 1f)]
    private float _targetResolutionModifier = 1.0f;

    private void Awake()
    {
        Debug.Assert(_finalCameraRenderMat != null, $"Camera Material was null", this );

        Debug.Assert(_cameraOrigin != null, $"Origin Camera was null", this);
        Debug.Assert(_cameraTarget != null, $"Target Camera was null", this);
    }

    private void SetupRenderTargets()
    {
        if (_screenWidth == Screen.width && _screenHeight == Screen.height)
        {
            return;
        }

        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        RenderTextureDescriptor renderTextureOriginDescriptor;
        if (_cameraOrigin.targetTexture != null)
        {
            renderTextureOriginDescriptor = _cameraOrigin.targetTexture.descriptor;
        }
        else
        {
            renderTextureOriginDescriptor = new RenderTextureDescriptor(_screenWidth, _screenHeight);
        }

        _cameraOrigin.targetTexture = new RenderTexture(renderTextureOriginDescriptor);

        RenderTextureDescriptor renderTextureTargetDescriptor;
        if (_cameraOrigin.targetTexture != null)
        {
            renderTextureTargetDescriptor = _cameraTarget.targetTexture.descriptor;
        }
        else
        {
            renderTextureTargetDescriptor = new RenderTextureDescriptor((int)((float)_screenWidth * _targetResolutionModifier), (int)((float)_screenHeight * _targetResolutionModifier));
        }

        _cameraTarget.targetTexture = new RenderTexture(renderTextureTargetDescriptor);

        _finalCameraRenderMat.SetTexture("_CameraOrigin", _cameraOrigin.targetTexture);
        _finalCameraRenderMat.SetTexture("_CameraTarget", _cameraTarget.targetTexture);
    }

    private void OnPreRender()
    {
        SetupRenderTargets();
    }
}
