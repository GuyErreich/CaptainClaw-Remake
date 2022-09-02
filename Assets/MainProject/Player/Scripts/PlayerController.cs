using UnityEngine;
using CaptainClaw.Scripts.FSM;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CaptainClaw.Player.Scripts
{
    public class PlayerController : StateMachine {
        public enum StateModes {
            OnGround = 0,
            Jumping = 1,
            ClimbLadder = 2
        }

        [SerializeField] private StateModes InitState;
        
        private State[] States;
        private void Awake() {
            var arraySize = Enum.GetValues(typeof(StateModes)).Cast<StateModes>().Last();
            this.States = new State[(int)arraySize];

            this.States[(int)StateModes.OnGround] = this.GetComponent<MovementMode>();
            this.States[(int)StateModes.Jumping] = this.GetComponent<InAirMode>();
            this.States[(int)StateModes.ClimbLadder] = this.GetComponent<MovementMode>();
        }

        private void Start()
        {
            this.SetState(this.States[(int)InitState]);
        }
    }
}