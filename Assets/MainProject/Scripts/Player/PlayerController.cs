using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    public class PlayerController : MovementController {
        [SerializeField] private Animator anim;
        [SerializeField, Range(-1, 1)] private float rayOffset;
        [SerializeField] private LayerMask ignoreLayers;

        private bool isPointing;

        #region Anim Hashes
        private int id_isWalking = Animator.StringToHash("isWalking");
        private int id_isRunning = Animator.StringToHash("isRunning");
        private int id_isGrounded = Animator.StringToHash("isGrounded");
        private int id_Jump = Animator.StringToHash("Jump");
        private int id_Point = Animator.StringToHash("Point");
        private int id_movementX = Animator.StringToHash("movementX");
        private int id_movementY = Animator.StringToHash("movementY");
        private int id_distanceFromGround = Animator.StringToHash("distanceFromGround");
        #endregion Anim Hashes
        // Update is called once per frame
        // protected override void Update() {
        //     base.Update();
        //     this.Movement();
        //     this.Jump();
        //     this.Point();
        //     this.IsGrounded();
        // }

        private void Movement() {
            this.anim.SetBool(id_isWalking, base.isMoving);
            this.anim.SetBool(id_isRunning, base.isRunning);
            this.anim.SetFloat(id_movementX, base.movement.x);
            this.anim.SetFloat(id_movementY, base.movement.y);
        }

        private  void Point() {
            this.anim.SetBool(id_Point, this.isPointing);
        }

        private void Jump() {
            if (base.isJumping) {
               this.anim.SetTrigger(id_Jump);
               this.anim.SetBool(id_isGrounded, false);
            }
        }

        private void IsGrounded() {
            RaycastHit hit;
            Vector3 startPos = this.transform.position + (Vector3.up * this.rayOffset);
            Ray ray = new Ray(startPos, Vector3.down);

            if(Physics.Raycast(ray, out hit, ignoreLayers)) {
                var distance = startPos.y - hit.point.y;

                this.anim.SetFloat(id_distanceFromGround, distance);
            }

            this.anim.SetBool(id_isGrounded, base.isGrounded);
        }

        public void ReceiveInput(Vector2 movement, bool runPressed, bool jumpPressed, bool isPointing) {
            base.ReceiveInput(movement, runPressed, jumpPressed);
            this.isPointing = isPointing;
        }
    }
}
