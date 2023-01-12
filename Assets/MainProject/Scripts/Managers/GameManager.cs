using UnityEngine;
using UnityEngine.Events;
using CaptainClaw.Scripts.Player;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CaptainClaw.Scripts.Managers
{
    public class GameManager : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Header("References")]
        [SerializeField] private Transform startingPoint;
        [SerializeField] private Transform player;
        [SerializeField] Day_and_Night_Manager TimeDayManger;

        [Header("Game Setting")]
        [SerializeField] private int numberOfLives = 3;
        [SerializeField] private int numberOfCoinsToWin = 100;

        [Header("Pause Events"), Space()]
        [SerializeField] private UnityEvent onPause;
        [SerializeField] private UnityEvent onUnpause;

        [Header("End Game Events"), Space()]
        [SerializeField] private UnityEvent onLose;
        [SerializeField] private UnityEvent onNotEnoughCoins;
        [SerializeField] private UnityEvent onWin;
        [SerializeField] private UnityEvent onStart;


        private static GameManager _instance;
        private float lastTimeScale;

        public static Vector3 SpawnPosition { get; set; }
        public static int NumberOfLives { get; private set; }

        private delegate void PauseState();
        private PauseState currentPauseState;
        public delegate void ResetLevel();
        public static ResetLevel Reset { get; set; }

        public bool ReachedEndGame = true;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                //DontDestroyOnLoad(this.gameObject);

            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            onStart.Invoke();
            this.currentPauseState = this.OnPause;
        }

        private void Update()
        {
            this.HealthSupervisor();
            this.TimeSupervisor();
        }

        public static void Pause()
        {
            _instance.currentPauseState();
        }

        private void OnPause()
        {
            this.lastTimeScale = Time.timeScale;
            Time.timeScale = 0;

            onPause.Invoke();

            this.currentPauseState = this.OnUnpause;

            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Confined;
            //to hide the curser
            Cursor.visible = true;
        }

        private void OnUnpause()
        {
            Time.timeScale = this.lastTimeScale;

            onUnpause.Invoke();

            this.currentPauseState = this.OnPause;
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;
        }

        private void TimeSupervisor()
        {
            if (TimeDayManger.currentTimeOfDay > 30)
            Debug.Log("Start Timer");
            if (TimeDayManger.currentTimeOfDay <= 0 )
            {
                _instance.currentPauseState = () => { return; };
                //to lock in the centre of window
                Cursor.lockState = CursorLockMode.Confined;
                //to hide the curser
                Cursor.visible = true;
                onLose.Invoke();
            }
        }

        private void HealthSupervisor()
        {
            if (NumberOfLives <= 0)
            {
                this.player.position = SpawnPosition;
                Reset();
            }
            if (PlayerStats.CurrentHealth <= 0)
            {
                StartCoroutine(this.OnDeath());
            }
        }

        private IEnumerator OnDeath()
        {
            Debug.LogError("Death event");
            Mathf.Clamp(this.numberOfLives, 0, Mathf.Infinity);
            PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
            this.player.position = SpawnPosition;
            yield return new WaitForSeconds(0.3f);
            Reset();
        }

        public static void EndGame()
        {

            Debug.LogError("On EndGame event");
            _instance.currentPauseState = () => { return; };
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Confined;
            //to hide the curser
            Cursor.visible = true;
            if (ScoreManager.GetCurrentScore() <= _instance.numberOfCoinsToWin)
            {
                _instance.onNotEnoughCoins.Invoke();
            }
            else
            {
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