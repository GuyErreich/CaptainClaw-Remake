// using UnityEngine;
// using CaptainClaw.Scripts;

// namespace CaptainClaw.Player.Scripts
// {
//     public class LadderMode : MonoBehaviour {
//         [Header("Stats")]
//         [SerializeField] private float speed = 7f;

//         private CharacterController charController;
//         private float ySpeed;
//         private Vector3 velocity;
//         private bool jumpPressed;
//         private Vector2 movement;
//         private float? jumpButtonPressedTime;

//         public bool isMoving { get; protected set; } 
//         public bool isJumping { get; protected set; }


//         private void Awake() {
//             this.movementController = this.GetComponent<MovementController>();
//             this.charController = this.GetComponent<CharacterController>();
//         }

//         public void StartMode() {
//             this.HandleMovement();
//             this.HandleJump();
//         }

//         private void HandleMovement() {
//             this.isMoving = this.movement != Vector2.zero ? true : false;

//             var direction = (this.transform.up * this.movement.y);

//             this.velocity = direction * this.speed;
//             this.velocity.y += this.ySpeed;
//             this.ySpeed = 0;

//             this.charController.Move(this.velocity * Time.deltaTime);
//         }


//         private void HandleJump() {
//             if (this.jumpPressed)
//                 this.jumpButtonPressedTime = Time.time;

//             // The same as checking ground but gives a little preiod where it still considers you grounded.
//             // this gives the char a better jump interaction because most of the times people dont press the jump
//             //This does the same but gives a little period while you are in the air 
//             if (Time.time - this.jumpButtonPressedTime <= movementController.JumpGracePeriod) {
//                 this.ySpeed = movementController.JumpForce;
//                 this.isJumping = true;

//                 this.jumpButtonPressedTime = null;
//             }
//         }

//         public void ReceiveInput(Vector2 movement, bool jumpPressed) {
//             this.movement = movement;
//             this.jumpPressed = jumpPressed;
//         }
//     }
// }