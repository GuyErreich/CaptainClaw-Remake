using UnityEngine;

namespace CaptainClaw.Scripts.FSM
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }
}