using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStartStateAudioPlayer : StateAudioPlayer
{
    public FirstStartStateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController) : base(stateSwitcher, heroikSoundsControl, playerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroikSoundsControl.PlayOneShotClip(AudioNameGamePlay.FirstStartMoveRobotSound);
        
    }
    
    public override void Update()
    {
        base.Update();
        if(TimeCurrent <= 1)
            return;
        StateSwitcher.SwitchState<IdleStateAudioPlayer>();

    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
