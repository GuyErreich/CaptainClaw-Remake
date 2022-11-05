using UnityEngine;
using UnityEngine.Events;
using CaptainClaw.Scripts.Managers;

namespace CaptainClaw.Scripts.Player {
    [RequireComponent(typeof(CharacterController))]
    public class DetectHit : MonoBehaviour {
        [SerializeField] private float hitLaunchSpeed = 3f;
        [SerializeField] private float hitGracePeriod = 3f;
        [SerializeField] private bool ignoreCollisionInGraceTime = false;
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
            
            this.IgnoreCollision(false);

            Vector3 p1 = this.transform.position + Vector3.up * 0.25f;
            Vector3 p2 = p1 + Vector3.up * this.characterController.height;

            var hits = Physics.OverlapCapsule(p1, p2, this.characterController.radius, layerMask, QueryTriggerInteraction.Collide);

            if (hits != null && hits.Length > 0) {
                foreach(var hit in hits) {
                    var damage = hit.GetComponent<Damage>();
                    if (damage != null) {
                        MovementHandler.Launch(this.hitLaunchSpeed);
                        this.IgnoreCollision(this.ignoreCollisionInGraceTime);
                        this.lastHitTime = Time.time;
                        PlayerStats.CurrentHealth += Mathf.Clamp(damage.Amount, Mathf.NegativeInfinity, PlayerStats.MaxHealth);
                        return;
                    }
                }
            }
        }

        private void IgnoreCollision(bool ignore) {
            for (int i = 0; i < 32; i++)
            {
                if (this.layerMask == (this.layerMask | (1 << i)))
                {
                    Physics.IgnoreLayerCollision(i, LayerMask.NameToLayer("Player"), ignore);
                }
            }
        }

    }
}