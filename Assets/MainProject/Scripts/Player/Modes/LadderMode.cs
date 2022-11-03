using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class LadderMode : PlayerState {
        private DetectCollision detectCollision;

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;
            
            if (this.detectCollision.CompareTag("Ladder", DetectCollision.direction.front)) {
                var positionRelativeToLadder = Vector3.Dot(this.transform.forward, this.detectCollision.Front.Value.collider.transform.forward) < 0 ? -1f : 1f; // Is it behind the ladder or in front of it
                this.transform.rotation = Quaternion.LookRotation(this.detectCollision.Front.Value.collider.transform.forward * positionRelativeToLadder, this.transform.up);

                if (InputReceiver.Movement.x > 0f || InputReceiver.Movement.y > 0f) {
                    var distance = this.detectCollision.GetRange(DetectCollision.direction.front);
                    var normal = this.detectCollision.Front.Value.normal;
                    var hitDistance = this.detectCollision.Front.Value.distance;
                    MovementHandler.Move(this.detectCollision.Front.Value.normal, -(distance - (distance - hitDistance)) / Time.deltaTime);
                } 
            }


            while (true)
            {
                var direction = (this.transform.up * InputReceiver.SmoothMovement.y);
                var finalSpeed = (InputReceiver.RunPressed ? PlayerStats.SprintMultiplier : 1);
                finalSpeed *= PlayerStats.ClimbSpeed;
                MovementHandler.Climb(direction, finalSpeed, PlayerStats.ClimbGracePeriod);
                
                AnimationHandler.Climb(true);

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

            AnimationHandler.Climb(false);

            base.ChangeState(nextState);
        }
    }
}