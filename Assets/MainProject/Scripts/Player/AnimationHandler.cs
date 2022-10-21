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
        #endregion Anim Hashes

        public static void Move() {
            _anim.SetFloat(id_movementX, InputReceiver.SmoothMovement.x);
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
