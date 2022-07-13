using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;

    [System.Serializable]
    private class Score
    {
        public string playerName = "System";
        public int score = 0;
    }

    private string highScoreFilename = "high_score";
    private Score currentHighScore;
    public string currentPlayerName;

    public TextMeshProUGUI highScoreText;

    private MainManager mainManager;

    // Start is called before the first frame update
    void Awake()
    {
        // This is a singleton class -- we are only allowed a single instance
        // of HighScore. If we are trying to create a new instance, we should
        // destroy it.
        Debug.Log("Creating HighScore");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currentHighScore = LoadHighScore();
            UpdateHighScoreUI();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /*
     * Check if we have a new high score.
     */
    public void CheckHighScore(int challengeScore)
    {
        Debug.Log("Checking high score!");
        if (challengeScore > currentHighScore.score)
        {
            Debug.Log(" --> New high score!");
            Score newHighScore = new Score();
            newHighScore.playerName = currentPlayerName;
            newHighScore.score = challengeScore;
            currentHighScore = newHighScore;
            UpdateHighScoreUI();
        }
    }

    public void UpdateHighScoreUI()
    {
        Debug.Log("Updating UI element!");
        highScoreText.SetText("Hi-Score: " + currentHighScore.score + " (" + currentHighScore.playerName + ")");
    }

    /*
     * Save the current high score to disk. This is called from MainManager on
     * game over.
     */
    public void SaveHighScore()
    {
        Debug.Log("Saving high score to disk!");
        Score data = currentHighScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + highScoreFilename + ".json", json);
    }

    /*
     * Load the last saved high score. This is called on Awake().
     */
    private Score LoadHighScore()
    {
        Debug.Log("Loading high score from disk!");
        string path = Application.persistentDataPath + "/" + highScoreFilename + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<Score>(json);
        }
        else
        {
            Score blankHighScore = new Score();
            return blankHighScore;
        }
    }

    public void ResetHighScore()
    {
        Debug.Log("Resetting current hi-score!");
        currentHighScore = new Score();
        UpdateHighScoreUI();
        SaveHighScore();
    }
}
