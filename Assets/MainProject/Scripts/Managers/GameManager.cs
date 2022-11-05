using UnityEngine;
using CaptainClaw.Scripts.Player;
using UnityEngine.SceneManagement;

namespace CaptainClaw.Scripts.Managers {
    public class GameManager : MonoBehaviour, ISerializationCallbackReceiver {
        [Header("References")]
        [SerializeField] private Transform startingPoint;
        [SerializeField] private Transform player;
        [SerializeField] private Canvas pauseMenu;

        [Header("Game Setting")]
        [SerializeField] private int numberOfLives = 3;

        private static GameManager _instance;
        private float lastTimeScale;
        private delegate void PauseState();
        private static PauseState _currentPauseState;

        public static int NumberOfLives { get; private set;}
        public static Vector3 SpawnPosition { get; set;}

        public delegate void ResetLevel();
        public static ResetLevel Reset { get; set;}

        private void Awake() {
            if (_instance == null) {

                _instance = this;
                DontDestroyOnLoad(this.gameObject);

                //Rest of your Awake code
                _currentPauseState = this.OnPause;
            } 
            else {
                Destroy(this);
            }
        }

        private void Update() {
            this.HealthSupervisor();
        }

        public static void Pause() {
            _currentPauseState();
        }

        private void OnPause() {
            this.lastTimeScale = Time.timeScale;
            Time.timeScale = 0;
            this.pauseMenu.gameObject.SetActive(true);

            _currentPauseState = this.OnUnpause;
        }

        private void OnUnpause() {
            Time.timeScale = this.lastTimeScale;
            this.pauseMenu.gameObject.SetActive(false);

            _currentPauseState = this.OnPause;
        }

        private void HealthSupervisor() {
            if (NumberOfLives <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (PlayerStats.CurrentHealth <= 0) {
                NumberOfLives--;
                PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
                Reset();
                this.player.position = SpawnPosition;
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