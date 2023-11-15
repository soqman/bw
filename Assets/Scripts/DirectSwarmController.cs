using UnityEngine;

public class DirectSwarmController : BaseSwarmController
{
    [SerializeField] private Transform _target;
    
    protected override void Move(ISwarmUnit unit)
    {
        var position = unit.Transform.position;
        var direction = (_target.position - position).normalized;
        position += direction * Time.deltaTime * unit.Speed;
        unit.Transform.position = position;
        unit.Transform.up = direction;
    }
}