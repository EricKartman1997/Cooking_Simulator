
public class MediumState : StateRosting
{
    private const string NAMECUTLET = "MediumCulet";
    public MediumState(IStateSwitcherCutlet stateSwitcher,  Cutlet cutlet) : base(  stateSwitcher, cutlet)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Cutlet.Roasting = EnumRoasting.Medium;
        Cutlet.TimeCooking = Cutlet.Config.MediumStateSettings.MediumTimeCooking;
        Cutlet.Material = Cutlet.Config.MediumStateSettings.MediumMaterial;
        Cutlet.gameObject.name = NAMECUTLET;
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void Update()
    {
        base.Update();
        if(Cutlet.TimeCooking <= Cutlet.TimeRemaining)
            StateSwitcher.SwitchState<BurningState>();
    }
}
