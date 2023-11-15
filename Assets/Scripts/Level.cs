using UnityEngine;

public class Level : GameManager.ILevel
{
    private readonly ILevelComponent[] _levelPlayables;
    
    private bool _isStarted;
    
    public interface ILevelComponent
    {
        void Start();
        void Stop();
    }

    public Level(ILevelComponent[] levelPlayables)
    {
        //At first I was passing components here to create a level individually. But then I realized that this class has no other functionality than to manage the life cycle of the level
        _levelPlayables = levelPlayables;
    }
    
    public void StartLevel()
    {
        if (_isStarted) return;

        foreach (var item in _levelPlayables)
        {
            item.Start();
        }
        
        _isStarted = true;
        Debug.Log("level started");
    }

    public void StopLevel()
    {
        if (!_isStarted) return;
        
        foreach (var item in _levelPlayables)
        {
            item.Stop();
        }
        
        _isStarted = false;
        Debug.Log("level stopped");
    }
}
