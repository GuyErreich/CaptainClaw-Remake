using UnityEngine;
using CaptainClaw.Player.Scripts;


namespace CaptainClaw.Scripts.FSM.Player.Actions
{
    [CreateAssetMenu(menuName = "FSM/Player/Actions/LadderMovement")]
    public class Ladder : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var player = stateMachine.GetComponent<LadderMovement>();

            player.StartMode();
        }
    }
}