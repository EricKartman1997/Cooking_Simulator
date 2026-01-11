using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInput : MonoBehaviour, IPause
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Heroik heroik;
    private InputAction _interactableAction;
    private InputAction _moveAction;
    private InputAction _menuAction;
    
    private Menu _menu;
    
    private bool _isPause;
    private PauseHandler _pauseHandler;

    private bool _isStopInput;
    
    private void Awake()
    {
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
        _pauseHandler.Remove(this);
    }

    [Inject]
    private void ConstructZenject(Menu menu, PauseHandler pauseHandler)
    {
        _menu = menu;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    private void OnPressE(InputAction.CallbackContext context)
    {
        if(_isStopInput == true)
            return;
        
        if (_isPause == true)
            return;
        
        heroik.ToInteractAction?.Invoke();
    }
    
    private void OnPressEcs(InputAction.CallbackContext context)
    {
        if(_isStopInput == true)
            return;
        //Debug.Log("Вызов меню");
        if (_pauseHandler.IsPause == false)
        {
            _menu.Show();
            EventBus.PauseOn.Invoke();
            return;
        }
        _menu.Hide();
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        if(_isStopInput == true)
            return Vector2.zero;
        
        if (_isPause == true)
            return Vector2.zero;
        
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
 
        Vector3 movement =  new Vector3(inputVector.x,0f, inputVector.y);
        
        movement = movement.normalized;
        
        return movement;
    }

    public void SetPause(bool isPaused) => _isPause = isPaused;
    public void SetStopInput(bool isStopInput) => _isStopInput = isStopInput;

}

