using UnityEngine;

public class HardcodedSpeedProvider : MovementController.ISpeedProvider
{
    private const float SpeedHardcoded = 10f;
    
    public void ApplySpeed(ref Vector2 value)
    {
        value *= SpeedHardcoded;
    }
}