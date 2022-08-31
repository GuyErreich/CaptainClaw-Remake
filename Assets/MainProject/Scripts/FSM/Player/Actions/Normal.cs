using UnityEngine;
using CaptainClaw.Player.Scripts;

namespace CaptainClaw.Scripts.FSM.Player.Actions
{
    [CreateAssetMenu(menuName = "FSM/Player/Actions/NormalMovement")]
    public class Normal : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var player = stateMachine.GetComponent<MovementController>();

            player.StartMode();
        }
    }
}