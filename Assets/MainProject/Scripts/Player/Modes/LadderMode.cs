using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    public class LadderMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;
        [SerializeField] private float climbGracePeriod = 1f;

        public override IEnumerator On() {
            PlayerStates nextState;

            while (true)
            {
                var direction = (this.transform.up * InputReceiver.Movement.y);
                var finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1);
                finalSpeed *= this.speed;
                MovementHandler.Climb(direction, finalSpeed);

                yield return null;

                if (InputReceiver.JumpPressed) {
                    nextState = PlayerStates.Jumping;
                    break;
                }

                if (MovementHandler.isGrounded) {
                    nextState = PlayerStates.GroundMovement;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}