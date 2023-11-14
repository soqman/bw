public class HardcodedSpeedProvider : MovementController.ISpeedProvider
{
    private const float SpeedHardcoded = 2f;

    public float Speed => SpeedHardcoded;
}