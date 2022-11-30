using System.Collections;
using UnityEngine;
using CaptainClaw.Scripts.Player;
using UnityEngine.Events;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(SphereCollider))]
    public class Catnip : MonoBehaviour
    {
        [Header("Stats")] 
        [SerializeField] private GameObject catnip;

        [Header("Stats")]
        [SerializeField, Range(1, 3)] private float jumpMultiplier = 1.25f;
        [SerializeField] private float duration = 3f;

        [Header("Buff Events"), Space()]
        [SerializeField] private UnityEvent onStart;
        [SerializeField] private UnityEvent onEnd;

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
            this.GetComponent<Collider>().enabled = false;
            PlayerStats.JumpForce *= this.jumpMultiplier;

            this.onStart.Invoke();

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
            this.onEnd.Invoke();
        }
    }
}