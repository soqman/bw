﻿using System;
using UnityEngine;

public class Enemy : MonoBehaviour, Spell.IDamageable
{
    public interface IEnemyData
    {
        public float Health { get; }
        public float Armor { get; }
        public float Damage { get; }
        public float Speed { get; }
        public Sprite Image { get; }
    }

    //you could separate logic and representation, but that would be overcomplicating the task within the scope of the test
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CustomAnimation damageAnimation;
    
    public event Action<Enemy> OnKill;
    
    private float _health;
    private IEnemyData _config;

    public void Init(IEnemyData config)
    {
        _config = config;
        _health = config.Health;
        spriteRenderer.sprite = _config.Image;
        spriteRenderer.color = Color.black;
        gameObject.SetActive(true);
    }

    public void Deinit()
    {
        _config = null;
        gameObject.SetActive(false);
    }
    
    public void ApplyDamage(float value)
    {
        if (value == 0) return;
        if (_config == null) return;
        
        _health -= value * (1 - _config.Armor);
        damageAnimation.Play();
        
        if (_health < 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        OnKill?.Invoke(this);
    }
}