using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(AnimationHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class MovementMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.5f;

        private bool Jump { get => InputReceiver.JumpPressed || MovementHandler.jumpAgain;}

        private DetectCollision detectCollision;

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;
            
            while (true)
            {
                // var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.forward * InputReceiver.Movement.y);
                var direction = (Camera.main.transform.right * InputReceiver.SmoothMovement.x) + (Camera.main.transform.forward * InputReceiver.SmoothMovement.y);
                var finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1f);
                finalSpeed *= this.speed;

                MovementHandler.Move(direction, finalSpeed);
                MovementHandler.Gravity();
                MovementHandler.Rotate(this.rotationSpeed);
                
                AnimationHandler.Move();

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
                    MovementHandler.SetParent(this.detectCollision.Bottom.Value.transform);
                    nextState = PlayerStates.PlatformMovement;
                    break;
                }
            }

            base.ChangeState(nextState);
        }

    }
}