using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(DetectCollision))]
    public class MovementHandler : MonoBehaviour {
        private static CharacterController _charController;
        private static Vector3 _velocity;
        private static float _ySpeed;
        private static float? _lastGroundedTime, _jumpButtonPressedTime, _lastClimbTime = null;
        private static float _climbGracePeriod, _jumpGracePeriod;
        private static DetectCollision _detectCollision;

        public static bool isMoving { get; private set; } 
        public static bool isRunning { get; private set; } 
        public static bool isGrounded { get => _charController.isGrounded; }
        public static bool jumpAgain { get => Time.time - _jumpButtonPressedTime <= _jumpGracePeriod; } 
        public static bool climbAgain { get => (_lastClimbTime == null) || (Time.time - _lastClimbTime >= _climbGracePeriod); }
        
        private void Awake() {
            _charController = this.GetComponent<CharacterController>();
            _detectCollision = this.GetComponent<DetectCollision>();
        }

        public static void Move(Vector3 direction, float speed) {
            _velocity = direction * speed;
            _velocity.y = _ySpeed;

            _charController.Move(_velocity * Time.deltaTime);
        }

        public static void Gravity() {
            if (!_charController.isGrounded)
                _ySpeed += Physics.gravity.y * Time.deltaTime;

            if(_charController.isGrounded) {
                _lastGroundedTime = Time.time;

                if(_ySpeed < 0f)
                    _ySpeed = 0f;
            }
        }

        public static void Rotate(float rotationSpeed) {
            float rotationAngle = Mathf.LerpAngle(_charController.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, rotationSpeed * Time.deltaTime);
            _charController.transform.eulerAngles = new Vector3(0f, rotationAngle, 0f);
        }

        public static void Jump(float jumpForce, float jumpGracePeriod) {
            if (InputReceiver.JumpPressed)
                _jumpButtonPressedTime = Time.time;

            _jumpGracePeriod = jumpGracePeriod;

            // The same as checking ground but gives a little period where it still considers you grounded.
            // this gives the char a better jump interaction because most of the times people don`t press the jump
            // button in the perfect right time to make the char jump again. 
            if (Time.time - _lastGroundedTime <= jumpGracePeriod || 
                Time.time - _lastClimbTime <= jumpGracePeriod) 
            {
                _ySpeed = jumpForce;

                _lastGroundedTime = null;
                _jumpButtonPressedTime = null;
            }
        }

        public static void Climb(Vector3 direction, float speed, float climbGracePeriod) {
            _lastClimbTime = Time.time;
            _climbGracePeriod = climbGracePeriod;
            _velocity = direction * speed;

            _charController.Move(_velocity * Time.deltaTime);
        }

        public static void Launch(float launchForce) {
            _ySpeed = launchForce;
        }
        
        public static void SetParent(Transform parent) => _charController.transform.SetParent(parent);
    }
}