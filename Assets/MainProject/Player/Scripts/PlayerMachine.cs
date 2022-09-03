using UnityEngine;
using CaptainClaw.Scripts.FSM;
using System;
using System.Linq;

namespace CaptainClaw.Player.Scripts
{
    [RequireComponent(typeof(PlayerInputManager))]
    [RequireComponent(typeof(MovementMode))]
    [RequireComponent(typeof(InAirMode))]
    public class PlayerMachine : StateMachine {
        public enum StateModes {
            OnGround = 0,
            Jumping = 1
        }

        [SerializeField] private StateModes InitState;
        
        public State[] States { get; private set; }

        private void Awake() {
            var arraySize = Enum.GetValues(typeof(StateModes)).Cast<StateModes>().Last() + 1;
            this.States = new State[(int)arraySize];

            this.States[(int)StateModes.OnGround] = this.GetComponent<MovementMode>();
            this.States[(int)StateModes.Jumping] = this.GetComponent<InAirMode>();
            // this.States[(int)StateModes.ClimbLadder] = this.GetComponent<MovementMode>();
        }

        private void Start()
        {
            this.SetState(this.States[(int)InitState]);
        }
    }
}