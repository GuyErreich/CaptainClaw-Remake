using System.Collections;
using UnityEngine;
using CaptainClaw.Scripts.Managers;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(BoxCollider))]
    public class EndGameTrigger : MonoBehaviour
    {
        private void Awake() {
            this.GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                GameManager.EndGame();
            }
        }
    }
}