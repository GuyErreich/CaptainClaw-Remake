using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] Animator trapDoorAnimator;

    private void Start()
    {
        this.trapDoorAnimator.enabled = false;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.trapDoorAnimator.enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.trapDoorAnimator.SetTrigger("OpenDoor");
        }
    }
}
