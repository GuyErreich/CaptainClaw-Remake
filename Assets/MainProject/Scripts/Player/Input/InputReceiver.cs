using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    public class InputReceiver
    {
        public static bool RunPressed { get; private set; }
        public static bool JumpPressed { get; private set; }
        public static Vector2 Movement { get; private set; }

        public static void Receive(Vector2 movement, bool runPressed, bool jumpPressed) {
            Movement = movement;
            RunPressed = runPressed;
            JumpPressed = jumpPressed;
        }
    }
}