using UnityEngine;
using UnityEngine.Events;
using CaptainClaw.Scripts.Managers;

namespace CaptainClaw.Scripts.Player {
    [RequireComponent(typeof(CharacterController))]
    public class DetectHit : MonoBehaviour {
        [SerializeField] private float hitGracePeriod = 3f;
        [SerializeField] private LayerMask layerMask;

        private CharacterController characterController;
        private float? lastHitTime;

        private void Awake() {
            this.characterController = GetComponent<CharacterController>();
            this.lastHitTime = Time.time;
        }

        private void Update() {
            if (Time.time - this.lastHitTime <= this.hitGracePeriod)
                return;

            Vector3 p1 = this.transform.position + Vector3.up * 0.25f;
            Vector3 p2 = p1 + Vector3.up * this.characterController.height;

            var hits = Physics.OverlapCapsule(p1, p2, this.characterController.radius, layerMask);

            if (hits != null && hits.Length > 0) {
                foreach(var hit in hits) {
                    var damage = hit.GetComponent<Damage>();
                    if (damage != null) {
                        this.lastHitTime = Time.time;
                        PlayerStats.Health += damage.Amount;
                        return;
                    }
                }
            }
        }

    }
}