using UnityEngine;
using Ebac.StateMachine;

public class PlayerStateIdle : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        
    }

    public override void OnStateStay()
    {
        // lógica enquanto o player está parado
    }

    public override void OnStateExit()
    {
        
    }
}