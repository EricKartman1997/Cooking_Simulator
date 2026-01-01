using UnityEngine;
using System;

public class Cutlet : MonoBehaviour,IForStove
{
    private Action _stopSound;
    private Action _playSound;
    private CutletStateMachine _cutletStateMachine;
    private EnumStateRoasting _stateRoasting;
    
    [SerializeField] private Renderer renderer;
    [SerializeField] private CutletConfigs cutletConfig;
    [SerializeField] private GameObject timePref;
    [SerializeField] private EnumRoasting _roasting; //Debug
    [SerializeField] private float _timeCooking;     //Debug
    [SerializeField] private float _timeRemaining;   //Debug
    [SerializeField] private bool _isOnStove;        //Debug
    private bool _isFire;
    private TimerCutlet _componentTimerCutlet;

    public Action StopSoundAction
    {
        get => _stopSound;
        set => _stopSound = value;
    }
    
    public Action PlaySoundAction
    {
        get => _playSound;
        set => _playSound = value;
    }
    
    public Material Material
    {
        get => renderer.material;
        set => renderer.material = value;
    }
    
    public CutletConfigs Config
    {
        get => cutletConfig;
    }
    
    public TimerCutlet ComponentTimer
    {
        get => _componentTimerCutlet;
    }
    
    public GameObject Timer
    {
        get => timePref;
    }
    
    public EnumRoasting Roasting
    {
        get => _roasting;
        set
        {
            _roasting = value;
        }
    }

    public float TimeRemaining
    {
        get => _timeRemaining;
        set => _timeRemaining = value;
    }
  
    public bool IsOnStove
    {
        get => _isOnStove;
        set
        {
            if (value == false)
            {
                _stopSound?.Invoke();
            }
            
            _playSound?.Invoke();
            _isOnStove = value;

        }
    }

    public float TimeCooking
    {
        get => _timeCooking;
        set => _timeCooking = value;
    }
  
    public bool IsFire
    {
        get => _isFire;
        set
        {
            if (value == true)
            {
                _stopSound?.Invoke();
            }
            
            _isFire = value;
        } 
    }

    private void Awake()
    {
        _stateRoasting = Config.CurrentStateRoasting;
        //_renderer = GetComponent<Renderer>();
        _cutletStateMachine = new CutletStateMachine(this,_stateRoasting);
        _componentTimerCutlet = timePref.GetComponent<TimerCutlet>();
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
