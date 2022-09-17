using UnityEngine;

namespace CaptainClaw.Scripts
{
    public class DetectCollision : MonoBehaviour {
        [System.Serializable] private struct Detector
        {
            public float radius;
            public float range;

            public Detector(float radius, float range)
            {
                this.radius = 0.5f;
                this.range = 0.5f;
            }
        }

        public enum direction {
            front = 0,
            back = 1,
            above = 2,
            bottom = 3
        }

        [Header("Detectors:")]
        [SerializeField] private Detector FrontDetector = new Detector(0.5f, 0.5f);
        [SerializeField] private Detector BottomDetector  = new Detector(0.5f, 0.5f);

        public Collider Front { get; private set; }
        public Collider Bottom { get; private set; }

        private void Update() {
            RaycastHit hit;

            // Front detection
            if (Physics.SphereCast(this.transform.position, this.FrontDetector.radius, this.transform.forward, out hit, this.FrontDetector.range))
                this.Front = hit.collider;
            else
                this.Front = null;

            // Bottom detection
            if (Physics.SphereCast(this.transform.position, this.BottomDetector.radius, -this.transform.up, out hit, this.BottomDetector.range))
                this.Bottom = hit.collider;
            else
                this.Bottom = null;
        }

        public bool CompareTag(string tag, direction dir) {
            if (dir == direction.front)
                return this.Front != null && this.Front.tag == tag;
            if (dir == direction.bottom)
                return this.Bottom != null && this.Bottom.tag == tag;

            throw new System.Exception("Unknown direction");
        } 

        void OnDrawGizmosSelected() {
            // Front detection
            var frontRadius = this.FrontDetector.radius;
            var frontRange = this.FrontDetector.range;

            Gizmos.DrawLine(this.transform.position, this.transform.position + (this.transform.forward * (frontRange - frontRadius)));
            Gizmos.DrawWireSphere(this.transform.position + (this.transform.forward * frontRange), frontRadius);

            // Bottom detection
            var bottomRadius = this.BottomDetector.radius;
            var bottomRange = this.BottomDetector.range;

            Gizmos.DrawLine(this.transform.position, this.transform.position + (-this.transform.up * (bottomRange - bottomRadius)));
            Gizmos.DrawWireSphere(this.transform.position + (-this.transform.up * bottomRange), bottomRadius);
        }

    }
}