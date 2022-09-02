using UnityEngine;

namespace CaptainClaw.Scripts.FSM.ScriptableObjectConcept
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }
}