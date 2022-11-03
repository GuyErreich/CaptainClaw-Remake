using UnityEngine;
using CaptainClaw.Scripts.Player;

namespace CaptainClaw.Scripts {
    public class JumpPlatform : MonoBehaviour
    {
        [Header("Jump Stats")]
        [SerializeField] private float jumpForce = 7f;
        [SerializeField, Range(1, 10)] private float launchBoost = 1.5f;
        [SerializeField] private float launchDelay = 3f;

        [Header("Collider Stats")]
        [SerializeField] private float scale = 2f;
        [SerializeField] private Vector3 center = Vector3.zero;

        [SerializeField] private LayerMask layerMask;

        private float startTime;
        private float currentTime;

        
#nullable enable
        CharacterController? cached_characterController;
#nullable disable

        private void ResetDelay() {
            this.startTime = Time.time;
        }

        private void Cache_Reset() {
            this.cached_characterController = null;
        }

        // void OnControllerColliderHit(ControllerColliderHit hit)
        // {
        //     Debug.Log("onControllerColliderHit: " + hit);
        //     // if (hit.collider.CompareTag("Player")) {
        //     //     this.currentTime = Time.time;
        //     //     if (this.currentTime - this.startTime >= this.launchDelay) {
        //     //         MovementHandler.Launch(this.jumpForce * this.launchBoost);
        //     //     }
        //     // }
        //     // else this.ResetDelay();
        // }

        private void FixedUpdate() {
            var colliders = Physics.OverlapSphere(this.transform.TransformPoint(this.center), this.scale, layerMask, QueryTriggerInteraction.Collide);

            if (colliders.Length != 1) {
                this.Cache_Reset();
                this.ResetDelay();
                return;
            }

            this.currentTime = Time.time;
            if (this.currentTime - this.startTime < this.launchDelay) {
                this.Cache_Reset();
                return;
            }

            if (!this.cached_characterController) {
                foreach (var collider in colliders) {
                    this.cached_characterController = collider.GetComponent<CharacterController>();
                }
            }

            if (this.cached_characterController) {
                MovementHandler.Launch(this.jumpForce * this.launchBoost);
                this.Cache_Reset();
                this.ResetDelay();
            }
        }
        
        private void OnDrawGizmos() {
            // The height of the launch
            var velocity = this.jumpForce * this.launchBoost;
            float timeToReachApexOfJump = velocity / -Physics.gravity.y;
            float heightOfJump = (0.5f * Physics.gravity.y * Mathf.Pow(timeToReachApexOfJump, 2f)) + (velocity * timeToReachApexOfJump);
            var startPosition = this.transform.position + (Vector3.up * this.transform.localScale.y / 2);
            var jumpPeak = startPosition + (Vector3.up * heightOfJump);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPosition, jumpPeak);
            Gizmos.DrawWireSphere(jumpPeak, 0.5f);

            // The collider
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.TransformPoint(this.center), this.scale); 
        }
    }
}