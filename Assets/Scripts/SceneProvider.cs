using System.Collections.Generic;
using UnityEngine;

public class SceneProvider : MonoBehaviour, GameManager.ISceneProvider
{
    // I don't want to write resource management logic because it depends on usage scenarios
    
    [SerializeField] private GameObject player;
    [SerializeField] private Spell spellPrefab;
    [SerializeField] private Transform root;

    private readonly List<Spell> _spellsPool = new();

    public GameObject Player => player;
    
    
    public Spell GetNewSpell()
    {
         foreach (var spell in _spellsPool)
         {
             if (!spell.IsBusy)
             {
                 return spell;
             }
         }
         
         var newSpell = Instantiate(spellPrefab, root);
         _spellsPool.Add(newSpell);
         return newSpell;
    }
    
    public void Init()
    {
        player.SetActive(true);
    }

    public void Deinit()
    {
        player.SetActive(false);
        _spellsPool.Clear();
    }
}