using UnityEngine;
using CaptainClaw.Scripts;

namespace CaptainClaw.Scripts.FSM.Player.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Player/Decisions/IsTouchingLadder")]
    public class IsTouchingLadder : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var collision = stateMachine.GetComponent<DetectCollision>().GetCollider;

            if (collision.gameObject.CompareTag("Ladder")) {
                return true;
            }

            return false;
        }
    }
}