using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualMixer : MonoBehaviour
{
    [SerializeField]
    private Material _finalCameraRenderMat;

    [SerializeField]
    private DualSerializedObject _initialMix;

    private class Job
    {
        public DualSerializedObject mix = null;

        public float seconds = 0;
    };

    private Coroutine _coroutine = null;

    private Job extraJob = null;

    private static DualMixer instance = null;

    private void Awake()
    {
        Debug.Assert(_finalCameraRenderMat != null, $"Camera Material was null", this);
        Debug.Assert(_initialMix != null, $"Initial mix was null", this);
        if (instance != null && instance != this)
        {
            Debug.LogError($"Duplicated Dual Mixes", this);
            Destroy(this);
            return;
        }
        instance = this;


        _finalCameraRenderMat.SetTexture("_Mix1", _initialMix.Mix);
        _finalCameraRenderMat.SetTexture("_Layout1", _initialMix.Layout);
        _finalCameraRenderMat.SetTexture("_Mix2", _initialMix.Mix);
        _finalCameraRenderMat.SetTexture("_Layout2", _initialMix.Layout);
        _finalCameraRenderMat.SetFloat("_LayoutMixValue", 1);
    }

    public static void ChangeMix(DualSerializedObject newMix, float seconds)
    {
        instance.ChangeMixInternal(newMix, seconds);
    }

    private void ChangeMixInternal(DualSerializedObject newMix, float seconds)
    {
        if (_finalCameraRenderMat.GetTexture("_Mix2") == newMix.Mix && _finalCameraRenderMat.GetTexture("_Layout2") == newMix.Layout)
        {
            // Nothing to do
            return;
        }

        if (_coroutine != null)
        {
            extraJob = new Job { mix = newMix, seconds = seconds };
            return;
        }
        _coroutine = StartCoroutine(Mixer(new Job { mix = newMix, seconds = seconds }));
    }

    private IEnumerator Mixer(Job job)
    {
        // Swap
        _finalCameraRenderMat.SetTexture("_Mix1", _finalCameraRenderMat.GetTexture("_Mix2"));
        _finalCameraRenderMat.SetTexture("_Layout1", _finalCameraRenderMat.GetTexture("_Layout2"));
        _finalCameraRenderMat.SetFloat("_LayoutMixValue", 0);

        _finalCameraRenderMat.SetTexture("_Mix2", job.mix.Mix);
        _finalCameraRenderMat.SetTexture("_Layout2", job.mix.Layout);

        float startTime = Time.time;
        yield return null;
        while (true)
        {
            float percent = (Time.time - startTime) / job.seconds;
            _finalCameraRenderMat.SetFloat("_LayoutMixValue", percent);

            if (percent >= 1)
            {
                break;
            }

            yield return null;
        }

        _finalCameraRenderMat.SetFloat("_LayoutMixValue", 1);
        yield return null;

        if (extraJob != null)
        {
            Job copy = extraJob;
            extraJob = null;
            _coroutine = StartCoroutine(Mixer(copy));
        }
        else
        {
            _coroutine = null;
        }
    }
}
