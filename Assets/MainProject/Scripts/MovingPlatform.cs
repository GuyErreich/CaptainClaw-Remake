using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainClaw.Scripts {
    public class MovingPlatform : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField] private Transform refPlatform;
        [SerializeField] private Vector3 start = new Vector3(1f, 0f, 0f);
        [SerializeField] private Vector3 end = new Vector3(-1f, 0f, 0f);
        [SerializeField] private float smoothTime = 1f;
        [SerializeField] private float maxSpeed = 1f;
        [SerializeField] private float threshHold = 0.1f;
        [SerializeField, Min(0f)] private float delay;

        private Transform platform;
        private Vector3 startPoint, endPoint;
        private Quaternion rotation = Quaternion.identity;
        private Vector3 velocity;
        private float startTime, currentTime;

        private delegate void CurrentMovement();
        private CurrentMovement currentMovement;

        private void Start() {
            this.currentMovement = this.MoveForth;
            this.SetPointsAndRotation();
            this.platform = Instantiate(refPlatform, this.startPoint, this.rotation, this.transform);
        }

        void Update()
        {
            this.currentTime = Time.time;
            if (this.currentTime - this.startTime < this.delay) {
                return;
            }

            currentMovement();
        }

        private void MoveForth() {
            this.platform.position = Vector3.SmoothDamp(this.platform.position, this.endPoint, ref this.velocity, this.smoothTime, this.maxSpeed);
            
            if (Vector3.Distance(this.platform.position, this.endPoint) < threshHold)
                currentMovement = this.MoveBack;
        }

        private void MoveBack() {
            this.platform.position = Vector3.SmoothDamp(this.platform.position, this.startPoint, ref this.velocity, this.smoothTime, this.maxSpeed);

            if (Vector3.Distance(this.platform.position, this.startPoint) <= threshHold)
                currentMovement = this.MoveForth;
        }

        private void SetPointsAndRotation() {
            this.startPoint = this.transform.position + this.start; 
            this.endPoint = this.transform.position + this.end;

            var direction = endPoint - startPoint;
            var yRotation = Quaternion.FromToRotation(refPlatform.forward, direction).eulerAngles.y;
            this.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        private void OnDrawGizmos() {
            // First point
            Gizmos.color = Color.cyan;
            Gizmos.matrix = Matrix4x4.TRS(this.startPoint, this.rotation, this.transform.lossyScale);
            
            Gizmos.DrawWireCube(Vector3.zero, refPlatform.localScale);

            Gizmos.matrix = Matrix4x4.identity;

            Gizmos.DrawLine(this.startPoint, this.endPoint);
            Gizmos.DrawWireSphere(this.endPoint, 0.5f); 
        }

        public void OnBeforeSerialize()
        {
            this.SetPointsAndRotation();
        }

        public void OnAfterDeserialize()
        {
            return;
        }
    }
}
