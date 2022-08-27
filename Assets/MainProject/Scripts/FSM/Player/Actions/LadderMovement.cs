using UnityEngine;
using CaptainClaw.Scripts.Player;


namespace CaptainClaw.Scripts.FSM.Player.Actions
{
    [CreateAssetMenu(menuName = "FSM/Player/Actions/LadderMovement")]
    public class LadderMovement : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var player = stateMachine.GetComponent<LadderController>();

            player.StartMode();
        }
    }
}