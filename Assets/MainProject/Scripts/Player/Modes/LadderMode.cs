using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class LadderMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float positionCorrectionOffset = 0.03f;
        [SerializeField] private float speed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;
        [SerializeField, Range(0.5f, 10f)] private float climbGracePeriod = 1f;

        private DetectCollision detectCollision;

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;
            
            if (this.detectCollision.CompareTag("Ladder", DetectCollision.direction.front)) {
                var positionRelativeToLadder = Vector3.Dot(this.transform.forward, this.detectCollision.Front.transform.forward) < 0 ? -1f : 1f; // Is it behind the ladder or in front of it
                this.transform.rotation = Quaternion.LookRotation(this.detectCollision.Front.transform.forward * positionRelativeToLadder, this.transform.up);

                if (InputReceiver.Movement.y > 0f) {
                    this.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                                this.transform.localPosition.y,
                                                                this.transform.localPosition.z + positionCorrectionOffset * positionRelativeToLadder);
                } 
            }


            while (true)
            {
                // var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.up * InputReceiver.Movement.y);
                var direction = (this.transform.up * InputReceiver.Movement.y);
                var finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1);
                finalSpeed *= this.speed;
                MovementHandler.Climb(direction, finalSpeed, climbGracePeriod);

                yield return new WaitForEndOfFrame();

                if (InputReceiver.JumpPressed) {
                    nextState = PlayerStates.Jumping;
                    break;
                }

                if (MovementHandler.isGrounded) {
                    nextState = PlayerStates.GroundMovement;
                    break;
                }

                if (!this.detectCollision.CompareTag("Ladder", DetectCollision.direction.front) &&
                    !this.detectCollision.CompareTag("Ladder", DetectCollision.direction.frontFeet)) 
                {
                    nextState = PlayerStates.GroundMovement;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}