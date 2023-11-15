using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        Register();
    }

    private void OnDisable()
    {
        Unregister();
    }

    private void Register()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        GameManager.OnGameStarted += Hide;
        GameManager.OnGameEnded += Show;
    }

    private void Unregister()
    {
        startButton.onClick.RemoveListener(OnStartButtonClick);
        GameManager.OnGameStarted -= Hide;
        GameManager.OnGameEnded -= Show;
    }

    private void OnStartButtonClick()
    {
        App.Game.StartGame();
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    
    private void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}