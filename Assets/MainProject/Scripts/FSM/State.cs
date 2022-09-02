using UnityEngine;
using System.Threading.Tasks;

namespace CaptainClaw.Scripts.FSM
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual Task Start()
        {
            return Task.CompletedTask;
        }
    }
}