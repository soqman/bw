using System.Collections;
using UnityEngine;

public class DamageAnimation : CustomAnimation
{
    //here it would be easier to use DoTween instead
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float time;

    private float _elapsedTime;
    private Color _originalColor;
    private Coroutine _routine;

    private void Awake()
    {
        _originalColor = spriteRenderer.color;
    }

    public override void Play()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
        }
        
        _routine = StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        _elapsedTime = 0f;
        while (_elapsedTime < time)
        {
            _elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(damageColor, _originalColor, animationCurve.Evaluate(_elapsedTime / time));
            yield return null;
        }

        _routine = null;
    }
}