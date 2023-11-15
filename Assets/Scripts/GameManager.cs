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
        GameObject Player { get; } 
        Spell GetNewSpell();
        Enemy GetNewEnemy();
        Rect Rect { get; }
        void Init();
        void Deinit();
        float EnemySpawnOffset { get; }
    }
    
    [SerializeField] private SceneProvider levelSceneProvider;
    [SerializeField] private SpellConfig[] spells;
    [SerializeField] private EnemyConfig[] enemies;
    [SerializeField] private int enemiesCountMax;
    
    private ILevel _level;
    private ISceneProvider _levelSceneProvider;

    private void Start()
    {
        Init();
        StartGame();
    }

    private void Init()
    {
        //in real use, the game may be initialized in a different way and in a different place.  
        //In my case, the data will be taken from the serialized scene objects and scriptableObjects .

        _levelSceneProvider = levelSceneProvider;
        var movementController = new MovementController(_levelSceneProvider.Player.transform, new HardcodedSpeedProvider(), _levelSceneProvider);
        var spellsController = new SpellsController(spells, _levelSceneProvider);

        var levelComponents = new List<Level.ILevelComponent>()
        {
            new Player(movementController, spellsController),
            new EnemiesController(enemiesCountMax, _levelSceneProvider, enemies),
        };
            
        _level = new Level(levelComponents.ToArray());
    }

    [EditorButton]
    public void StartGame()
    {
        _levelSceneProvider.Init();
        _level?.StartLevel();
    }

    [EditorButton]
    public void StopGame()
    {
        _levelSceneProvider.Deinit();
        _level?.StopLevel();
    }
}
