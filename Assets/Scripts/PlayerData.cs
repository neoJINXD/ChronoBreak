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

        scores = new float[10];
        scores[0] = gm.scores["Nature1"];
        scores[1] = gm.scores["Nature2"];
        scores[2] = gm.scores["City1"];
        scores[3] = gm.scores["City2"];
        scores[4] = gm.scores["City3"];
        scores[5] = gm.scores["HCNature1"];
        scores[6] = gm.scores["HCNature2"];
        scores[7] = gm.scores["HCCity1"];
        scores[8] = gm.scores["HCCity2"];
        scores[9] = gm.scores["HCCity3"];
        
    }
}