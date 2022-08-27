using UnityEngine;

namespace CaptainClaw.Scripts
{
    public class DetectCollision : MonoBehaviour {
        private new Collider collider;

        public Collider GetCollider { get => this.collider; }

        private void OnTriggerEnter(Collider other) {
            this.collider = other;
        }

        private void OnCollisionEnter(Collision other) {
            this.collider = other.collider;
        }
    }
}