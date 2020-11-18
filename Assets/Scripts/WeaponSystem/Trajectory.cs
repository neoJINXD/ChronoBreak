using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trajectory : Singleton<Trajectory>
{
    [SerializeField] GameObject obstacles;
    [SerializeField] int maxIterations;
    private Scene currentScene;
    private Scene predictionScene;

    private PhysicsScene currentPhysicsScene;
    private PhysicsScene predictionPhysicsScene;

    List<GameObject> dummyObstacles = new List<GameObject>();

    private LineRenderer line;
    private GameObject dummy;

    void Start()
    {
        Physics.autoSimulation = false;
        
        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters param = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", param);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();

        line = GetComponent<LineRenderer>();
    }

    void FixedUpdate() 
    {
        if (currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }    
    }

    public void CopyAllObstacles()
    {
        foreach(Transform t in obstacles.transform)
        {
            if(t.gameObject.GetComponent<Collider>() != null)
            {
                GameObject fakeT = Instantiate(t.gameObject);
                fakeT.transform.position = t.position;
                fakeT.transform.rotation = t.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if(fakeR){
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                dummyObstacles.Add(fakeT);
            }
        }
    }

    void KillAllObstacles()
    {
        foreach(var o in dummyObstacles)
        {
            Destroy(o);
        }
        dummyObstacles.Clear();
    }

    public void Predict(GameObject subject, Vector3 currentPosition, Vector3 force)
    {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if(dummy == null)
            {
                dummy = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }
            // print($"Prediction time for {dummy.name} created from {subject.name}\n at {currentPosition}");
            // Instantiate(subject, currentPosition, Quaternion.identity);

            dummy.transform.position = currentPosition;
            dummy.GetComponent<Rigidbody>().isKinematic = false;
            dummy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            line.positionCount = 0;
            line.positionCount = maxIterations;

            // line.sortingOrder = 1;
            // line.material = new Material (Shader.Find ("Sprites/Default"));
            // line.material.color = Color.red; 

            for (int i = 0; i < maxIterations; i++)
            {
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                line.SetPosition(i, dummy.transform.position);
            }

            Destroy(dummy);
        }
    }

    void OnDestroy() 
    {
        KillAllObstacles();    
    }


    /*
    [SerializeField] LineRenderer line;
    [SerializeField] int res;

    [SerializeField] Vector3 velocity;
    [SerializeField] float Limit;
    [SerializeField] float g;
    
    void Start()
    {
        g = Mathf.Abs(Physics.gravity.y);
    }
    
    void Update()
    {
        StartCoroutine(RenderArc());
    }

    private IEnumerator RenderArc()
    {
        line.positionCount = res + 1;
        line.SetPositions(CalculateLineArray());
        yield return null;
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[res + 1];

        for (int i = 0; i < lineArray.Length; i++)
        {
            float t = i / lineArray.Length;
            lineArray[i] = CalculateLinePoint(t);
        }

        return lineArray;
    }

    private Vector3 CalculateLinePoint(float t)
    {
        // float x = velocity.x * t;
        float x = FindHorizontal(velocity.x, velocity.z) * t;
        float y = (velocity.y * t) - (g * t * t) / 2;
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float ProjectileMotion1(float x0, float vx, float t)
    {
        return x0 + (vx * t);
    }

    private float ProjectileMotion2(float y0, float vy, float t)
    {
        return y0 + (vy * t) - (g * t * t)/2;
    }

    private float FindHorizontal(float x, float z)
    {
        return Mathf.Sqrt(x * x + z * z);
    }
    */
}   
