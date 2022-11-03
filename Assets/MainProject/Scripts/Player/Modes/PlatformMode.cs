using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(PlayerStats))]
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class PlatformMode : PlayerState {
        private DetectCollision detectCollision;

        private bool Jump { get => InputReceiver.JumpPressed || MovementHandler.jumpAgain;}

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
                MovementHandler.Rotate(PlayerStats.RotationSpeed);

                AnimationHandler.Move();

                yield return new WaitForEndOfFrame();

                if (this.Jump) {
                    this.transform.parent = null;
                    nextState = PlayerStates.Jumping;
                    break;
                }

                if(!this.detectCollision.CompareTag("Platform", DetectCollision.direction.bottom)) {
                    this.transform.parent = null;
                    nextState = PlayerStates.GroundMovement;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}