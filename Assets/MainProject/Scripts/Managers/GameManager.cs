using UnityEngine;
using UnityEngine.Events;


namespace CaptainClaw.Scripts.Managers {
    public class GameManager : MonoBehaviour {
        [SerializeField] private Canvas pauseMenu;

        private static GameManager _instance;
        private float lastTimeScale;
        private delegate void PauseState();
        private static PauseState _currentPauseState;

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

        public static void Pause() {
            _currentPauseState();
        }

        private void OnPause() {
            this.lastTimeScale = Time.timeScale;
            Time.timeScale = 0;
            print("fuck");
            _currentPauseState = this.OnUnpause;
        }

        private void OnUnpause() {
            Time.timeScale = this.lastTimeScale;

            _currentPauseState = this.OnPause;
        }
    }
}