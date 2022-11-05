using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CaptainClaw.Scripts.Player;
using DG.Tweening;

namespace CaptainClaw.Scripts.UI {
    [RequireComponent(typeof(Image))]
    public class ChangeHealthFillAmount : MonoBehaviour
    {
        [SerializeField] float fillSpeed = 0.5f;
        private Image image;

        
        private float maxFill, currentFill;

        private void Awake() {
            this.image =  GetComponent<Image>();
        }

        private void Update() {
            var fillAmount = PlayerStats.CurrentHealth / PlayerStats.MaxHealth;
            this.image.DOFillAmount(fillAmount, this.fillSpeed * Time.deltaTime);
        }
    }
}
