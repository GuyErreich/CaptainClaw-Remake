using UnityEngine;
using CaptainClaw.Scripts.Managers;

namespace CaptainClaw.Scripts {
    [RequireComponent(typeof(ResetLevel))]
    public class RespawnPoint : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                GameManager.SpawnPosition = this.transform.position;
                GameManager.Reset = this.GetComponent<ResetLevel>().Activate;
            }
        }
    }
}