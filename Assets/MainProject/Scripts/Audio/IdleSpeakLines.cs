using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CaptainClaw.Scripts.Audio {
    public class IdleSpeakLines : StateMachineBehaviour
    {
        [SerializeField] private float startDelay = 3f;
        [SerializeField] private float linesDelay = 1f;
        [SerializeField] private AudioClip[] clips;

        private AudioSource audioSource;
        private AudioClip currentClip;
        private bool stopTask = false;
        private Task mainTask;
        private CancellationTokenSource cancellationTokenSource;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.audioSource = animator.GetComponent<AudioSource>();

            if(this.audioSource == null) {
                throw new System.Exception("You need to have an AudioSource on the same object that has the animator attached.");
            }

            this.cancellationTokenSource = new CancellationTokenSource();
            this.mainTask = StartTalking(cancellationTokenSource.Token);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
            this.StopTalking();
        }

        private void OnDestroy() {
            this.StopTalking();
        }

        private void StopTalking() {
            if (this.cancellationTokenSource != null) {
                this.cancellationTokenSource.Cancel();
            }
        }

        private async Task StartTalking(CancellationToken cancellationToken) {            
            await Task.Delay(ConvertSecToMilisec(this.startDelay));
            while (true) {
                var clipTime = this.currentClip == null ? 0 : this.currentClip.length;
                await Task.Delay(ConvertSecToMilisec(clipTime + this.linesDelay), cancellationToken);
                
                this.Talk();

                if (cancellationToken.IsCancellationRequested)
                    return;
            }
        }

        private void Talk() {
            this.currentClip = this.GetRandomClip();
            this.audioSource.PlayOneShot(currentClip);
        }

        private int ConvertSecToMilisec(float sec) {
            return (int)(sec * 1000);
        }

        private AudioClip GetRandomClip() {
            return this.clips[Random.Range(0, this.clips.Length)];
        }
    }
}
