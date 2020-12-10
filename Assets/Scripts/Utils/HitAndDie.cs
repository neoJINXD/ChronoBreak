using UnityEngine;
using UnityEngine.SceneManagement;

public class HitAndDie : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    [SerializeField] float force = 20;
    [SerializeField] float timerEnd = 0.5f;
    [SerializeField] GameObject deathUI;
    Rigidbody playerRB;
    bool hit = false;
    float timer = 0;
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            timer = timer + Time.deltaTime;
            deathUI.SetActive(true);
            //Debug.Log(timer);
        }

        if(timer > timerEnd)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            hit = true;
            Vector3 dir = (player.transform.position - transform.position).normalized;
            playerRB.AddForce(dir * force, ForceMode.Impulse);
            Time.timeScale = 0.05f;
        }
    }
}
