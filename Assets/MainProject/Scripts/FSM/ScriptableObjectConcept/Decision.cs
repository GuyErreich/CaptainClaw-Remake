using UnityEngine;

namespace CaptainClaw.Scripts.FSM.ScriptableObjectConcept
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine state);
    }
}