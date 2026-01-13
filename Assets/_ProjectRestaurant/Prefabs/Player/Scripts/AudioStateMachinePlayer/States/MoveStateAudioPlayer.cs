using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateAudioPlayer : StateAudioPlayer
{
    public MoveStateAudioPlayer(IStateSwitcherAudioPlayer stateSwitcher, HeroikSoundsControl heroikSoundsControl, PlayerController playerController) : base(stateSwitcher, heroikSoundsControl, playerController)
    {
    }

    public override void Enter()
    {
        base.Enter();
        HeroikSoundsControl.PlayClip(AudioNameGamePlay.ContinueMoveRobotSound);
    }
    
    public override void Update()
    {
        base.Update();
        // ждем прекращения движения игрока
        if(PlayerController.IsMoving == true)
            return;
        StateSwitcher.SwitchState<FinishStateAudioPlayer>();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
