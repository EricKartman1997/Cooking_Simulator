
public class RawState : StateRosting
{
    private const string NAMECUTLET = "RawCulet";
    
    public RawState( IStateSwitcherCutlet stateSwitcher,  Cutlet cutlet) : base( stateSwitcher, cutlet )
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        
        Cutlet.Roasting = EnumRoasting.Raw;
        Cutlet.TimeCooking = Cutlet.Config.RawStateSettings.RawTimeCooking;
        Cutlet.Material = Cutlet.Config.RawStateSettings.RawMaterial;
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
            StateSwitcher.SwitchState<MediumState>();
    }
}
