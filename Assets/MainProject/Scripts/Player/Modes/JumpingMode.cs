using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    public class JumpingMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private float jumpGracePeriod = 0.2f;

        private DetectCollision detectCollider;

        private void Awake() => this.detectCollider = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;

            while (true)
            {
                var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.forward * InputReceiver.Movement.y);
                var finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1f);
                finalSpeed *= this.speed;

                MovementHandler.Jump(this.jumpForce, this.jumpGracePeriod);
                MovementHandler.Move(direction, finalSpeed);
                MovementHandler.Gravity();
                MovementHandler.Rotate(this.transform, this.rotationSpeed);

                yield return null;

                if (MovementHandler.isGrounded) {
                    nextState = PlayerStates.GroundMovement;
                    break;
                }

                if (this.detectCollider.CompareTag("Ladder") ) {
                    nextState = PlayerStates.ClimbLadder;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}