using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowShip : MonoBehaviour
{
    public GameObject shipCamera;
    public GameObject shipCameraTrigger;

    private void OnTriggerEnter(Collider other)
    {
        shipCamera.SetActive(true);
        StartCoroutine(DisableShipCamera());
    }

   
    private IEnumerator DisableShipCamera()
    {
        yield return new WaitForSeconds(6);
        shipCamera.SetActive(false);
        shipCameraTrigger.GetComponent<BoxCollider>().enabled = false;
    }
}
