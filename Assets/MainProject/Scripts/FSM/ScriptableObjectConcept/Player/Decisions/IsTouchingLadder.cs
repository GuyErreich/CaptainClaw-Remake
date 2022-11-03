using UnityEngine;
using CaptainClaw.Scripts;
using System.Collections;

namespace CaptainClaw.Scripts.FSM.ScriptableObjectConcept.Player.Decisions
{
    [CreateAssetMenu(menuName = "FSM/Player/Decisions/IsTouchingLadder")]
    public class IsTouchingLadder : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            new WaitForSeconds(3f);
            // var collider = stateMachine.GetComponent<DetectCollision>();
            
            // if (collider.CompareTag("Ladder", DetectCollision.direction.front)) {
            //     return true;
            // }

            return false;
        }
    }
}