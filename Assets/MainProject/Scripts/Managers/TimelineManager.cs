
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private float timeStamp;

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.GetComponent<PlayableDirector>().time = this.timeStamp;
            this.enabled = false;
        }
    }
}
