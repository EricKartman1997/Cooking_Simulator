using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private InputAction _moveAction;
    private InputAction _interactableAction;
    private InputAction _menuAction;

    private Heroik _heroik;
    private Menu _menu;
    private IInputBlocker _inputBlocker;
    private PauseHandler _pauseHandler;
    
    [Inject]
    private void Construct(
        Menu menu,
        IInputBlocker inputBlocker,
        PauseHandler pauseHandler)
    {
        _menu = menu;
        _inputBlocker = inputBlocker;
        _pauseHandler = pauseHandler;
    }
    
    private void Awake()
    {
        _heroik = GetComponent<Heroik>();
        playerInput.ActivateInput();
        _moveAction = playerInput.actions["Move"];
        _interactableAction = playerInput.actions["interactable"];
        _menuAction = playerInput.actions["Menu"];
    }
    
    private void OnEnable()
    {
        _interactableAction.performed += OnPressE;
        _menuAction.performed += OnPressEcs;
    }
    
    private void OnDisable()
    {
        _interactableAction.performed -= OnPressE;
        _menuAction.performed -= OnPressEcs;
    }
    
    private void OnPressE(InputAction.CallbackContext context)
    {
        if (_inputBlocker.IsBlocked(InputBlockType.OnPressE))
            return;

        _heroik.ToInteractAction?.Invoke();
    }
    
    private void OnPressEcs(InputAction.CallbackContext context)
    {
        if (_inputBlocker.IsBlocked(InputBlockType.Menu))
            return;

        if (_pauseHandler.IsPause == false)
        {
            _menu.Show();
        }
        else
        {
            _menu.Hide();
        }
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        if (_inputBlocker.IsBlocked(InputBlockType.Movement))
            return Vector3.zero;
        
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
 
        Vector3 movement =  new Vector3(inputVector.x,0f, inputVector.y);
        
        movement = movement.normalized;
        
        return movement;
    }

}

