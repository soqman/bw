using UnityEngine;

public class Player
{
    public interface ISpellsController
    {
        void SetNext();
        void SetPrevious();
        void Fire(Vector2 vector);
    }
    
    public interface IMovementController
    {
        public void Move(Vector2 value);
    }

    private readonly ISpellsController _spellsController;
    private readonly IMovementController _movementController;

    private Vector2 _currentDirection;


    public Player(IMovementController movementController, ISpellsController spellsController)
    {
        _movementController = movementController;
        _spellsController = spellsController;
    }
    
    private void Register()
    {
        App.Input.OnJoystickUpdate += OnJoystickUpdate;
        App.Input.OnFireDown += OnFireDown;
        App.Input.OnLeftTriggerDown += OnLeftTriggerDown;
        App.Input.OnRightTriggerDown += OnRightTriggerDown;
    }

    private void Unregister()
    {
        App.Input.OnJoystickUpdate -= OnJoystickUpdate;
        App.Input.OnFireDown -= OnFireDown;
        App.Input.OnLeftTriggerDown -= OnLeftTriggerDown;
        App.Input.OnRightTriggerDown -= OnRightTriggerDown;
    }

    private void OnJoystickUpdate(Vector2 value)
    {
        if (value == Vector2.zero) return;
        
        _currentDirection = value.normalized;
        _movementController.Move(value);
    }

    private void OnFireDown()
    {
        _spellsController.Fire(_currentDirection);
    }

    private void OnLeftTriggerDown()
    {
        _spellsController.SetPrevious();
    }
    
    private void OnRightTriggerDown()
    {
        _spellsController.SetNext();
    }

    public void Init()
    {
        Register();
    }
    
    public void Deinit()
    {
        Unregister();
    }
}