using Ebac.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMExample;

    public class FSMExample : MonoBehaviour
    {
        public enum ExampleEnum
        {
            STATE_0NE,
            STATE_TWO,
            STATE_THREE
        }

        public StateMachine<ExampleEnum> stateMachine;

        private void Start()
        {
            stateMachine = new StateMachine<ExampleEnum> ();
            stateMachine.Init();
            stateMachine.RegisterStates(ExampleEnum.STATE_0NE, new StateBase());
            stateMachine.RegisterStates(ExampleEnum.STATE_TWO, new StateBase());

        }
    }

