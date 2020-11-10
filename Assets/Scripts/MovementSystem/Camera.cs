using UnityEngine;

public class Camera : MonoBehaviour
{
    // Assignables
    [SerializeField] Transform player;
    void Update()
    {
        transform.position = player.transform.position;
    }
}
