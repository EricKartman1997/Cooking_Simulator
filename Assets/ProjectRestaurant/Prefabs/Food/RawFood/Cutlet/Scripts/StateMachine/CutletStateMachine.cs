using System;
using System.Collections.Generic;
using System.Linq;

public class CutletStateMachine : IStateSwitcherCutlet
{
    private List<IStateCutlet> _statesList;
    private IStateCutlet _currentState;
    private EnumStateRoasting _enumStateRoasting;

    public CutletStateMachine(Cutlet cutlet, EnumStateRoasting enumStateRoasting)
    {
        _enumStateRoasting = enumStateRoasting;
        _statesList = new List<IStateCutlet>()
        {
            new RawState(this,cutlet),
            new MediumState(this,cutlet),
            new BurningState(this,cutlet),
            new FireState(this,cutlet)
        };
        
        _currentState = _statesList[(int)_enumStateRoasting];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IStateCutlet
    {
        IStateCutlet state = _statesList.FirstOrDefault(state => state is T);
        
        //проверка на null
        if (state == null)
            throw new InvalidOperationException($"State of type {typeof(T).Name} not found in states list");

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();
}
