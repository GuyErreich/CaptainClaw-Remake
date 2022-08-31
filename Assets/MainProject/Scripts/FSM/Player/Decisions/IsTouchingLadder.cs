using UnityEngine;
using CaptainClaw.Scripts;
using System.Collections;

namespace CaptainClaw.Scripts.FSM.Player.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Player/Decisions/IsTouchingLadder")]
    public class IsTouchingLadder : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            new WaitForSeconds(3f);
            var collider = stateMachine.GetComponent<DetectCollision>().collider;
            
            if (collider.gameObject.CompareTag("Ladder")) {
                return true;
            }

            return false;
        }
    }
}