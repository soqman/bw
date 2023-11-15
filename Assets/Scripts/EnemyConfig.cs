using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject, Enemy.IEnemyData
{
    [SerializeField] private float health;
    [SerializeField][Range(0f, 1f)] private float armor;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Sprite image;

    public float Health => health;
    public float Armor => armor;
    public float Damage => damage;
    public float Speed => speed;
    public Sprite Image => image;
}