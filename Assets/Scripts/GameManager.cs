using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
// using UnityEngine.Networking;

// using Newtonsoft.Json;


public class GameManager : Singleton<GameManager>
{
#region Inner Classes for JSON
    
    // [System.Serializable] public class Entry
    // {
    //     [SerializeField]public string name;
    //     [SerializeField]public string score;
    //     [SerializeField]public string seconds;
    //     [SerializeField]public string text;
    //     [SerializeField]public string date;
    // }

    // public class Leaderboard    
    // {
    //     public List<Entry> entry { get; set; } 
    // }

    // public class Dreamlo    
    // {
    //     public Leaderboard leaderboard { get; set; } 
    // }

    // public class Root    
    // {
    //     public Dreamlo dreamlo { get; set; } 
    // }
#endregion
    
    // Leaderboard info
#region LeaderboardAPI
    // public List<Entry> LeaderboardTop; // scores from API
    // private const string PRIVATE_KEY_NATURE = "3V-PNjW4gEypH7ldT-Ss1gGg4OfkQ0OE2ZO0CuRvcBvQ";
    // private const string PUBLIC_KEY_NATURE = "5fc70d38eb36fd27142232fb";
    // private const string PRIVATE_KEY_CITY  = "VoPTk4bDCUOD3Pl4Bo2SNAoao6H9XfFUa4Bza7y-AF4g";
    // private const string PUBLIC_KEY_CITY = "5fc72887eb36fd271422c072";
    // private const string URL = "http://dreamlo.com/lb/";
#endregion
    

    // Individual level data
    public bool gameDone; // when level is complete
    public bool hardcoreMode; // hardcore mode enabled, no weapon pickups

    // Game data - Needs to be saved
    public string username; // name to use for leaderboards // TODO scene with prompt if no save detected
    public Dictionary<string, float> scores; // Local saves of player's times
    public float sensitivity = 50f; // TODO load this into playermovement
    public float fov = 60f; // TODO load this into both cameras


    public float[] scoress;

    void Start() 
    {
        username = "UniteeeyTest";

        gameDone = false;
        hardcoreMode = false;

        //Default values
        scores = new Dictionary<string, float>();
        scores.Add("Nature1", 0);
        scores.Add("Nature2", 0);
        scores.Add("City1", 0);
        scores.Add("City2", 0);
        scores.Add("City3", 0);

        sensitivity = 50f;
        fov = 60f;
        
        PlayerData loadedData = LoadData();

        if (loadedData == null)
        {
            // save doesnt exist
            SaveData(new PlayerData(this));
            print("New Save Created"); // TODO scene to enter name
        }
        else
        {
            // save exist
            username = loadedData.username;
            sensitivity = loadedData.sensitivity;
            fov = loadedData.fov;

            scores["Nature1"] = loadedData.scores[0];
            scores["Nature2"] = loadedData.scores[1];
            scores["City1"] = loadedData.scores[2];
            scores["City2"] = loadedData.scores[3];
            scores["City3"] = loadedData.scores[4];
        }
        print(Application.persistentDataPath);

        scoress = new float[5];
    }

    public void Score(float time, string key)
    {
        if (scores.ContainsKey(key))
        {
            float currentScore = scores[key];
            if (time < currentScore || Mathf.Approximately(currentScore, 0f))
            {
                scores[key] = time;
            }
            else 
            {
                print($"this a worse score :pensive: {currentScore}");
            }
        }
        else
        {
            scores.Add(key, time);
        }

#region icantcollapsethis😔
        /*
        // TODO upload score when both nature/ 3 city scores are reveived

        if(scores.ContainsKey("Nature1") && !Mathf.Approximately(scores["Nature1"], 0f) &&
            scores.ContainsKey("Nature2") && !Mathf.Approximately(scores["Nature2"], 0f)
        )
        {
            // both nature scores exist
            float totalScore = scores["Nature1"] + scores["Nature2"];
            print($"Totalscore: {totalScore}");
        }
        else
        {
            print("Missing Nature scores");
        }

        if(scores.ContainsKey("City1") && !Mathf.Approximately(scores["City1"], 0f) &&
            scores.ContainsKey("City2") && !Mathf.Approximately(scores["City2"], 0f) &&
            scores.ContainsKey("City3") && !Mathf.Approximately(scores["City3"], 0f)
        )
        {
            // all 3 city scores exist
            float totalScore = scores["City1"] + scores["City2"] + scores["City3"];

            //AddNewScore(totalScore, false, false);
        }
        else
        {
            print("Missing City scores");
        }

       */
#endregion
    
    }

    void Update()
    {
        if (scores.ContainsKey("Test"))
            print($"My scores for Test is {scores["Test"]}");
        // foreach(var i in scores)
        // {
        //     print($"My scores for {i.Key} is {i.Value}");
        // }
        int i = 0;
        foreach (var score in scores)
        {
            scoress[i] = score.Value;
            i++;
        }
    }

    // // TODO add hardcore submision
    // public void AddNewScore(float score, bool isNature, bool isHardcore)
    // {
    //     print($"Submitting score for {username}");
    //     StartCoroutine(UploadScore(score, isNature, false));
    // }

    // public void DownloadLeaderboards(bool isNature)
    // {
    //     print($"Downloading scores are downloaded");
    //     StartCoroutine(DownloadScores(isNature, false));
    // }

    // // TODO add hardcore mode as seconds entry
    // private IEnumerator UploadScore(double score, bool isNature, bool isHardcore)
    // {
    //     string PRIVATE_KEY = isNature ? PRIVATE_KEY_NATURE : PRIVATE_KEY_CITY; // // TODO dont send if score is 0f
    //     int temp = (int)((score - 2) * 1000000000);
    //     print($"{URL}{PRIVATE_KEY}/add/{UnityWebRequest.EscapeURL(username)}/{-temp}");
    //     // string PUBLIC_KEY = isNature ? PUBLIC_KEY_NATURE : PUBLIC_KEY_CITY;
    //     using (UnityWebRequest req = UnityWebRequest.Get($"{URL}{PRIVATE_KEY}/add/{UnityWebRequest.EscapeURL(username)}/{-temp}"))
    //     {
    //         yield return req.SendWebRequest();

    //         if (req.isNetworkError)
    //         {
    //             Debug.LogError($"UPLOAD FAILED {req.error}");
    //         }
    //         else
    //         {
    //             print("Upload Success");
    //         }
    //     }
    // }

    // // TODO add hardcore mode as get 'json-seconds'
    // private IEnumerator DownloadScores(bool isNature, bool isHardcore)
    // {
    //     string PUBLIC_KEY = isNature ? PUBLIC_KEY_NATURE : PUBLIC_KEY_CITY;
    //     using (UnityWebRequest req = UnityWebRequest.Get($"{URL}{PUBLIC_KEY}/json/5"))
    //     {
    //         yield return req.SendWebRequest();

    //         if (req.isNetworkError)
    //         {
    //             Debug.LogError($"UPLOAD FAILED {req.error}");
    //         }
    //         else
    //         {
    //             print("Download Success");
    //             print(req.downloadHandler.text);

    //             var json = JsonConvert.DeserializeObject<Root>(req.downloadHandler.text);
    //             // print(groot.dreamlo.leaderboard.entry[0].name);
    //             if (json != null)
    //                 LeaderboardTop = json.dreamlo.leaderboard.entry;

    //             // TODO invert scores back to positive
    //         }

    //     }
    // }

    private void SaveData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/chronobreak_data.fuck";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    private PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/chronobreak_data.fuck";
        
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData loadedData = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return loadedData;
        }
        else
        {
            // TODO create new file with default data / ask palyer for username
            Debug.LogError("NO SAVE FILE EXISTS");
            return null;
        }

    }

    void OnDestroy() 
    {
        SaveData(new PlayerData(this));    
    }
}
