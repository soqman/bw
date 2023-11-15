using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public interface ILevel
    {
        void StartLevel();
        void StopLevel();
    }
    
    public interface ISceneProvider
    {
        MonoBehaviour LevelRoutines { get; }
        Player Player { get; } 
        Spell GetNewSpell();
        Enemy GetNewEnemy();
        Rect Rect { get; }
        void Init();
        void Deinit();
        float EnemySpawnOffset { get; }
    }

    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    
    [SerializeField] private SceneProvider levelSceneProvider;
    
    [Header("Level settings")]
    [SerializeField] private SpellConfig[] spells;
    [SerializeField] private EnemyConfig[] enemies;
    [SerializeField] private int enemiesCountMax;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerArmor;
    
    private ILevel _level;
    private ISceneProvider _levelSceneProvider;
    private Player _player;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //in real use, the game may be initialized in a different way and in a different place.  
        //In my case, the data will be taken from the serialized scene objects and scriptableObjects .

        _levelSceneProvider = levelSceneProvider;
        var movementController = new MovementController(_levelSceneProvider.Player.transform, new HardcodedSpeedProvider(), _levelSceneProvider);
        var spellsController = new SpellsController(spells, _levelSceneProvider);
        _player = _levelSceneProvider.Player;
        _player.Init(movementController, spellsController, playerHealth, playerArmor);
        var enemiesController = new EnemiesController(enemiesCountMax, _levelSceneProvider, enemies);

        var levelComponents = new List<Level.ILevelComponent>()
        {
            _player,
            enemiesController,
        };
            
        _level = new Level(levelComponents.ToArray());
    }

    [EditorButton]
    public void StartGame()
    {
        _player.OnDead += StopGame;
        _levelSceneProvider.Init();
        _level?.StartLevel();
        OnGameStarted?.Invoke();
    }

    [EditorButton]
    public void StopGame()
    {
        _player.OnDead -= StopGame;
        _levelSceneProvider.Deinit();
        _level?.StopLevel();
        OnGameEnded?.Invoke();
    }
}
