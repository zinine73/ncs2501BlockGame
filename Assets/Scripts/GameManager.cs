using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputField;
    const string BESTSCORE_KEY = "BESTSCORE";
    const string BESTPLAYER_KEY = "BESTPLAYER";
    const string NONAME = "NoName";
    string userName;

    // Exepression Bodied Member
    public string UserName
    {
        get => userName;
        set => userName = (value.Length == 0) ? NONAME : value;
    }
    public string BestPlayer
    {
        get => PlayerPrefs.GetString(BESTPLAYER_KEY, NONAME);
        set => PlayerPrefs.SetString(BESTPLAYER_KEY, value);
    }
    public int BestScore
    {
        get => PlayerPrefs.GetInt(BESTSCORE_KEY, 0);
        set
        {
            if (BestScore <= value)
            {
                PlayerPrefs.SetInt(BESTSCORE_KEY, value);
                BestPlayer = userName;
            }
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() => bestScoreText.text = GetBestText();
    public string GetBestText() => $"Best Player : {BestPlayer} : {BestScore}";
    public void UpdateName() => userName = inputField.text;
    public void ExitGame() => Application.Quit();
    public void StartGame()
    {
        UserName = inputField.text;
        SceneManager.LoadScene("main");
    }

    [MenuItem("BlockGame/Reset score")]
    public static void ResetBestScore()
    {
        PlayerPrefs.SetInt(BESTSCORE_KEY, 0);
        PlayerPrefs.SetString(BESTPLAYER_KEY, NONAME);
    }
}
