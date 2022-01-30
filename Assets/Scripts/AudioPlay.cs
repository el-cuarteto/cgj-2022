using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour
{

    private Coroutine coroutine = null;
    private AudioSource audioSource;

    [SerializeField]
    private float volume = 0.3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        audioSource.loop = true;
        audioSource.volume = 0;
        audioSource.Play();
    }

    public void Play()
    {
        SetVolume(1, volume);
    }

    public void Stop()
    {
        SetVolume(1, 0);
    }

    public void SetVolume(float seconds, float target)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PlayInternal(seconds, audioSource.volume, target));
    }

    private IEnumerator PlayInternal(float seconds, float start, float target)
    {
        if (seconds <= 0)
        {
            audioSource.volume = target;
            yield return null;
        }
        else
        {
            float startTime = Time.time;
            yield return null;
            while (true)
            {
                float percent = (Time.time - startTime) / seconds;
                audioSource.volume = Mathf.Lerp(start, target, percent);

                if (percent >= 1)
                {
                    break;
                }

                yield return null;
            }

            audioSource.volume = target;
            yield return null;
        }

        coroutine = null;
    }
}