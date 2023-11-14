using UnityEngine;

public class MovementController : Player.IMovementController
{
    public interface ISpeedProvider
    {
        public float Speed { get; }
    }
    
    private readonly Transform _root;
    private readonly ISpeedProvider _speedProvider;

    public Vector2 Position => _root.position;
    public Vector2 Direction => _root.up;
    
    public MovementController(Transform root, ISpeedProvider speedProvider)
    {
        _root = root;
        _speedProvider = speedProvider;
    }
    
    public void Move(Vector2 value)
    {
        Vector3 movement = value * Time.deltaTime * _speedProvider.Speed;
        _root.position += movement;
        _root.up = value.normalized;
    }

   
}