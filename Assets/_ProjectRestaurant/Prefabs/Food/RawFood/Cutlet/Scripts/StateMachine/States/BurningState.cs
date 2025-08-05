
public class BurningState : StateRosting
{
    private const string NAMECUTLET = "BurnCulet";
    public BurningState(IStateSwitcherCutlet stateSwitcher, Cutlet cutlet) : base(stateSwitcher, cutlet)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Cutlet.Roasting = EnumRoasting.Burn;
        Cutlet.TimeCooking = Cutlet.Config.BurnStateSettings.BurnTimeCooking;
        Cutlet.Material = Cutlet.Config.BurnStateSettings.BurnMaterial;
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
            StateSwitcher.SwitchState<FireState>();
    }
}
