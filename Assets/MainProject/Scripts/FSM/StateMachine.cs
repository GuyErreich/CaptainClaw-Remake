using UnityEngine;

namespace CaptainClaw.Scripts.FSM
{
    public class StateMachine : MonoBehaviour
    {
        protected State state;

        public async void SetState(State state) 
        {
            this.state = state;
            await state.Start();
        }
    }
}