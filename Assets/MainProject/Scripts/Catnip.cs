using System.Collections;
using UnityEngine;
using CaptainClaw.Scripts.Player;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(SphereCollider))]
    public class Catnip : MonoBehaviour
    {
        [SerializeField, Range(1, 3)] private float jumpMultiplier = 1.25f;
        [SerializeField] private float duration = 3f;

        private float cachedJumpForce;

        private void Awake() {
            this.GetComponent<SphereCollider>().isTrigger = true;
            this.cachedJumpForce = PlayerStats.JumpForce;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                StartCoroutine(this.StartBuff());
            }
        }

        private IEnumerator StartBuff() {
            this.gameObject.GetComponent<Renderer>().enabled = false;
            this.GetComponent<SphereCollider>().enabled = false;
            PlayerStats.JumpForce *= this.jumpMultiplier;

            var time = 0f;

            while( time <= this.duration ) {
                time += Time.deltaTime;
                print(time);
                yield return new WaitForEndOfFrame();
            }

            this.EndBuff();
        }

        private void EndBuff() {
            PlayerStats.JumpForce = this.cachedJumpForce;
        }
    }
}