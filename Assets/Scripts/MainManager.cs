using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    private int highestScore = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        loadHighScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if(m_Points > highestScore)
        {
            HighScoreText.text = "Best Score: " + NameManager.playerName + " : " + m_Points; 
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > highestScore)
            UpdateHighScore();
    }

    [System.Serializable]
    class HighScore
    {
        public int highScore;
        public string name;
    }

    
    public void UpdateHighScore()
    {
        HighScore hiScore = new HighScore();
        hiScore.highScore = m_Points;
        hiScore.name = NameManager.playerName;

        string json = JsonUtility.ToJson(hiScore);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void loadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore hiscore = JsonUtility.FromJson<HighScore>(json);

            highestScore = hiscore.highScore;
            HighScoreText.text = "Best Score: " + hiscore.name + " : " + hiscore.highScore;
        }
    }

}
