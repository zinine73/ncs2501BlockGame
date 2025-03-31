using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputField;
    private const string HIGHSCORE_KEY = "HIGHSCORE";
    private const string BESTPLAYER_KEY = "BESTPLAYER";
    private string name;
    private int highScore;
    private string bestPlayer;
    public string BestPlayer
    {
        get { return bestPlayer; }
    }
    public string Name
    {
        get { return name; }
        set
        {
            if (value.Length == 0)
            {
                name = "NoName";
            }
        }        
    }

    public int HighScore
    {
        get 
        {
            highScore = PlayerPrefs.GetInt("HIGHSCORE_KEY", 0);
            bestPlayer = PlayerPrefs.GetString("BESTPLAYER", "NoName"); 
            return highScore; 
        }
        set
        {
            if (highScore <= value)
            {
                highScore = value;
                PlayerPrefs.SetInt("HIGHSCORE_KEY", highScore);
                PlayerPrefs.SetString("BESTPLAYER", bestPlayer);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        highScore = HighScore;
        bestScoreText.text = $"{highScore} : {bestPlayer}";
    }

    public void UpdateName()
    {
        name = inputField.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
