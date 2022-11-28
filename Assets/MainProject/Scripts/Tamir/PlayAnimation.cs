using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private Animator somenamewewillneveruseagain;
    private void OnTriggerEnter(Collider other)
    {
        somenamewewillneveruseagain.Play("TrapDoor");
    }
}
