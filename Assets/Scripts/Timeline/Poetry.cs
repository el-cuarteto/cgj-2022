using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poetry : MonoBehaviour
{
    [SerializeField]
    private Text _poetryText = null;
    [SerializeField]
    private ParticleSystem _particles = null;


    private void Awake()
    {
        Color color = _poetryText.color;
        _poetryText.color = new Color(color.r, color.g, color.b, 0f);
        _particles.Stop();
    }


    public void ShowPoetry(float easeInTime, float easeOutTime, float totalDuration, string text)
    {
        _poetryText.text = text;
        StartCoroutine(PoetryRoutine(easeInTime, easeOutTime, totalDuration));
        _particles.Play();
    }


    private IEnumerator PoetryRoutine(float easeInTime, float easeOutTime, float totalDuration)
    {
        yield return StartCoroutine(BlendingRoutine(1f, easeInTime));
        yield return new WaitForSeconds(totalDuration - easeInTime - easeOutTime);
        yield return StartCoroutine(BlendingRoutine(0f, easeOutTime));
        _particles.Stop();
    } 

    private IEnumerator BlendingRoutine(float target, float time)
    {
        float step = 1f / time;
        float start = _poetryText.color.a;
        float t = 0f;

        yield return null;
        while (t < 1f)
        {
            Color color = _poetryText.color;
            t = Mathf.Min(t + step * Time.deltaTime, 1f);
            float alpha = Mathf.Lerp(start, target, t);
            _poetryText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}
