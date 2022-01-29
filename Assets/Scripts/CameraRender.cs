using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRender : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraOrigin;

    [SerializeField]
    private Camera _cameraTarget;

    private RenderTexture _finalRender;

    [SerializeField]
    private Material _finalCameraRenderMat;

    private int _screenWidth = -1;
    private int _screenHeight = -1;
    private Rect _screenRect = new Rect();

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

        _screenRect = new Rect(0, 0, _screenWidth, _screenHeight);

        RenderTextureDescriptor renderTextureDescriptor;
        if (_cameraOrigin.targetTexture != null)
        {
            renderTextureDescriptor = _cameraOrigin.targetTexture.descriptor;
        }
        else
        {
            renderTextureDescriptor = new RenderTextureDescriptor(_screenWidth, _screenHeight);
        }

        _cameraOrigin.targetTexture = new RenderTexture(renderTextureDescriptor);
        _cameraTarget.targetTexture = new RenderTexture(renderTextureDescriptor);
        _finalRender = new RenderTexture(renderTextureDescriptor);

        _finalCameraRenderMat.SetTexture("_CameraOrigin", _cameraOrigin.targetTexture);
        _finalCameraRenderMat.SetTexture("_CameraTarget", _cameraTarget.targetTexture);
    }

    private void OnPreRender()
    {
        SetupRenderTargets();
    }
}
