using UnityEngine;
using CaptainClaw.Scripts;
using CaptainClaw.Player.Scripts;


namespace CaptainClaw.Scripts.FSM.Player.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Player/Decisions/IsJumpingOffLadder")]
    public class IsJumpingOffLadder : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var player = stateMachine.GetComponent<LadderMovement>();

            if (player.isJumping) 
                return true;
            
            return false;
        }
    }
}