using UnityEngine;

namespace CaptainClaw.Scripts.FSM.ScriptableObjectConcept
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }
}