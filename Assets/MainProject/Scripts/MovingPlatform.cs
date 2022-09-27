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

        private Transform platform;
        private Vector3 startPoint;
        private Vector3 endPoint; 
        public Vector3 velocity = Vector3.zero;

        private delegate void CurrentMovement();
        private CurrentMovement currentMovement;

        private void Start() {
            this.currentMovement = this.MoveForth;
            this.SetPoints();
            var direction = endPoint - startPoint;
            var yRotation = Quaternion.FromToRotation(refPlatform.forward, direction).eulerAngles.y;
            var rotation = Quaternion.Euler(0, yRotation, 0);
            this.platform = Instantiate(refPlatform, this.startPoint, rotation, this.transform);
        }

        void Update()
        {
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

        private void SetPoints() {
            this.startPoint = this.transform.position + this.start; 
            this.endPoint = this.transform.position + this.end;
        }

        private void OnDrawGizmosSelected() {
            // First point
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(this.startPoint, refPlatform.localScale);
            Gizmos.DrawLine(this.startPoint, this.endPoint);
            Gizmos.DrawWireSphere(this.endPoint, 0.5f); 
        }

        public void OnBeforeSerialize()
        {
            this.SetPoints();
        }

        public void OnAfterDeserialize()
        {
            return;
        }
    }
}
