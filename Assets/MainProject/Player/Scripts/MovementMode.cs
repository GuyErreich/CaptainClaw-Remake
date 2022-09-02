using UnityEngine;
using CaptainClaw.Scripts.FSM;
using System.Threading.Tasks;

namespace CaptainClaw.Player.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class MovementMode : State {
        [Header("Stats")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1.1f, 5f)] private float sprintMultiplier = 1.5f;

        protected MovementMode(StateMachine stateMachine) : base(stateMachine) {}

        public override async Task Start()
        {
            var state = await Task.Run(Do());
            stateMachine.SetState(state);
        }

        protected State Do()
        {
            return this;
        }
    }
}