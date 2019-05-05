using UnityEngine;
using Vector3 = cyclone.Vector3;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 cameraOffset;
    
    void Start()
    {
        cameraOffset = new Vector3
        {
            x = transform.position.x - player.transform.position.x,
            y = transform.position.y - player.transform.position.y,
            z = transform.position.z - player.transform.position.z
        };
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraOffset.CycloneToUnity();
    }
}