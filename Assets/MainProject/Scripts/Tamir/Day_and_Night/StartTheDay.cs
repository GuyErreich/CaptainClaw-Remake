using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTheDay : MonoBehaviour
{
    [SerializeField] private GameObject day_and_Night_Manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            day_and_Night_Manager.SetActive(true);
        }
    }
}
