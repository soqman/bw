using UnityEngine;

public class SceneProvider : MonoBehaviour, GameManager.ISceneProvider
{
    // I don't want to write resource management logic because it depends on usage scenarios
    
    public GameObject player;

    public GameObject Player => player;

    public void Init()
    {
        player.SetActive(true);
    }

    public void Deinit()
    {
        player.SetActive(false);
    }
}