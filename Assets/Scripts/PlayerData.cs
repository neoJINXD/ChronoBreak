using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string username;
    public float sensitivity;
    public float fov;

    public float[] scores;



    public PlayerData(GameManager gm)
    {
        username = gm.username;
        sensitivity = gm.sensitivity;
        fov = gm.fov;

        scores = new float[5];
        scores[0] = gm.scores["Nature1"];
        scores[1] = gm.scores["Nature2"];
        scores[2] = gm.scores["City1"];
        scores[3] = gm.scores["City2"];
        scores[4] = gm.scores["City3"];
        
    }
}