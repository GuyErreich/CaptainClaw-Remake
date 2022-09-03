using UnityEngine;
using CaptainClaw.Scripts.FSM;
using System.Collections;

namespace CaptainClaw.Player.Scripts
{
    [RequireComponent(typeof(MovementHandler))]
    public class MovementMode : PlayerState {
        [Header("Stats")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1f, 5f)] private float sprintMultiplier = 1.5f;

        private PlayerMachine.StateModes jumpMode = PlayerMachine.StateModes.Jumping;

        public override IEnumerator On() {
            while (true)
            {
                var direction = (this.transform.right * InputReceiver.Movement.x) + (this.transform.forward * InputReceiver.Movement.y);
                var finalSpeed = this.speed * this.sprintMultiplier;

                MovementHandler.HandleMovement(direction, finalSpeed);
                MovementHandler.HandleGravity();
                MovementHandler.HandleRotation(this.transform, this.rotationSpeed);

                yield return null;

                if (InputReceiver.JumpPressed)
                    break;
            }

            if(InputReceiver.JumpPressed)
                base.ChangeState(jumpMode);
        }
    }
}