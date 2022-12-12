using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerSFX : MonoBehaviour
{
    public AudioSource playsound;
    public GameObject textForSound;
    private int hasbeenplayed = 0;
     void OnTriggerEnter(Collider other)
    {
        if (!playsound.isPlaying)
        {
            playsound.Play();
            textForSound.SetActive(true);
        }
        if (playsound.isPlaying)
        {
            hasbeenplayed++;
        }
    }
    private void Update()
    {
        if(!playsound.isPlaying && hasbeenplayed >= 1)
        {
            Destroy(this.gameObject);
            textForSound.SetActive(false);
        }
    }
}
