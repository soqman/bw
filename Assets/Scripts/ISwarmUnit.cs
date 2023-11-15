using UnityEngine;

public interface ISwarmUnit
{
    public Transform Transform { get; }
    public float Speed { get; }
}