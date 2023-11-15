using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public interface IDamageable
    {
        void ApplyDamage(float value);
    }
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private SpellConfig _config;
    private bool _isFlying;
    private Vector2 _direction;

    public bool IsBusy => _isFlying;

    public void Play(SpellConfig config, Vector2 startPosition, Vector2 direction)
    {
        _config = config;
        _direction = direction;
        RefreshView(startPosition);
        StartMove();
    }

    private void RefreshView(Vector2 startPosition)
    {
        if (_config == null) return;
        
        spriteRenderer.sprite = _config.Image;
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    private void StartMove()
    {
        //you could use DoTween here
        _isFlying = true;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (_isFlying)
        {
            Vector3 movement = _direction * Time.deltaTime * _config.Speed;
            transform.position += movement;
            yield return null;
        }
    }

    private void OnBecameInvisible()
    {
        //you could use triggers on the edges of the scene, but I decided to keep it simple
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            var damageable = other.GetComponent<IDamageable>();
            damageable.ApplyDamage(_config.Damage);
            Reset();
        }
    }
    
    private void Reset()
    {
        _isFlying = false;
        _config = null;
        gameObject.SetActive(false);
    }
}