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
    
    //private GameManager _gameManager;
    private Menu _menu;
    
    private bool _isPause;
    private IHandlerPause _pauseHandler;
    
    private void Awake()
    {
        playerInput.ActivateInput();
        _moveAction = playerInput.actions["Move"];
        _interactableAction = playerInput.actions["interactable"];
        _menuAction = playerInput.actions["Menu"];
    }
    
    // private void Start()
    // {
    //     //playerInput.ActivateInput();
    //     _moveAction = playerInput.actions["Move"];
    //     _interactableAction = playerInput.actions["interactable"];
    //     _menuAction = playerInput.actions["Menu"];
    //
    //     Debug.Log("Move = " + _moveAction);
    // }
    
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
    private void ConstructZenject(Menu menu, IHandlerPause pauseHandler)
    {
        _menu = menu;
        _pauseHandler = pauseHandler;
        _pauseHandler.Add(this);
    }
    
    private void OnPressE(InputAction.CallbackContext context)
    {
        if (_isPause == true)
            return;
        
        heroik.ToInteractAction?.Invoke();
    }
    
    private void OnPressEcs(InputAction.CallbackContext context)
    {
        Debug.Log("Вызов меню");
        if (_menu.IsPause == false)
        {
            _menu.Show();
            return;
        }
        _menu.Hide();
        
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        if (_isPause == true)
            return Vector2.zero;
        
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
 
        Vector3 movement =  new Vector3(inputVector.x,0f, inputVector.y);
        
        movement = movement.normalized;
        
        return movement;
    }

    public void SetPause(bool isPaused) => _isPause = isPaused;

}

