using UnityEngine;
using UnityEngine.Events;
using CaptainClaw.Scripts.Player;
using UnityEngine.SceneManagement;

namespace CaptainClaw.Scripts.Managers {
    public class GameManager : MonoBehaviour, ISerializationCallbackReceiver {
        [Header("References")]
        [SerializeField] private Transform startingPoint;
        [SerializeField] private Transform player;

        [Header("Game Setting")]
        [SerializeField] private float amountOfTime = 3f;
        [SerializeField] private int numberOfLives = 3;
        [SerializeField] private int numberOfCoinsToWin = 100;

        [Header("Pause Events"), Space()]
        [SerializeField] private UnityEvent onPause;
        [SerializeField] private UnityEvent onUnpause;

        [Header("End Game Events"), Space()]
        [SerializeField] private UnityEvent onLose;
        [SerializeField] private UnityEvent onNotEnoughCoins;
        [SerializeField] private UnityEvent onWin;

        private static GameManager _instance;
        private float lastTimeScale;

        public static Vector3 SpawnPosition { get; set;}
        public static int NumberOfLives { get; private set;}

        private delegate void PauseState();
        private PauseState currentPauseState;
        public delegate void ResetLevel();
        public static ResetLevel Reset { get; set;}

        private void Awake() {
            if (_instance == null) {

                _instance = this;
                DontDestroyOnLoad(this.gameObject);

                //Rest of your Awake code
                this.currentPauseState = this.OnPause;
            } 
            else {
                Destroy(this);
            }
        }

        private void Update() {
            this.HealthSupervisor();
        }

        public static void Pause() {
            _instance.currentPauseState();
        }

        private void OnPause() {
            this.lastTimeScale = Time.timeScale;
            Time.timeScale = 0;

            onPause.Invoke();

            this.currentPauseState = this.OnUnpause;
        }

        private void OnUnpause() {
            Time.timeScale = this.lastTimeScale;

            onUnpause.Invoke();

            this.currentPauseState = this.OnPause;
        }

        private void TimeSupervisor() {
            if (this.amountOfTime <= 0) {
                onLose.Invoke();
            }
        }

        private void HealthSupervisor() {
            if (NumberOfLives <= 0) {
                this.player.position = SpawnPosition;
                Reset();
            }
            if (PlayerStats.CurrentHealth <= 0) {
                Mathf.Clamp(this.numberOfLives, 0, Mathf.Infinity);
                PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
                Reset();
                this.player.position = SpawnPosition;
            }
        }

        public static void EndGame() {
            if (ScoreManager.GetCurrentScore() <= _instance.numberOfCoinsToWin) {
                _instance.onNotEnoughCoins.Invoke();
            }
            else {
                _instance.onWin.Invoke();
            }
        }

        public void OnBeforeSerialize()
        {
            return;
        }

        public void OnAfterDeserialize()
        {
            NumberOfLives = this.numberOfLives;
        }
    }
}