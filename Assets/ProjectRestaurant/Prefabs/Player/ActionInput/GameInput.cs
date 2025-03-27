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

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
 
        inputVector = inputVector.normalized;
        
        return inputVector;
    }

    private void OnEnable()
    {
        _playerInputActions.Player.InteractionWithObjects.performed += OnInteractablePeformed;
        
    }
    
    private void OnDisable()
    {
        _playerInputActions.Player.InteractionWithObjects.performed -= OnInteractablePeformed;
    }
    
    private void OnInteractablePeformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Нажата буква Е. Новая система ввода");
    }
}

