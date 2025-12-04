using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInput : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Heroik heroik;
    private InputAction _interactableAction;
    private InputAction _moveAction;
    private InputAction _menuAction;
    
    private GameManager _gameManager;
    //private Menu _menu;
    
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
    }

    [Inject]
    private void ConstructZenject(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    private void OnPressE(InputAction.CallbackContext context)
    {
        heroik.ToInteractAction?.Invoke();
    }
    
    private void OnPressEcs(InputAction.CallbackContext context)
    {
        Debug.Log("Вызов меню");
        if (_gameManager.IsPause == false)
        {
            _gameManager.ShowMenu();
            return;
        }
        _gameManager.HideMenu();
        
    }
    
    public Vector3 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _moveAction.ReadValue<Vector2>();
 
        Vector3 movement =  new Vector3(inputVector.x,0f, inputVector.y);
        
        movement = movement.normalized;
        
        return movement;
    }
}

