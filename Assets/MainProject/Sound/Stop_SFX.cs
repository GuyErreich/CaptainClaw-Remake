using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_SFX : MonoBehaviour
{
    [SerializeField] private AudioSource StopSound;

    private void OnTriggerEnter(Collider other)
    {
    
        StopSound.Stop();
    }
    
}
