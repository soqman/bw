using UnityEngine;

public class Level : GameManager.ILevel
{
    private bool _isStarted;
    private Player _player;

    public Level(Player.IMovementController moveController, Player.ISpellsController spellsController)
    {
        _player = new Player(moveController, spellsController);
    }
    
    public void StartLevel()
    {
        if (_isStarted) return;
        
        _player.Init();
        _isStarted = true;
        Debug.Log("level started");
    }

    public void StopLevel()
    {
        if (!_isStarted) return;
        
        _player.Deinit();
        _isStarted = false;
        Debug.Log("level stopped");
    }
}
