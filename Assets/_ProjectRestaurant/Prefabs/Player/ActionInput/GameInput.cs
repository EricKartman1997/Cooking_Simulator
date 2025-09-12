using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    //private PlayerInputActions _playerInputActions;
    private InputAction _interactableAction;
    private InputAction _moveAction;
    
    private void Awake()
    {
        playerInput.ActivateInput();
        _moveAction = playerInput.actions["Move"];
        _interactableAction = playerInput.actions["interactable"];
        
        //_playerInputActions = new PlayerInputActions();
        //playerInputActions.Player.Enable();
    }
    
    
    
    private void OnEnable()
    {
        _interactableAction.performed += OnPressE;
        
    }
    
    private void OnDisable()
    {
        _interactableAction.performed -= OnPressE;
    }
    
    private void OnPressE(InputAction.CallbackContext obj)
    {
        EventBus.PressE?.Invoke();
    }
    
    private void OnMove(InputAction.CallbackContext context)
    {
        GetMovementVectorNormalized();
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
 
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}

