using UnityEngine;

public class MovementController : Player.IMovementController
{
    public interface ISpeedProvider
    {
        public float Speed { get; }
    }
    
    private readonly Transform _root;
    private readonly ISpeedProvider _speedProvider;
    private readonly GameManager.ISceneProvider _sceneProvider;

    public Vector2 Position => _root.position;
    public Vector2 Direction => _root.up;
    
    public MovementController(Transform root, ISpeedProvider speedProvider, GameManager.ISceneProvider sceneProvider)
    {
        _root = root;
        _speedProvider = speedProvider;
        _sceneProvider = sceneProvider;
    }
    
    public void Move(Vector2 value)
    {
        Vector3 movement = value * Time.deltaTime * _speedProvider.Speed;
        var newPosition = _root.position + movement;
        if (_sceneProvider.Rect.Contains(newPosition))
        {
            _root.position = newPosition;
        }
        
        _root.up = value.normalized;
    }

   
}