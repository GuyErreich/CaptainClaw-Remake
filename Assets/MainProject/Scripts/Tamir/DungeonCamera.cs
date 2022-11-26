using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{   
    public GameObject Camera1;
    public GameObject Camera2;
    // Start is called before the first frame update
    void Start()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Camera2.SetActive(true);
        Camera1.SetActive(false);
    }
}
