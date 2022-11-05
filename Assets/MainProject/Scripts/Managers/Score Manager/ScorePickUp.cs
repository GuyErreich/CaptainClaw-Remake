using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{

    private ScoreManager scoreManager;
    public string playerTag = "Player";
    public int scoreValue = 1;


    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (scoreManager)
            {
                scoreManager.AddToScore(scoreValue);
            }
            Destroy(this.gameObject);
        }
    }
}
