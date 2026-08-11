using System.Collections;
using UnityEngine;

public class BeginningAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSourse;
    [SerializeField] private ThiefDetector _detector;
    [SerializeField] private float _maxValue = 1f;
    [SerializeField] private float _fadeDuration = 3f;

    private Coroutine _fadeCoroutine;

    private void OnEnable()
    {
        _detector.ThiefEntered += OnThiefEntered;
        _detector.ThiefExited += OnThiefExited;
    }

    private void OnDisable()
    {
        _detector.ThiefEntered -= OnThiefEntered;
        _detector.ThiefExited -= OnThiefExited;
    }

    private void OnThiefEntered()
    {
        if (!_alarmSourse.isPlaying)
            _alarmSourse.Play();

        if(_fadeCoroutine != null)
            StopCoroutine( _fadeCoroutine );

        _fadeCoroutine = StartCoroutine(FadeValue(_maxValue, _maxValue / _fadeDuration));
    }

    private void OnThiefExited()
    {
        if( _fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine );

        _fadeCoroutine = StartCoroutine(FadeValue(0f,_maxValue / (_fadeDuration / 2f )));  
    }

    private IEnumerator FadeValue( float targetValue, float speed)
    {
        while(_alarmSourse.volume != targetValue)
        {
            _alarmSourse.volume = Mathf.MoveTowards(_alarmSourse.volume, targetValue, speed * Time.deltaTime);
            yield return null;
        }

        if(targetValue == 0f)
            _alarmSourse.Stop();
    }
}