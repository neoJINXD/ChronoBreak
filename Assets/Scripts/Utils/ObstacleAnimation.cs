using UnityEngine;

public class ObstacleAnimation : MonoBehaviour
{
    // Assignables
    [SerializeField] Vector3 point1;
    [SerializeField] Vector3 point2;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    float speed;
    float interpolant;
    bool increasing;

    void Start()
    {
        interpolant = Random.value;
        increasing = (Random.value > 0.5f);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(point1, point2, interpolant);

        if (increasing)
        {
            interpolant = interpolant + (Time.deltaTime * speed);
            if (interpolant > 1.0f)
            {
                increasing = false;
            }
        }
        else
        {
            interpolant = interpolant - (Time.deltaTime * speed);

            if (interpolant < 0.0f)
            {
                increasing = true;
            }
        }
    }
}
