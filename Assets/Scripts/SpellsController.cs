using UnityEngine;

public class SpellsController : Player.ISpellsController
{
    private readonly Spell.ISpellData[] _spells;
    private readonly GameManager.ISceneProvider _sceneProvider;
    
    private int _selectedIndex;

    public SpellsController(Spell.ISpellData[] spells, GameManager.ISceneProvider sceneProvider)
    {
        _sceneProvider = sceneProvider;
        _spells = spells;
    }
    
    public void SetNext()
    {
        _selectedIndex = (_selectedIndex + 1) % _spells.Length;
    }

    public void SetPrevious()
    {
        _selectedIndex = (_selectedIndex - 1) % _spells.Length;
        if (_selectedIndex < 0)
        {
            _selectedIndex = _spells.Length - 1;
        }
    }

    public void Fire(Vector2 startPosition, Vector2 direction)
    {
        var spell = _sceneProvider.GetNewSpell();
        spell.Play(_spells[_selectedIndex], startPosition, direction);
    }
}