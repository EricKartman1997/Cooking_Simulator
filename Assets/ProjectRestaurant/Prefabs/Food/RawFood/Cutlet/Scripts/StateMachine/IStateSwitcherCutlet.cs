
public interface IStateSwitcherCutlet
{
    public void SwitchState<T>() where T : IStateCutlet;
}
