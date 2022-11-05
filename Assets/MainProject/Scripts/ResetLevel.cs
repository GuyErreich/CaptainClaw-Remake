using System.Collections.Generic;
using UnityEngine;

namespace CaptainClaw.Scripts {
    public class ResetLevel : MonoBehaviour {
        [SerializeField] private List<GameObject> objects;

        public void Activate() {
            print(this.objects.Count > 0);
            if (this.objects != null && this.objects.Count > 0)
            {
                foreach (var obj in this.objects) {
                    obj.SetActive(true);
                }
            }
        }
    }
}