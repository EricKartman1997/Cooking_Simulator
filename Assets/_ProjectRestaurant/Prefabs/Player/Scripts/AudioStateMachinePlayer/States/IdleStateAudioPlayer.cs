using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateAudioPlayer : StateAudioPlayer
{
    public IdleStateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController) : base(stateSwitcher, heroikSoundsControl, playerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroikSoundsControl.PlayClip(AudioNameGamePlay.IdleMoveRobotSound);

    }
    
    public override void Update()
    {
        base.Update();
        // ждем движения игрока
        if(PlayerController.IsMoving == false)
            return;
        StateSwitcher.SwitchState<StartStateAudioPlayer>();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
