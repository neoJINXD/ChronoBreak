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

    public void ResetLine()
    {
        line.positionCount = 0;
    }

    void OnDestroy() 
    {
        KillAllObstacles();    
    }

}   
