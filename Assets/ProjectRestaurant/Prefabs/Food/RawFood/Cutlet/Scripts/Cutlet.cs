using UnityEngine;

public class Cutlet : MonoBehaviour
{
    private CutletStateMachine _cutletStateMachine;
    
    private EnumStateRoasting _stateRoasting;
    
    private Renderer _renderer;
    [SerializeField] private CutletConfigs cutletConfig;
    [SerializeField]private EnumRoasting _roasting;
    [SerializeField]private float _timeCooking;
    [SerializeField]private float _timeRemaining;
    [SerializeField]private bool _isOnStove = false;
    private bool _isFire;
    
    public Material Material
    {
        get => _renderer.material;
        set => _renderer.material = value;
    }
    
    public CutletConfigs Config
    {
        get => cutletConfig;
    }
    
    public EnumRoasting Roasting
    {
        get => _roasting;
        set => _roasting = value;
    }

    public float TimeRemaining
    {
        get => _timeRemaining;
        set => _timeRemaining = value;
    }
  
    public bool IsOnStove
    {
        get => _isOnStove;
        set => _isOnStove = value;
    }
  
    public float TimeCooking
    {
        get => _timeCooking;
        set => _timeCooking = value;
    }
  
    public bool IsFire
    {
        get => _isFire;
        set => _isFire = value;
    }

    public void Delete()
    {
        Destroy(gameObject);
        Debug.Log("котлета сгорела");
    }

    private void Awake()
    {
        _stateRoasting = Config.CurrentStateRoasting;
        _renderer = GetComponent<Renderer>();
        _cutletStateMachine = new CutletStateMachine(this,_stateRoasting);
    }

    private void Update()
    {
        _cutletStateMachine.Update();
    }
}
