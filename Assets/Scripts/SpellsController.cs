using UnityEngine;

public class SpellsController : Player.ISpellsController
{
    public interface ISpellCasterView
    {
        void SetColor(Color color);
    }
    
    private readonly Spell.ISpellData[] _spells;
    private readonly GameManager.ISceneProvider _sceneProvider;
    private readonly ISpellCasterView _view;
    
    private int _selectedIndex;

    public SpellsController(Spell.ISpellData[] spells, GameManager.ISceneProvider sceneProvider, ISpellCasterView view)
    {
        _sceneProvider = sceneProvider;
        _spells = spells;
        _view = view;
        RefreshView();
    }

    private void RefreshView()
    {
        _view.SetColor(_spells[_selectedIndex].Color);
    }
    
    public void SetNext()
    {
        _selectedIndex = (_selectedIndex + 1) % _spells.Length;
       RefreshView();
    }

    public void SetPrevious()
    {
        _selectedIndex = (_selectedIndex - 1) % _spells.Length;
        if (_selectedIndex < 0)
        {
            _selectedIndex = _spells.Length - 1;
        }
        
        RefreshView();
    }

    public void Fire(Vector2 startPosition, Vector2 direction)
    {
        var spell = _sceneProvider.GetNewSpell();
        spell.Play(_spells[_selectedIndex], startPosition, direction);
    }
}