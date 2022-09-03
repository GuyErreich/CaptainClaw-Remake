using CaptainClaw.Scripts.FSM;

namespace CaptainClaw.Player.Scripts
{
    public class PlayerState : State
    {
        protected PlayerMachine stateMachine;

        private void Awake() {
            this.stateMachine = GetComponent<PlayerMachine>();
        }

        protected void ChangeState(PlayerMachine.StateModes mode) {
            this.stateMachine.SetState(this.stateMachine.States[(int)mode]);
        }
    }
}