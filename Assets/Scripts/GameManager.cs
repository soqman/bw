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
        GameObject Player { get; } 
        Spell GetNewSpell();
        Rect Rect { get; }
        void Init();
        void Deinit();
    }
    
    [SerializeField] private SceneProvider levelSceneProvider;
    [SerializeField] private SpellConfig[] spells;
    
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
        _level = new Level(movementController, spellsController);
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
