using System.Collections;
using UnityEngine;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(Material))]
    public class DissolvingPlatform : MonoBehaviour
    {
        [Header("Dissolve Stats")]
        [SerializeField] private float minDissolveRatio = 0f;
        [SerializeField] private float maxDissolveRatio = 1f;
        [SerializeField, Min(0f)] private float dissolveTime = 3f;
        [SerializeField, Min(0f)] private float threshold = 0.02f;

        [Header("Collider Stats")]
        [SerializeField] private Vector3 center = Vector3.zero;
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private LayerMask layerMask;

        
        private Material material;
        private Vector2 cacheTile;

        private void Awake() {
            this.material = this.GetComponent<Renderer>().material;
            this.cacheTile = this.material.GetVector("_Dissolve_Tile_Direction");

            this.Reset();
        }

        private void OnEnable() {
            this.Reset();
        }

        private void Reset() {
            this.material.SetVector("_Dissolve_Tile_Direction", this.cacheTile * Time.deltaTime / this.dissolveTime);
            this.material.SetFloat("_Dissolve_Strength", this.minDissolveRatio);
        }

        private bool once = false;
        private void FixedUpdate() {
            var colliders = Physics.OverlapBox(this.transform.TransformPoint(this.center), this.scale, this.transform.rotation, layerMask, QueryTriggerInteraction.Collide);

            if (colliders.Length != 1)
                return;

            if (!this.once) {
                StartCoroutine(this.Dissolve());
                this.once = true;
            }
        }

        private IEnumerator Dissolve() {
            var t = 0f;
            var dissolvePercentage = this.minDissolveRatio;

            while (dissolvePercentage < this.maxDissolveRatio - this.threshold) {
                dissolvePercentage = Mathf.Lerp(this.minDissolveRatio, this.maxDissolveRatio, t / this.dissolveTime);
                t += Time.deltaTime;
                this.material.SetFloat("_Dissolve_Strength", dissolvePercentage);

                yield return new WaitForEndOfFrame();
            }

            this.gameObject.SetActive(false);
        }

        private void OnDestroy() {
            this.material.SetVector("_Dissolve_Tile_Direction", this.cacheTile);
            this.material.SetFloat("_Dissolve_Strength", this.minDissolveRatio);
        }

        private void OnDrawGizmos() {
            // The collider
            // Gizmos.matrix = Matrix4x4.TRS(this.transform.position, this.transform.rotation, this.transform.lossyScale);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(this.center, this.scale); 
        }
    }
}