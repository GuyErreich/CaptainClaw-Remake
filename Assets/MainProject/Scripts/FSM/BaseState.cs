using UnityEngine;

namespace CaptainClaw.Scripts.FSM
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }
}