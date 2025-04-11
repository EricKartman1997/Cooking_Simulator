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
        
        //Debug.Log(GetType());
    }

    public virtual void Exit()
    {
        Cutlet.TimeRemaining = 0;
        Cutlet.Roasting = EnumRoasting.Uncertain;
    }

    public virtual void Update()
    {
        if (Cutlet.IsOnStove == false)
        {
            Cutlet.Timer.SetActive(false);
            return;
        }
        
        Cutlet.Timer.SetActive(true);
        Cutlet.TimeRemaining += Time.deltaTime;
        Cutlet.ComponentTimer.UpdateArrowRotation(Cutlet.TimeRemaining, Cutlet.TimeCooking);
    }
}
