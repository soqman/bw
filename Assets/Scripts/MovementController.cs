using UnityEngine;

public class MovementController : Player.IMovementController
{
    public interface ISpeedProvider
    {
        public void ApplySpeed(ref Vector2 value);
    }
    
    private readonly Transform _root;
    private readonly ISpeedProvider _speedProvider;
    
    public MovementController(Transform root, ISpeedProvider speedProvider)
    {
        _root = root;
        _speedProvider = speedProvider;
    }
    
    public void Move(Vector2 value)
    {
        var direction = value * Time.deltaTime;
        _speedProvider.ApplySpeed(ref direction);
        _root.Translate(direction);
    }
}