using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombColorChanger : ColorChanger
{
    [SerializeField] private float _minTransitionDuration;
    [SerializeField] private float _maxTransitionDuration;

    public event Action OnAlphaChanged;

    private void OnValidate()
    {
        if(_minTransitionDuration >= _maxTransitionDuration)
            _minTransitionDuration = _maxTransitionDuration;
    }

    public override void Change(Material material)
    {
        SetDefault(material);
        StartCoroutine(OnChangeAlphaCoroutine(material));
    }

    private IEnumerator OnChangeAlphaCoroutine(Material material)
    {
        float duration = Random.Range(_minTransitionDuration, _maxTransitionDuration);
        float elapsedTime = 0f;
        float targetAlpha = 0f;
        float defaultAlpha = material.color.a;

        while(elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(defaultAlpha, targetAlpha, elapsedTime / duration);

            Color color = material.color;
            color.a = newAlpha;
            material.color = color;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        OnAlphaChanged?.Invoke();
    }
}
