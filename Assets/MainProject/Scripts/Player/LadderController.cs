using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    
    public class LadderController : MovementController {
        public override void StartMode() {
            base.StartMode();
        }

        protected override void HandleMovement() {
            this.isMoving = this.movement != Vector2.zero ? true : false;

            var speedMultiplier = this.runPressed ? this.sprintMultiplier : 1f;
            var direction = (this.transform.right * this.movement.x) + (this.transform.up * this.movement.y);

            this.velocity = direction * this.speed * speedMultiplier;

            this.charController.Move(this.velocity * Time.deltaTime);
        }
    }
}