using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] objs)
        {
            UnityEngine.Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay()
        {
            UnityEngine.Debug.Log("OnStateStay");
        }

        public virtual void OnStateExit()
        {
            UnityEngine.Debug.Log("OnStateExit");
        }
    }
}