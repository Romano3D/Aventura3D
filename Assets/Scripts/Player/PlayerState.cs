using Ebac.Core.Singleton;
using Ebac.StateMachine;
using UnityEngine;


public class PlayerState : Singleton<PlayerState>
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        JUMP
    }

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();

        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIdle());
        stateMachine.RegisterStates(PlayerStates.WALK, new PlayerStateWalk());
        stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerStateJump());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    private void Update()
    {
        stateMachine.Update();

        if (Input.GetKey(KeyCode.W))
        {
            stateMachine.SwitchState(PlayerStates.WALK);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            stateMachine.SwitchState(PlayerStates.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SwitchState(PlayerStates.JUMP);
        }
    }
}
