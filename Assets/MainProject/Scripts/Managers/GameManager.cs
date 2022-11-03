using UnityEngine;
using UnityEngine.Events;


namespace CaptainClaw.Scripts.Managers {
    public class GameManager : MonoBehaviour {
        private static GameManager _instance;

        private void Awake() {
            if (_instance == null) {

                _instance = this;
                DontDestroyOnLoad(this.gameObject);

                //Rest of your Awake code
            } 
            else {
                Destroy(this);
            }
        }
    }
}