using Ebac.StateMachine;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Player começou a pular");
    }

    public override void OnStateStay()
    {

    }

    public override void OnStateExit()
    {

    }
}
