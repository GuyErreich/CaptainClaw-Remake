using UnityEngine;
using CaptainClaw.Scripts.FSM;
using System.Collections;

namespace CaptainClaw.Player.Scripts
{
    [RequireComponent(typeof(MovementHandler))]
    public class InAirMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 2f;
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private float jumpGracePeriod = 0.2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.2f;

        private PlayerMachine.StateModes groundMode = PlayerMachine.StateModes.OnGround;

        public override IEnumerator On() {
            while (true)
            {
                var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.forward * InputReceiver.Movement.y);
                var finalSpeed = this.speed * this.sprintMultiplier;

                MovementHandler.HandleJump(this.jumpForce, this.jumpGracePeriod);
                MovementHandler.HandleMovement(direction, finalSpeed);
                MovementHandler.HandleGravity();

                yield return null;

                if (MovementHandler.isGrounded)
                    break;
            }

            if (MovementHandler.isGrounded)
                base.ChangeState(groundMode);
        }

        // private void HandleJump() {
        //     if(this.charController.isGrounded)
        //     {
        //         this.isJumping = false;
        //         this.lastGroundedTime =  Time.time;
        //     }

        //     if (this.jumpPressed)
        //         this.jumpButtonPressedTime = Time.time;

        //     // The same as checking ground but gives a little preiod where it still considers you grounded.
        //     // this gives the char a better jump interaction because most of the times people dont press the jump
        //     // button in the perfect right time to make the char jump again. 
        //     if (Time.time - this.lastGroundedTime <= this.jumpGracePeriod) {
        //         //This does the same but gives a little period while you are in the air 
        //         if (Time.time - this.jumpButtonPressedTime <= this.jumpGracePeriod) {
        //             this.ySpeed = jumpForce;
        //             this.isJumping = true;
        //             // this.jumpPressed = false;

        //             this.lastGroundedTime = null;
        //             this.jumpButtonPressedTime = null;
        //         }
        //     }
        // }
    }
}