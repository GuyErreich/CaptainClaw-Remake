using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(AnimationHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class MovementMode : PlayerState {
        private bool Jump { get => InputReceiver.JumpPressed || MovementHandler.jumpAgain;}

        private DetectCollision detectCollision;

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;
            
            while (true)
            {
                var direction = (Camera.main.transform.right * InputReceiver.SmoothMovement.x) + 
                                (Camera.main.transform.forward * InputReceiver.SmoothMovement.y);
                var finalSpeed = (InputReceiver.RunPressed ? PlayerStats.SprintMultiplier : 1f);
                finalSpeed *= PlayerStats.Speed;
                MovementHandler.Move(direction, finalSpeed);
                // MovementHandler.Gravity();
                MovementHandler.Fall(PlayerStats.FallMultiplier);
                MovementHandler.Rotate(PlayerStats.RotationSpeed);
                
                AnimationHandler.Move();
                AnimationHandler.Grounded(MovementHandler.isGrounded);
                AnimationHandler.Velocity(MovementHandler.Velocity);
                AnimationHandler.GoJackSparroNut();

                yield return new WaitForEndOfFrame();

                if (this.Jump) {
                    nextState = PlayerStates.Jumping;
                    break;
                }
                
                if (this.detectCollision.CompareTag("Ladder", DetectCollision.direction.front)) {
                    nextState = PlayerStates.ClimbLadder;
                    break;
                }

                if(this.detectCollision.CompareTag("Platform", DetectCollision.direction.bottom)) {
                    this.transform.parent = this.detectCollision.Bottom.Value.transform;
                    nextState = PlayerStates.PlatformMovement;
                    break;
                }

            }

            base.ChangeState(nextState);
        }

    }
}