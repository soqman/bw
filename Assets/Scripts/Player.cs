using System;
using UnityEngine;

public class Player : MonoBehaviour, Level.ILevelComponent, IDamageable, SpellsController.ISpellCasterView
{
    public interface ISpellsController
    {
        void SetNext();
        void SetPrevious();
        void Fire(Vector2 startPosition, Vector2 direction);
    }
    
    public interface IMovementController
    {
        public void Move(Vector2 value);
        Vector2 Position { get; }
        Vector2 Direction { get; }
    }

    public event Action OnDead;

    [SerializeField] private CustomAnimation damageAnimation;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spellRenderer;

    private ISpellsController _spellsController;
    private IMovementController _movementController;
    private float _armor;
    private float _initialHealth;

    private float _health;


    public void Init(IMovementController movementController, ISpellsController spellsController, float initialHealth, float armor)
    {
        _movementController = movementController;
        _spellsController = spellsController;
        _armor = armor;
        _initialHealth = initialHealth;
    }
    
    private void Register()
    {
        App.Input.OnJoystickUpdate += OnJoystickUpdate;
        App.Input.OnFireDown += OnFireDown;
        App.Input.OnLeftTriggerDown += OnLeftTriggerDown;
        App.Input.OnRightTriggerDown += OnRightTriggerDown;
    }

    private void Unregister()
    {
        App.Input.OnJoystickUpdate -= OnJoystickUpdate;
        App.Input.OnFireDown -= OnFireDown;
        App.Input.OnLeftTriggerDown -= OnLeftTriggerDown;
        App.Input.OnRightTriggerDown -= OnRightTriggerDown;
    }

    private void OnJoystickUpdate(Vector2 value)
    {
        if (value == Vector2.zero) return;
        
        _movementController.Move(value);
    }

    private void OnFireDown()
    {
        _spellsController.Fire(_movementController.Position, _movementController.Direction);
    }

    private void OnLeftTriggerDown()
    {
        _spellsController.SetPrevious();
    }
    
    private void OnRightTriggerDown()
    {
        _spellsController.SetNext();
    }

    public void OnStartLevel()
    {
        _health = _initialHealth;
        transform.position = Vector3.zero;
        spriteRenderer.color = Color.white;
        Register();
    }
    
    public void OnStopLevel()
    {
        Unregister();
    }

    public void ApplyDamage(float value)
    {
        if (value == 0) return;

        var damage = value * (1 - _armor);
        _health -= damage;
        damageAnimation.Play();
        Debug.Log($"damaged: {damage}");
        if (_health < 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        OnDead?.Invoke();
    }

    public void SetColor(Color color)
    {
        spellRenderer.color = color;
    }
}