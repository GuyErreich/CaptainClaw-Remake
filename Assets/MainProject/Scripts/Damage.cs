using UnityEngine;


namespace CaptainClaw.Scripts {
    public class Damage : MonoBehaviour {
            [SerializeField] private float amount;

            public float Amount { get => this.amount; }
    }
}