using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : Level.ILevelComponent
{
    private readonly GameManager.ISceneProvider _sceneProvider;
    private readonly int _maxCount;
    private readonly List<Enemy> _enemies = new();
    private readonly Enemy.IEnemyData[] _configs;
    
    private bool _isActive;
    
    public EnemiesController(int maxCount, GameManager.ISceneProvider sceneProvider, Enemy.IEnemyData[] configs)
    {
        _sceneProvider = sceneProvider;
        _maxCount = maxCount;
        _configs = configs;
    }
    
    public void OnStartLevel()
    {
        StartSpawn();
    }

    public void OnStopLevel()
    {
        StopSpawn();
        foreach (var enemy in _enemies)
        {
            enemy.OnDead -= OnEnemyDead;
        }
        
        _enemies.Clear();
    }

    private void StartSpawn()
    {
        _isActive = true;
        _sceneProvider.LevelRoutines.StartCoroutine(EnemySpawnRoutine());
    }

    private void StopSpawn()
    {
        _isActive = false;
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (_isActive)
        {
            while (_enemies.Count < _maxCount)
            {
                CreateEnemy();
            }
            
            yield return null;
        }
    }

    private void CreateEnemy()
    {
        var enemy = _sceneProvider.GetNewEnemy();
        enemy.transform.position = GetRandomPointOnPerimeter(_sceneProvider.Rect, _sceneProvider.EnemySpawnOffset);
        enemy.OnDead += OnEnemyDead;
        enemy.Init(_configs[Random.Range(0, _configs.Length)]);
        _enemies.Add(enemy);
    }
    
    private Vector2 GetRandomPointOnPerimeter(Rect rect, float offset)
    {
        var randomSide = Random.Range(0, 4);

        return randomSide switch
        {
            0 => new Vector2(Random.Range(rect.x, rect.x + rect.width), rect.y - offset),
            1 => new Vector2(rect.x + rect.width + offset, Random.Range(rect.y, rect.y + rect.height)),
            2 => new Vector2(Random.Range(rect.x, rect.x + rect.width), rect.y + rect.height + offset),
            3 => new Vector2(rect.x - offset, Random.Range(rect.y - offset, rect.y + rect.height)),
            _ => Vector2.zero
        };
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.OnDead -= OnEnemyDead;
        enemy.Deinit();
        _enemies.Remove(enemy);
    }
}