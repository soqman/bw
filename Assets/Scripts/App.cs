using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameManager gameManager;
    
    public static InputManager Input { get; private set; }
    public static GameManager Game { get; private set; }

    private void Awake()
    {
        Input = inputManager;
        Game = gameManager;
    }
}