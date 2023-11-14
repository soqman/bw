using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    
    public static InputManager Input { get; private set; }

    private void Awake()
    {
        Input = inputManager;
    }
}