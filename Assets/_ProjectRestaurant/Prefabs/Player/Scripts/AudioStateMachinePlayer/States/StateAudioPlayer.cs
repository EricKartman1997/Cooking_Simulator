using UnityEngine;

public abstract class StateAudioPlayer : IStateAudioPlayer
{
    protected readonly IStateSwitcherAudioPlayer StateSwitcher;
    protected readonly HeroikSoundsControl HeroikSoundsControl;
    protected readonly PlayerController PlayerController;
    private float _timeSound = 0;

    protected float TimeCurrent => _timeSound;
    
    protected StateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController)
    {
        StateSwitcher = stateSwitcher;
        HeroikSoundsControl = heroikSoundsControl;
        PlayerController = playerController;
    }

    public virtual void Enter()
    {
        //Debug.Log("Enter");
    }

    public virtual void Exit()
    {
        HeroikSoundsControl.StopClip();
        _timeSound = 0;
    }

    public virtual void Update()
    {
        if(HeroikSoundsControl.IsPause == true)
        {
            return;
        }
        
        _timeSound += Time.deltaTime;
    }
}
