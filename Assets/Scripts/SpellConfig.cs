using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Configs/Spell")]
public class SpellConfig : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public Sprite Image => image;
    public float Damage => damage;
    public float Speed => speed;
}
