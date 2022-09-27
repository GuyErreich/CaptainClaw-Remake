using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float colliderSize = 2f;
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

        private void FixedUpdate() {
            var colliders = Physics.OverlapSphere(this.transform.position, this.colliderSize, layerMask, QueryTriggerInteraction.Collide);

            if (colliders.Length != 1) {
                this.Cache_Reset();
                this.ResetDelay();
                return;
            }
                
            print("start");

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
        
        private void OnDrawGizmosSelected() {
            // The height of the launch
            var velocity = this.jumpForce * this.launchBoost;
            float timeToReachApexOfJump = velocity / -Physics.gravity.y;
            float heightOfJump = (0.5f * Physics.gravity.y * Mathf.Pow(timeToReachApexOfJump, 2f)) + (velocity * timeToReachApexOfJump);
            // + (this.jumpForce * this.launchBoost * timeToReachApexOfJump);
            var startPosition = this.transform.position + (Vector3.up * this.transform.localScale.y / 2);
            var jumpPeak = startPosition + (Vector3.up * heightOfJump);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPosition, jumpPeak);
            Gizmos.DrawWireSphere(jumpPeak, 0.5f);

            // The collider
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, this.colliderSize); 
        }
    }
}