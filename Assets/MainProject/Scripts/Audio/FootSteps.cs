using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainClaw.Scripts.Audio {
    [RequireComponent(typeof(AudioSource))]
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;

        private AudioSource audioSource;

        public void Awake() {
            this.audioSource = this.GetComponent<AudioSource>();
        }

        private void Step() {
            AudioClip clip =  this.GetRandomClip();
            this.audioSource.PlayOneShot(clip);
        }

        private AudioClip GetRandomClip()
        {
            return this.clips[UnityEngine.Random.Range(0, this.clips.Length)];
        }
    }
}
