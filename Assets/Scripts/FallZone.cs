using UnityEngine;

public class FallZone : MonoBehaviour
{
    // Assignables
    [SerializeField] Timer timer;
    void OnTriggerEnter(Collider col) 
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerMovement>().LoadSafePos(); // // TODO keep track of this in a game manager
            timer.CountEvent("falling");
            AudioManager.instance.Play("DeathSound");
        }
    }
}   
