using UnityEngine;

public abstract class StateRosting : IStateCutlet
{
    //protected readonly StateMachineDateCutlet Date;
    protected readonly IStateSwitcherCutlet StateSwitcher;
    protected readonly Cutlet Cutlet;

    protected StateRosting(IStateSwitcherCutlet stateSwitcher,  Cutlet cutlet)
    {
        StateSwitcher = stateSwitcher;
        Cutlet = cutlet;
    }

    public virtual void Enter()
    {
        Debug.Log(GetType());
    }

    public virtual void Exit()
    {
        Cutlet.TimeRemaining = 0;
        Cutlet.Roasting = EnumRoasting.Uncertain;
    }

    public virtual void Update()
    {
        if(Cutlet.IsOnStove == false)
            return;

        Cutlet.TimeRemaining += Time.deltaTime;
    }
}
