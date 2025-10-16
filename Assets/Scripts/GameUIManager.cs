using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject GamePlayScreen;
    [SerializeField] private GameObject EndGameScreen;

    [SerializeField] private TextMeshProUGUI gameoverscoreText;


    [SerializeField] private string mainMenuSceneName = "MainMenu";


    private void Start()
    {
        ShowGameSceen();
    }
    public void ShowGameSceen()
    {
        scoreText.text = "0";
        GamePlayScreen.SetActive(true);
        EndGameScreen.SetActive(false);

    }
    public void ShowGameOverScreen()
    {
        gameoverscoreText.text = scoreText.text;
        GamePlayScreen.SetActive(false);
        EndGameScreen.SetActive(true);
    }
    public void Replay()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnTimerChanged += UpdateTimerUI;
        GameManager.OnGameEnd += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
        GameManager.OnTimerChanged -= UpdateTimerUI;
        GameManager.OnGameEnd -= ShowGameOverScreen;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
    private void UpdateTimerUI(float timeLeft)
    {
        timerText.text = Mathf.Ceil(timeLeft).ToString();
    }
}
