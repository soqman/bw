using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Configs/Spell")]
public class SpellConfig : ScriptableObject, Spell.ISpellData
{
    [SerializeField] private Color color;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    public Color Color => color;
    public float Damage => damage;
    public float Speed => speed;
}
