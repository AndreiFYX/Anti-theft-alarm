using System.Collections;
using UnityEngine;

public class BeginningAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSourse;
    [SerializeField] private float _maxValue = 1f;
    [SerializeField] private float _fadeDuration = 3f;

    private Coroutine _fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief) == false)
            return;

        if (!_alarmSourse.isPlaying)
        {
            _alarmSourse.Play();
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeValue(_alarmSourse.volume, _maxValue, _fadeDuration));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out Thief thief) == false)
            return;

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }

        _fadeCoroutine = StartCoroutine(FadeValue(_alarmSourse.volume, 0f, _fadeDuration / 2f));
    }

    private IEnumerator FadeValue(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _alarmSourse.volume = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            yield return null;
        }

        _alarmSourse.volume = endValue;

        if (endValue == 0.01f)
        {
            _alarmSourse.Stop();
        }
    }
}