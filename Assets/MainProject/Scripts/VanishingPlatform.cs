using UnityEngine;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(Material))]
    public class VanishingPlatform : MonoBehaviour
    {

        [Header("Vanish Stats")]
        [SerializeField, Min(0f)] private float vanishingTime = 3f;
        [SerializeField, Min(0f)] private float materializingTime = 3f;
        [SerializeField, Min(0f)] private float visibleTime = 3f;
        [SerializeField, Min(0f)] private float invisibleTime = 3f;
        [SerializeField, Min(0f)] private float threshold = 0.02f;
        [SerializeField, Min(0f)] private float delay;


        private Material material;
        private delegate void CurrentTransition();
        private CurrentTransition currentTransition;
        // private Vector3 velocity;
        private float startTime, visibleStartTime, invisibleStartTime, time;
        private new Collider collider;

        private void Awake() {
            this.material = this.GetComponent<Renderer>().material;
            this.collider = this.GetComponent<Collider>();
        }

        private void Start() {
            this.currentTransition = this.Vanish;
            this.startTime = Time.time;
        }
        
        void Update()
        {
            if (Time.time - this.startTime < this.delay) {
                return;
            }

            currentTransition();
        }

        private void ResetTime() {
            this.time = 0;
        }

        private void Vanish() {
            if (Time.time - this.visibleStartTime < this.visibleTime) {
                return;
            }

            var dissolvePercentage = Mathf.Lerp(0f, 1f, time / this.vanishingTime);
            this.time += Time.deltaTime;
            this.material.SetFloat("_Amount", dissolvePercentage);

            if (dissolvePercentage > 1 - this.threshold) {
                this.invisibleStartTime = Time.time;
                this.collider.enabled = false;
                this.ResetTime();
                this.currentTransition = this.Materialize;
            }
        }

        private void Materialize() {
            if (Time.time - this.invisibleStartTime < this.invisibleTime) {
                return;
            }

            this.collider.enabled = true;

            var dissolvePercentage = Mathf.Lerp(1f, 0f, time / this.materializingTime);
            this.time += Time.deltaTime;
            this.material.SetFloat("_Amount", dissolvePercentage);

            if (dissolvePercentage < 0 + this.threshold) {
                this.visibleStartTime = Time.time;
                this.ResetTime();
                this.currentTransition = this.Vanish;
            }
        }
    }
}