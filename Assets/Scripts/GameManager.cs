using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : Singleton<GameManager>
{

    // Game data - Needs to be saved
    private string name;


    // Individual level data
    public bool gameDone;

    private string username;


    private const string PRIVATE_KEY_NATURE = "3V-PNjW4gEypH7ldT-Ss1gGg4OfkQ0OE2ZO0CuRvcBvQ";
    private const string PUBLIC_KEY_NATURE = "5fc70d38eb36fd27142232fb";
    private const string PRIVATE_KEY_CITY  = "VoPTk4bDCUOD3Pl4Bo2SNAoao6H9XfFUa4Bza7y-AF4g";
    private const string PUBLIC_KEY_CITY = "5fc72887eb36fd271422c072";
    private const string URL = "http://dreamlo.com/lb/";

    private Dictionary<string, float> scores;

    void Start() 
    {
        // gameDone = false;
        name = "Test Player";
        scores = new Dictionary<string, float>();
        AddNewScore("UnityTest", 10021);
    }

    public void Score(float time)
    {
        scores.Add("Test", time);
    }

    void Update()
    {
        foreach(var i in scores)
        {
            print($"My scores for {i.Key} is {i.Value}");
        }
    }

    public void AddNewScore(string username, float score)
    {
        print($"Submitting score for {username}");
        StartCoroutine(UploadScore(username, score, true));
    }

    private IEnumerator UploadScore(string username, float score, bool isNature)
    {
        string PRIVATE_KEY = isNature ? PRIVATE_KEY_NATURE : PRIVATE_KEY_CITY;
        // string PUBLIC_KEY = isNature ? PUBLIC_KEY_NATURE : PUBLIC_KEY_CITY;
        using (UnityWebRequest req = UnityWebRequest.Get($"{URL}{PRIVATE_KEY}/add/{UnityWebRequest.EscapeURL(username)}/{score}"))
        {
            yield return req.SendWebRequest();

            if (req.isNetworkError)
            {
                Debug.LogError($"UPLOAD FAILED {req.error}");
            }
            else
            {
                print("Upload Success");
            }
        }

    }
}
