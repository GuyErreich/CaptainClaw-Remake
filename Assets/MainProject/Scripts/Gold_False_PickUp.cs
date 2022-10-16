using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_False_PickUp : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Destroy(this.gameObject);
        }
    }
}
