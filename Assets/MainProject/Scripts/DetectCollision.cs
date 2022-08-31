using UnityEngine;

namespace CaptainClaw.Scripts
{
    public class DetectCollision : MonoBehaviour {
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float range = 0.5f;

        public new Collider collider { get; private set; }

        private void FixedUpdate() {
            RaycastHit hit;
            if (Physics.SphereCast(this.transform.position, this.radius, this.transform.forward, out hit, this.range)) {
                this.collider = hit.collider;
            }
            else
                this.collider = null;
        }

        


        void OnDrawGizmosSelected() {
            Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.forward * (this.range - this.radius)));
            Gizmos.DrawWireSphere(this.transform.position + (this.transform.forward * this.range), this.radius);
        }
    }
}