using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Assignables
    [SerializeField] Transform player;
    void Update()
    {
        transform.position = player.transform.position;
    }
}
