using UnityEngine;

namespace CaptainClaw.Scripts
{
    public class DetectCollision : MonoBehaviour {
        [System.Serializable] private struct Detector
        {
            public float height;
            public float radius;
            public float range;

            public Detector(float height, float radius, float range)
            {
                this.height = 0f;
                this.radius = 0.5f;
                this.range = 0.5f;
            }
        }

        public enum direction {
            front = 0,
            bottom = 1,
            frontFeet = 2
        }

        [Header("Detectors:")]
        [SerializeField] private Detector FrontDetector = new Detector(0f, 0.5f, 0.5f);
        [SerializeField] private Detector BottomDetector  = new Detector(0f, 0.5f, 0.5f);
        [SerializeField] private Detector FrontFeetDetector = new Detector(-0.5f, 0.5f, 0.5f);

        public Collider Front { get; private set; }
        public Collider Bottom { get; private set; }
        public Collider FrontFeet { get; private set; }

        private void Update() {
            // Front detection
            this.Front = this.DetectedCollider(this.FrontDetector.height, this.FrontDetector.radius, this.FrontDetector.range, this.transform.forward);
            
            // Bottom detection
            this.Bottom = this.DetectedCollider(this.BottomDetector.height, this.BottomDetector.radius, this.BottomDetector.range, -this.transform.up);

            // Front feet detection
            this.FrontFeet = this.DetectedCollider(this.FrontFeetDetector.height, this.FrontFeetDetector.radius, this.FrontFeetDetector.range, this.transform.forward);
            
        }

        private Collider DetectedCollider(float height, float radius, float range, Vector3 direction) {
            var origin = this.transform.position + (Vector3.up * height);

            RaycastHit hit;
            if (Physics.SphereCast(origin, radius, direction, out hit, range))
                return hit.collider;
            else
                return null;
        }

        public bool CompareTag(string tag, direction dir) {
            if (dir == direction.front)
                return this.Front != null && this.Front.tag == tag;
            if (dir == direction.bottom)
                return this.Bottom != null && this.Bottom.tag == tag;
            if (dir == direction.frontFeet)
                return this.FrontFeet != null && this.FrontFeet.tag == tag;

            throw new System.Exception("Unknown direction");
        } 

        void OnDrawGizmosSelected() {
            // Front detection
            this.DrawDetector(this.FrontDetector.height, this.FrontDetector.radius, this.FrontDetector.range, this.transform.forward);
            
            // Bottom detection
            this.DrawDetector(this.BottomDetector.height, this.BottomDetector.radius, this.BottomDetector.range, -this.transform.up);

            // Front feet detection
            this.DrawDetector(this.FrontFeetDetector.height, this.FrontFeetDetector.radius, this.FrontFeetDetector.range, this.transform.forward);
        }

        private void DrawDetector(float height, float radius, float range, Vector3 direction) {
            var startPoint = this.transform.position + (Vector3.up * height);
            var endPoint = startPoint + (direction * (range - radius));
            Gizmos.DrawLine(startPoint, endPoint);
            Gizmos.DrawWireSphere(startPoint + (direction * range), radius);
        }

    }
}