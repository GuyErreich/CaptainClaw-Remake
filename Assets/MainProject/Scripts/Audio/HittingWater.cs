using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainClaw.Scripts.Audio {
    [RequireComponent(typeof(CapsuleCollider))]
    public class HittingWater : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;

        private AudioSource audioSource;

        public void Awake() {
            this.audioSource = this.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other) {
            
        }

        private AudioClip GetRandomClip()
        {
            return this.clips[UnityEngine.Random.Range(0, this.clips.Length)];
        }
    }
}
