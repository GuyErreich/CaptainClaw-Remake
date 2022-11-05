using UnityEngine;
using TMPro;
using CaptainClaw.Scripts.Managers;

namespace CaptainClaw.Scripts.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ChangeNumberOfLives : MonoBehaviour
    {
        private TextMeshProUGUI TMP;

        private void Awake() {
            TMP = GetComponent<TextMeshProUGUI>();
        }

        private void Update() {
            TMP.text = GameManager.NumberOfLives.ToString();
        }
    }
}
