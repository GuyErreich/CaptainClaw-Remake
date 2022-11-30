using System.Collections;
using UnityEngine;
using CaptainClaw.Scripts.Player;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(Collider))]
    public class Catnope : MonoBehaviour
    {
        [SerializeField, Range(0.001f, 1)] private float jumpMultiplier = 0.25f;

        private float cachedJumpForce;

        private void Awake() {
            this.GetComponent<Collider>().isTrigger = true;
            this.cachedJumpForce = PlayerStats.JumpForce;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                PlayerStats.JumpForce *= this.jumpMultiplier;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.tag == "Player") {
                PlayerStats.JumpForce = this.cachedJumpForce;
            }
        }
    }
}