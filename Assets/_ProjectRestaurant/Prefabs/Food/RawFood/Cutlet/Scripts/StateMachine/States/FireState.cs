
public class FireState : StateRosting
{
    
    public FireState(IStateSwitcherCutlet stateSwitcher, Cutlet cutlet) : base(stateSwitcher, cutlet)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        Cutlet.Roasting = EnumRoasting.Fire;
        Cutlet.IsFire = true;
        Cutlet.Delete();
    }
}
