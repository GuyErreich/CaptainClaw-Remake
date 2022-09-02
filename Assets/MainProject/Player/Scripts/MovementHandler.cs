using UnityEngine;

namespace CaptainClaw_Remake.Assets.MainProject.Player.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementHandler : MonoBehaviour {
        private static CharacterController _charController;
        private static Vector3 _velocity;
        private static float _ySpeed;
        private static float? _lastGroundedTime, _jumpButtonPressedTime;

        public static bool isMoving { get; private set; } 
        public static bool isRunning { get; private set; } 
        public static bool isJumping { get; private set; }

        public static bool isGrounded { get => _charController.isGrounded; }

        private void Awake() {
            _charController = this.GetComponent<CharacterController>();
        }

        public static void HandleMovement(Vector3 direction, float speed) {
            _velocity = direction * speed;
            _velocity.y = _ySpeed;

            _charController.Move(_velocity * Time.deltaTime);
        }

        public static void HandleGravity() {
            if (!_charController.isGrounded)
                _ySpeed += Physics.gravity.y * Time.deltaTime;
            else if(_ySpeed < 0f)
                _ySpeed = 0f;
        }

        public static void HandleRotation(Transform self ,float rotationSpeed) {
            float rotationAngle = Mathf.LerpAngle(self.eulerAngles.y, Camera.main.transform.eulerAngles.y, rotationSpeed * Time.deltaTime);
            self.transform.eulerAngles = new Vector3(0f, rotationAngle, 0f);
        }

        public static void HandleJump(float jumpForce, float jumpGracePeriod) {
            if(_charController.isGrounded)
            {
                isJumping = false;
                _lastGroundedTime =  Time.time;
            }

            _jumpButtonPressedTime = Time.time;

            // The same as checking ground but gives a little preiod where it still considers you grounded.
            // this gives the char a better jump interaction because most of the times people dont press the jump
            // button in the perfect right time to make the char jump again. 
            if (Time.time - _lastGroundedTime <= jumpGracePeriod) {
                //_does the same but gives a little period while you are in the air 
                if (Time.time - _jumpButtonPressedTime <= jumpGracePeriod) {
                    _ySpeed = jumpForce;
                    isJumping = true;
                    // this.jumpPressed = false;

                    _lastGroundedTime = null;
                    _jumpButtonPressedTime = null;
                }
            }
        }
    }
}