using UnityEngine;

namespace CaptainClaw.Player.Scripts
{
    public class InputReceiver
    {
        private bool runPressed ,jumpPressed;
        protected Vector2 movement;

        public void Receive(Vector2 movement, bool runPressed, bool jumpPressed) {
            this.movement = movement;
            this.runPressed = runPressed;
            this.jumpPressed = jumpPressed;
        }
    }
}