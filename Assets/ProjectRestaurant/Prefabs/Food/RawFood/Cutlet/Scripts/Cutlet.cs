using UnityEngine;

public class Cutlet : MonoBehaviour,IForStove
{
    private CutletStateMachine _cutletStateMachine;
    
    private EnumStateRoasting _stateRoasting;
    
    private Renderer _renderer;
    [SerializeField] private CutletConfigs cutletConfig;
    [SerializeField]private EnumRoasting _roasting;
    [SerializeField]private float _timeCooking;
    [SerializeField]private float _timeRemaining;
    [SerializeField]private bool _isOnStove;
    private bool _isFire;
    [SerializeField]private bool _isNewCutlet = true;
    
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
    
    public bool IsNewCutlet
    {
        get => _isNewCutlet;
        set => _isNewCutlet = value;
    }

    public void Delete()
    {
        Destroy(gameObject);
        Debug.Log("котлета сгорела");
    }
    
    public void UpdateTime(float timeRemaining)
    {
        _timeRemaining = timeRemaining;
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
