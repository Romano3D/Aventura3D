using Ebac.StateMachine;
using UnityEngine;

public class PlayerStateWalk : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        Debug.Log("Player começou a andar");
    }

    public override void OnStateStay()
    {
        // lógica de movimento
    }

    public override void OnStateExit()
    {
        Debug.Log("Player parou de andar");
    }
}