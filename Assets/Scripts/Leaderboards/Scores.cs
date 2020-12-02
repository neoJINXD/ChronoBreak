using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// TODO should be singleton
public class Scores : MonoBehaviour
{
    private const string PRIVATE_KEY = "3V-PNjW4gEypH7ldT-Ss1gGg4OfkQ0OE2ZO0CuRvcBvQ";
    private const string PUBLIC_KEY = "5fc70d38eb36fd27142232fb";
    private const string URL = "http://dreamlo.com/lb/";

    void Awake()
    {
        AddNewScore("UnityTest", 10001);
    }

    public void AddNewScore(string username, float score)
    {
        print($"Submitting score for {username}");
        StartCoroutine(UploadScore(username, score));
    }


    private IEnumerator UploadScore(string username, float score)
    {

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
