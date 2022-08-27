using UnityEngine;

namespace CaptainClaw.Scripts.FSM
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine state);
    }
}