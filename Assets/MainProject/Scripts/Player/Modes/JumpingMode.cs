using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class JumpingMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private bool usePhysics = false;
        [SerializeField] private float jumpForce = 7f;
        [SerializeField, Range(0f, 1f)] private float jumpGracePeriod = 0.2f;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;

        public float JumpForce { get => jumpForce; set => jumpForce =  value;}

        private DetectCollision detectCollision;

        private bool Climb { get => this.detectCollision.CompareTag("Ladder", DetectCollision.direction.front) && MovementHandler.climbAgain;}

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;
    
            var direction = Vector3.zero;
            var finalSpeed = 0f;

            if (this.usePhysics) {
                direction = this.GetComponent<CharacterController>().velocity;
                finalSpeed = 1f;
            }

            while (true)
            {
                if (!this.usePhysics) {
                    direction = (Camera.main.transform.right * InputReceiver.Movement.x) + (Camera.main.transform.forward * InputReceiver.Movement.y);
                    finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1f);
                    finalSpeed *= this.speed;
                }

                MovementHandler.Jump(this.jumpForce, this.jumpGracePeriod);
                MovementHandler.Move(direction, finalSpeed);
                MovementHandler.Gravity();
                MovementHandler.Rotate(this.rotationSpeed);

                yield return new WaitForEndOfFrame();

                if (MovementHandler.isGrounded) {
                    nextState = PlayerStates.GroundMovement;
                    break;
                }

                if (this.Climb) {
                    nextState = PlayerStates.ClimbLadder;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}