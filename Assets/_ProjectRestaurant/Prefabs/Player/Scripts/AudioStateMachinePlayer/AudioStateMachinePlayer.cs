using System;
using System.Collections.Generic;
using System.Linq;

public class AudioStateMachinePlayer : IStateSwitcherAudioPlayer
{
    private List<IStateAudioPlayer> _statesList;
    private IStateAudioPlayer _currentState;
    
    public AudioStateMachinePlayer(HeroikSoundsControl heroikSoundsControl,PlayerController playerController)
    {
        _statesList = new List<IStateAudioPlayer>()
        {
            new FirstStartStateAudioPlayer(this,heroikSoundsControl,playerController),
            new IdleStateAudioPlayer(this,heroikSoundsControl,playerController),
            new StartStateAudioPlayer(this,heroikSoundsControl,playerController),
            new MoveStateAudioPlayer(this,heroikSoundsControl,playerController),
            new FinishStateAudioPlayer(this,heroikSoundsControl,playerController)
        };
        
        _currentState = _statesList[0];
        _currentState.Enter();
    }
    
    public void SwitchState<T>() where T : IStateAudioPlayer
    {
        IStateAudioPlayer state = _statesList.FirstOrDefault(state => state is T);
        
        //проверка на null
        if (state == null)
            throw new InvalidOperationException($"State of type {typeof(T).Name} not found in states list");

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }
    
    public void Update() => _currentState.Update();
}

public enum EnumStateAudioMove
{
    Raw = 0,
    Medium = 1,
    Burn = 2,
}
