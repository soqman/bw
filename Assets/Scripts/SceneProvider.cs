using System.Collections.Generic;
using UnityEngine;

public class SceneProvider : MonoBehaviour, GameManager.ISceneProvider
{
    // I don't want to write resource management logic because it depends on usage scenarios
    
    [SerializeField] private Player player;
    [SerializeField] private Spell spellPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform root;
    [SerializeField] private Rect sceneRect;
    [SerializeField] private float enemySpawnOffset;

    private readonly List<Spell> _spellsPool = new();
    private readonly List<Enemy> _enemiesPool = new();

    public Player Player => player;
    public MonoBehaviour LevelRoutines => this;
    public Rect Rect => sceneRect;
    public float EnemySpawnOffset => enemySpawnOffset;
    
    //Here you can make a universal pool manager for any objects, but I don't need it right now
    public Spell GetNewSpell()
    {
         foreach (var spell in _spellsPool)
         {
             if (!spell.IsBusy)
             {
                 return spell;
             }
         }
         
         var newSpell = Instantiate(spellPrefab, root);
         _spellsPool.Add(newSpell);
         return newSpell;
    }
    
    public Enemy GetNewEnemy()
    {
        foreach (var enemy in _enemiesPool)
        {
            if (!enemy.gameObject.activeSelf)
            {
                return enemy;
            }
        }
         
        var newEnemy = Instantiate(enemyPrefab, root);
        _enemiesPool.Add(newEnemy);
        return newEnemy;
    }
    
    public void Init()
    {
        player.gameObject.SetActive(true);
    }

    public void Deinit()
    {
        player.gameObject.SetActive(false);
        foreach (var item in _spellsPool)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in _enemiesPool)
        {
            item.gameObject.SetActive(false);
        }
    }
}