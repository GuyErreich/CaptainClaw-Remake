using UnityEngine;
using CaptainClaw.Scripts.Player;

namespace CaptainClaw.Scripts.FSM.Player.Actions
{
    [CreateAssetMenu(menuName = "FSM/Player/Actions/NormalMovement")]
    public class NormalMovement : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var player = stateMachine.GetComponent<MovementController>();

            player.StartMode();
        }
    }
}