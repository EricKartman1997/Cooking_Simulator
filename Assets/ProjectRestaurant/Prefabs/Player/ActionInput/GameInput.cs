using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }
    
    private void OnEnable()
    {
        _playerInputActions.Player.InteractionWithObjects.performed += OnPressE;
        
    }
    
    private void OnDisable()
    {
        _playerInputActions.Player.InteractionWithObjects.performed -= OnPressE;
    }
    
    private void OnPressE(InputAction.CallbackContext obj)
    {
        EventBus.PressE?.Invoke();
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
 
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}

