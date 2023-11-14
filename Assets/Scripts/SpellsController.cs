using UnityEngine;

public class SpellsController : MonoBehaviour, Player.ISpellsController
{
    //example of a controller that is already configured on the stage
    public void SetNext()
    {
        Debug.Log("Next spell chose");
    }

    public void SetPrevious()
    {
        Debug.Log("Previous spell chose");
    }

    public void Fire(Vector2 vector)
    {
        Debug.Log($"Fire {vector}");
    }
}