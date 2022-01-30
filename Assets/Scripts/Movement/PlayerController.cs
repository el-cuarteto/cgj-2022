using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

using StandardAssets.Characters.Physics;

[RequireComponent(typeof(OpenCharacterController))]
public class PlayerController : MonoBehaviour
{
    private OpenCharacterController _controller = null;

    [SerializeField]
    private Transform _mainCamera = null;

    [SerializeField]
    private LayerMask _collisionLayers = ~0;

    [SerializeField]
    private float _forwardSpeed = 6f;
    [SerializeField]
    private float _sideSpeed = 6f;

    [SerializeField]
    private float _mouseXSpeed = 1f;
    [SerializeField]
    private float _mouseYSpeed = 1f;
    [SerializeField]
    private float _mouseXSensibility = 1f;
    [SerializeField]
    private float _mouseYSensibility = 1f;

    [SerializeField]
    private float _maxXAngle = 70f;

    [SerializeField]
    private bool _enabled = true;

    private void Awake()
    {
        _controller = GetComponent<OpenCharacterController>();

        Assert.IsNotNull(_controller, "Player controller needs an OpenCharacterController");
        Assert.IsNotNull(_mainCamera, "Player controller needs a Camera");

        _controller.SetCollisionLayerMask(_collisionLayers);
    }


    private void Update()
    {
        if (!_enabled)
            return;

        ManageMouseInput();
        ManageKeyboardInput();
    }


    public void DisableInput()
    {
        _enabled = false;
    }


    public void EnableInput()
    {
        _enabled = true;
    }


    private void ManageMouseInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X") * _mouseXSensibility * _mouseXSpeed, 
            Input.GetAxisRaw("Mouse Y") * _mouseYSensibility * _mouseYSpeed);

        Vector2 right = _mainCamera.right;
        right.y = 0f;

        Quaternion horizontalRotation = Quaternion.AngleAxis(input.x, Vector3.up);
        _mainCamera.rotation = horizontalRotation * _mainCamera.rotation;

        Vector3 forward = _mainCamera.forward;
        forward.y = 0f;

        float angle = Vector3.Angle(forward, _mainCamera.forward);

        if (angle >= _maxXAngle)
        {
            bool up = _mainCamera.forward.y > 0;
            Vector3 angles = _mainCamera.eulerAngles;
            angles.x = (!up ? _maxXAngle - 0.25f : 360f - _maxXAngle + 0.25f);
            _mainCamera.eulerAngles = angles;
            return;
        }


        if (input.y > 0 && (angle + input.y) > _maxXAngle)
            input.y = Mathf.Sign(input.y) * ((_maxXAngle - angle) - 0.25f);
        else if (input.y < 0 && (angle - input.y) > _maxXAngle)
            input.y = Mathf.Sign(input.y) * ((_maxXAngle - angle) - 0.25f);

        float ySign = (_mainCamera.forward.z < 0 ? 1f : -1f);
        Quaternion verticalRotation = Quaternion.AngleAxis(ySign * input.y, right);
        _mainCamera.rotation = _mainCamera.rotation * verticalRotation;
    }


    private void ManageKeyboardInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 right = _mainCamera.right * input.x;
        Vector3 forward = _mainCamera.forward * input.y;

        forward.y = right.y = 0f;
        forward.Normalize();
        right.Normalize();

        right *= _sideSpeed;
        forward *= _forwardSpeed;

        Vector3 movement = (right + forward) * Time.deltaTime;
        _controller.MoveStick(movement);
    }
}
