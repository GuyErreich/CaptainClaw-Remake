using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaptainClaw.Scripts.Audio {
    public class Land : StateMachineBehaviour
    {
        [SerializeField] private string[] transitionNames;
        [SerializeField] private AudioClip[] clips;

        private AudioSource audioSource;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.audioSource = animator.GetComponent<AudioSource>();

            if(this.audioSource == null) {
                throw new System.Exception("You need to have an AudioSource on the same object that has the animator attached.");
            }

            foreach (var name in this.transitionNames) {
                var IsTransition = animator.GetAnimatorTransitionInfo(layerIndex).IsUserName(name);

                if(IsTransition) {
                    AudioClip clip =  this.GetRandomClip();
                    this.audioSource.PlayOneShot(clip);
                }
            }
        }

        private AudioClip GetRandomClip()
        {
            return this.clips[UnityEngine.Random.Range(0, this.clips.Length)];
        }
    }
}
