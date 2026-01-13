
public interface IStateSwitcherAudioPlayer
{
    public void SwitchState<T>() where T : IStateAudioPlayer;
}


