using UnityEngine;

namespace CaptainClaw.Scripts.Player
{
    public class AnimationHandler : MonoBehaviour, ISerializationCallbackReceiver {
        [SerializeField] private Animator anim;
        [SerializeField] private static Animator _anim;

        #region Anim Hashes
        private static int id_movementX = Animator.StringToHash("movementX");
        private static int id_movementY = Animator.StringToHash("movementY");
        private static int id_isJumping = Animator.StringToHash("isJumping");
        private static int id_isMoving = Animator.StringToHash("isMoving");
        private static int id_isClimbing = Animator.StringToHash("isClimbing");
        #endregion Anim Hashes

        public static void Move() {
            if (InputReceiver.Movement != Vector2.zero)
                _anim.SetBool(id_isMoving, true);
            else
                _anim.SetBool(id_isMoving, false);

            
            _anim.SetFloat(id_movementX, InputReceiver.SmoothMovement.x);
            _anim.SetFloat(id_movementY, InputReceiver.SmoothMovement.y);
        }

        public static void Climb(bool isClimbing) {
            if(!isClimbing) {
                _anim.SetBool(id_isClimbing, false);
                _anim.speed = 1f;
                return;
            }

            _anim.SetBool(id_isClimbing, true);
            _anim.speed = Mathf.Abs(InputReceiver.SmoothMovement.y);
            _anim.SetFloat(id_movementY, InputReceiver.SmoothMovement.y);
        }

        public static void Jump(bool isJumping) {
            _anim.SetBool(id_isJumping, isJumping);
        }

        public void OnBeforeSerialize()
        {
            anim = _anim;
        }

        public void OnAfterDeserialize()
        {
            _anim = anim;
        }
    }
}
