public class FinishStateAudioPlayer : StateAudioPlayer
{
    public FinishStateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController) : base(stateSwitcher, heroikSoundsControl, playerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroikSoundsControl.PlayOneShotClip(AudioNameGamePlay.FinishMoveRobotSound);
        
    }
    
    public override void Update()
    {
        base.Update();
        if(TimeCurrent <= 0.8f)
            return;
        // повторная проверка на прекращения движения игрока
        if(PlayerController.IsMoving == true)
            StateSwitcher.SwitchState<StartStateAudioPlayer>();
        
        StateSwitcher.SwitchState<IdleStateAudioPlayer>();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
