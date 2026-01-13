public class StartStateAudioPlayer : StateAudioPlayer
{
    public StartStateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController) : base(stateSwitcher, heroikSoundsControl, playerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroikSoundsControl.PlayOneShotClip(AudioNameGamePlay.StartMoveRobotSound);
        
    }
    
    public override void Update()
    {
        base.Update();
        if(TimeCurrent <= 1)
            return;
        // повторная проверка на движение
        if(PlayerController.IsMoving == false)
            StateSwitcher.SwitchState<FinishStateAudioPlayer>();
        
        StateSwitcher.SwitchState<MoveStateAudioPlayer>();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
