using UnityEngine;

public class FallZone : MonoBehaviour
{
    // Assignables
    [SerializeField] Timer timer;
    void OnTriggerEnter(Collider col) 
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerMovement>().LoadSafePos();
            timer.CountEvent("falling");
        }
    }
}   
