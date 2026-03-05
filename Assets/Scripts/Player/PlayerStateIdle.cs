using UnityEngine;
using Ebac.StateMachine;

public class PlayerStateIdle : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        Debug.Log("Player entrou no estado IDLE");
    }

    public override void OnStateStay()
    {
        // lógica enquanto o player está parado
    }

    public override void OnStateExit()
    {
        Debug.Log("Player saiu do estado IDLE");
    }
}