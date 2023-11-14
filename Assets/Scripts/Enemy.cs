using UnityEngine;

public class Enemy : MonoBehaviour, Spell.IDamageable
{
    public void ApplyDamage(float value)
    {
        Debug.Log($"Applied damage {value}");
    }
}