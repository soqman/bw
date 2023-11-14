using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector2> OnJoystickUpdate;
    public event Action OnFireDown;
    public event Action OnLeftTriggerDown;
    public event Action OnRightTriggerDown;

    public void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        OnJoystickUpdate?.Invoke(new Vector2(x, y));

        if (Input.GetKeyDown(KeyCode.X))
        {
            OnFireDown?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnLeftTriggerDown?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnRightTriggerDown?.Invoke();
        }
    }
}