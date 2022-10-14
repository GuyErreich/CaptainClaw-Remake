using UnityEngine;
using System.Collections;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(DetectCollision))]
    public class PlatformMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;

        private DetectCollision detectCollision;

        private bool Jump { get => InputReceiver.JumpPressed || MovementHandler.jumpAgain;}

        private void Awake() => this.detectCollision = this.GetComponent<DetectCollision>();

        public override IEnumerator On() {
            PlayerStates nextState;

            while (true)
            {
                var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.forward * InputReceiver.Movement.y);
                var finalSpeed = (InputReceiver.RunPressed ? this.sprintMultiplier : 1f);
                finalSpeed *= this.speed;

                MovementHandler.Move(direction, finalSpeed);
                MovementHandler.Rotate(this.rotationSpeed);

                yield return new WaitForEndOfFrame();

                if (this.Jump) {
                    MovementHandler.SetParent(null);
                    nextState = PlayerStates.Jumping;
                    break;
                }

                if(!this.detectCollision.CompareTag("Platform", DetectCollision.direction.bottom)) {
                    MovementHandler.SetParent(null);
                    nextState = PlayerStates.GroundMovement;
                    break;
                }
            }

            base.ChangeState(nextState);
        }
    }
}