using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSwarmController : MonoBehaviour, Level.ILevelComponent
{
    private HashSet<ISwarmUnit> _swarm = new ();

    private bool _isActive;
    
    public void RegisterUnit(ISwarmUnit swarmUnit)
    {
        _swarm.Add(swarmUnit);
    }

    public void UnregisterUnit(ISwarmUnit swarmUnit)
    {
        _swarm.Remove(swarmUnit);
    }
    
    public void OnStartLevel()
    {
        _isActive = true;
    }

    public void OnStopLevel()
    {
        _isActive = false;
        _swarm.Clear();
    }
    
    public void Update()
    {
        if (_isActive)
        {
            foreach (var unit in _swarm)
            {
                Move(unit);
            }
        }
    }

    protected abstract void Move(ISwarmUnit unit);
}