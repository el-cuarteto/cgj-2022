using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DualController : MonoBehaviour
{
    [SerializeField]
    private string _dualButton = "Dual Swap";

    private static DualController instance = null;

    private bool _mainEnabled = true;

    public bool IsMainMixEnabled => _mainEnabled;

    [SerializeField]
    private DualSerializedObject _mainMix;

    [SerializeField]
    private DualSerializedObject _dualMix;

    [SerializeField]
    private float _swapTime = 0.3f;

    [SerializeField]
    private bool _isInputActive = true;

    public static bool IsInputActive { get { return instance._isInputActive; } set { instance._isInputActive = value; } }

    [SerializeField]
    private UnityEvent _onMainActivated;

    public static UnityEvent OnMainActivated => instance._onMainActivated;

    [SerializeField]
    private UnityEvent _onDualActivated;

    public static UnityEvent OnDualActivated => instance._onDualActivated;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError($"Duplicated Dual Controller", this);
            Destroy(this);
            return;
        }
        instance = this;
        _mainEnabled = true;
        Debug.Assert(_mainMix != null, "Main Mix was null", this);
        Debug.Assert(_dualMix != null, "Dual Mix was null", this);
    }

    private void Start()
    {
        DualMixer.ChangeMix(_mainMix, 0);
    }

    private void Update()
    {
        if (!_isInputActive)
        {
            return;
        }

        if (Input.GetButtonDown(_dualButton))
        {
            if (_mainEnabled)
            {
                DualMixer.ChangeMix(_dualMix, _swapTime);
                _mainEnabled = false;
                _onDualActivated.Invoke();
            }
            else
            {
                DualMixer.ChangeMix(_mainMix, _swapTime);
                _mainEnabled = true;
                _onMainActivated.Invoke();
            }
        }
    }

    public static void ChangeDualMix(DualSerializedObject newDual)
    {
        Debug.Assert(newDual != null, "Dual Mix was null", instance);

        instance._dualMix = newDual;
        if (!instance._mainEnabled)
        {
            DualMixer.ChangeMix(instance._dualMix, instance._swapTime);
        }
    }

    public static void ChangeMainMix(DualSerializedObject newMain)
    {
        Debug.Assert(newMain != null, "Main Mix was null", instance);

        instance._mainMix = newMain;
        if (instance._mainEnabled)
        {
            DualMixer.ChangeMix(instance._mainMix, instance._swapTime);
        }
    }
}