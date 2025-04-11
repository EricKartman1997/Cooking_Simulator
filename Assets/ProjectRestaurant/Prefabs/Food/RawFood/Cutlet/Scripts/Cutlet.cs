using UnityEngine;

public class Cutlet : MonoBehaviour,IForStove
{
    private CutletStateMachine _cutletStateMachine;
    private EnumStateRoasting _stateRoasting;
    
    private Renderer _renderer;
    [SerializeField] private CutletConfigs cutletConfig;
    [SerializeField] private GameObject timePref;
    [SerializeField] private EnumRoasting _roasting; //Debug
    [SerializeField] private float _timeCooking;     //Debug
    [SerializeField] private float _timeRemaining;   //Debug
    [SerializeField] private bool _isOnStove;        //Debug
    private bool _isFire;
    private Timer2 _componentTimer2;
    
    public Material Material
    {
        get => _renderer.material;
        set => _renderer.material = value;
    }
    
    public CutletConfigs Config
    {
        get => cutletConfig;
    }
    
    public Timer2 ComponentTimer
    {
        get => _componentTimer2;
    }
    
    public GameObject Timer
    {
        get => timePref;
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

    private void Awake()
    {
        _stateRoasting = Config.CurrentStateRoasting;
        _renderer = GetComponent<Renderer>();
        _cutletStateMachine = new CutletStateMachine(this,_stateRoasting);
        _componentTimer2 = timePref.GetComponent<Timer2>();
    }

    private void Update()
    {
        _cutletStateMachine.Update();
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
}
