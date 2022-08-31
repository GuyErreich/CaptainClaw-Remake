using UnityEngine;

namespace CaptainClaw.Player.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour {
        [Header("Stats")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1.1f, 5f)] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 7f, jumpGracePeriod = 0.2f;
        
        private CharacterController charController;
        private Vector3 velocity;
        private float ySpeed;
        private float? lastGroundedTime, jumpButtonPressedTime;
        private bool runPressed ,jumpPressed;
        
        protected Vector2 movement;

        public bool isMoving { get; protected set; } 
        public bool isRunning { get; protected set; } 
        public bool isJumping { get; protected set; }
        
        
        public bool isGrounded { get => this.charController.isGrounded; }
        public float Speed { get => this.speed; }
        public float JumpGracePeriod { get => this.jumpGracePeriod; }
        public float JumpForce { get => this.JumpForce; }


        // Start is called before the first frame update
        private void Awake() {
            this.charController = this.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        public void StartMode() {
            this.HandleMovement();
            this.HandleGravity();
            this.HandleJump();
            this.HandleRotation();
        }

        private void HandleMovement() {
            this.isMoving = this.movement != Vector2.zero ? true : false;

            var speedMultiplier = this.runPressed ? this.sprintMultiplier : 1f;
            var direction = (this.transform.right * this.movement.x) + (this.transform.forward * this.movement.y);

            this.velocity = direction * this.speed * speedMultiplier;
            this.velocity.y = this.ySpeed;

            this.charController.Move(this.velocity * Time.deltaTime);
        }

        private void HandleGravity() {
            if (!this.charController.isGrounded)
                this.ySpeed += Physics.gravity.y * Time.deltaTime;
            else if(ySpeed < 0f)
                ySpeed = 0f;
        }

        private void HandleRotation() {
            if(this.movement.x != 0f || this.movement.y != 0f)
            {
                float rotationAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, rotationSpeed * Time.deltaTime);
                this.transform.eulerAngles = new Vector3(0f, rotationAngle, 0f);
            }
        }

        private void HandleJump() {
            if(this.charController.isGrounded)
            {
                this.isJumping = false;
                this.lastGroundedTime =  Time.time;
            }

            if (this.jumpPressed)
                this.jumpButtonPressedTime = Time.time;

            // The same as checking ground but gives a little preiod where it still considers you grounded.
            // this gives the char a better jump interaction because most of the times people dont press the jump
            // button in the perfect right time to make the char jump again. 
            if (Time.time - this.lastGroundedTime <= this.jumpGracePeriod) {
                //This does the same but gives a little period while you are in the air 
                if (Time.time - this.jumpButtonPressedTime <= this.jumpGracePeriod) {
                    this.ySpeed = jumpForce;
                    this.isJumping = true;
                    // this.jumpPressed = false;

                    this.lastGroundedTime = null;
                    this.jumpButtonPressedTime = null;
                }
            }
        }

        public void ReceiveInput(Vector2 movement, bool runPressed, bool jumpPressed) {
            this.movement = movement;
            this.runPressed = runPressed;
            this.jumpPressed = jumpPressed;
        }
    }
}