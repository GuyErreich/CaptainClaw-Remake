using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    // [RequireComponent(typeof(DetectCollision))]
    public class MovementHandler : MonoBehaviour {
        #region Private Variables
        private static CharacterController _charController;
        private static Vector3 _velocity;
        private static float _ySpeed;
        private static float? _lastGroundedTime, _jumpButtonPressedTime, _lastClimbTime = null;
        private static float _climbGracePeriod, _jumpGracePeriod;
        private static DetectCollision _detectCollision;
        #endregion Private Variables

        #region Getters/Setters
        public static bool UsePhysics { get; private set; }
        public static bool isMoving { get; private set; } 
        public static bool isRunning { get; private set; } 
        public static Vector3 Velocity { get => _velocity; } 
        public static bool isGrounded { get => _detectCollision.Bottom != null; }
        public static bool jumpAgain { get => Time.time - _jumpButtonPressedTime <= _jumpGracePeriod; } 
        public static bool climbAgain { get => (_lastClimbTime == null) || (Time.time - _lastClimbTime >= _climbGracePeriod); }

        public static Vector3 Direction { 
            get {
                var X = (Camera.main.transform.right.normalized * InputReceiver.Movement.x);
                var Z = (Camera.main.transform.forward.normalized * InputReceiver.Movement.y);
                var directionXZ = Vector3.Scale(X + Z, new Vector3(1,0,1));

                return directionXZ;
            }
        }
        #endregion Getters/Setters
        
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

        public static void Fall(float speed) {
            if (!_charController.isGrounded) {
                if (_ySpeed >= 0f)
                    _ySpeed += Physics.gravity.y * Time.deltaTime;
                else 
                    _ySpeed += Physics.gravity.y * speed * Time.deltaTime;
            }

            if(_charController.isGrounded) {
                _lastGroundedTime = Time.time;

                if(_ySpeed < 0f)
                    _ySpeed = 0f;
            }
        }

        public static void Rotate(float rotationTime) {
            if(InputReceiver.Movement != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Direction, Vector3.up);

                var angle = Quaternion.Angle(_charController.transform.rotation, toRotation);

                _charController.transform.rotation = Quaternion.RotateTowards(_charController.transform.rotation, toRotation, angle / (rotationTime / Time.deltaTime));
            }
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
    }
}