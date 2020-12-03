using UnityEngine;

public class ObstacleAnimation : MonoBehaviour
{
    // Assignables
    [SerializeField]
    Vector3 point1;

    [SerializeField]
    Vector3 point2;

    [SerializeField]
    float minSpeed;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float speed;

    float t;
    bool increasing;

    void Start()
    {
        t = Random.value;
        increasing = Random.value > 0.5f;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(point1, point2, t);

        if (increasing)
        {
            t += Time.deltaTime * speed;
            if (t > 1.0f)
            {
                increasing = false;
            }
        }
        else
        {
            t -= Time.deltaTime * speed;
            if (t < 0.0f)
            {
                increasing = true;
            }
        }
    }
}
