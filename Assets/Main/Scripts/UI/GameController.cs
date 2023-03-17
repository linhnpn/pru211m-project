using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    private Label _time;
    private Label _healthDetail;
    private Label _normalScore;
    private Label _bossScore;
    private Label _totalTime;
    private Label _normalScoreScreen;
    private Label _bossScoreScreen;
    private VisualElement _currenHealth;
    private VisualElement _pauseLayout;
    private VisualElement _loseLayout;
    private UIDocument _document;
    private Button _continueButton;
    private Button _quitButton;
    private Button _playAgainButton;
    private Button _mainMenuButton;
    private float timeRun = 0;
    private float timeupdate = 0;
    private float maxHp;
    private float currentHp;
    private float playerCurrentHp;
    private string totalTime;
    // Start is called before the first frame update
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _time = _document.rootVisualElement.Q<Label>("Time");
        _currenHealth = _document.rootVisualElement.Q<VisualElement>("CurrentHealth");
        _healthDetail = _document.rootVisualElement.Q<Label>("HealthDetail");
        _normalScore = _document.rootVisualElement.Q<Label>("ScoreNormal");
        _totalTime = _document.rootVisualElement.Q<Label>("TotalTime");
        _bossScore = _document.rootVisualElement.Q<Label>("ScoreBoss");
        _bossScoreScreen = _document.rootVisualElement.Q<Label>("BossS");
        _normalScoreScreen = _document.rootVisualElement.Q<Label>("NormalS");
        _pauseLayout = _document.rootVisualElement.Q<VisualElement>("PauseScreen");
        _continueButton = _document.rootVisualElement.Q<Button>("ContinueButton");
        _quitButton = _document.rootVisualElement.Q<Button>("QuitGameButton");
        _loseLayout = _document.rootVisualElement.Q<VisualElement>("LoseScreen");
        _playAgainButton = _document.rootVisualElement.Q<Button>("PlayAgainButton");
        _mainMenuButton = _document.rootVisualElement.Q<Button>("MainMenuButton");

        PlayerControl playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        maxHp = playerControl.maxHp;
        currentHp = maxHp;
        playerCurrentHp = maxHp;

        _currenHealth.style.width = 800;
        _healthDetail.text = $"{currentHp}/{maxHp}";
        _continueButton.clicked += ContinueButtonClick;
        _quitButton.clicked += QuitButtonOnClick;
        _playAgainButton.clicked += PlayAgainButtonClick;
        _mainMenuButton.clicked += MainMenuButtonOnClick;

        Time.timeScale = 1;
    }

    private void QuitButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp != playerCurrentHp)
        {
            currentHp = playerCurrentHp;
            _currenHealth.style.width = currentHp * 800 / maxHp;
            _healthDetail.text = $"{(Math.Round(currentHp, 0) < 0 ? 0 : Math.Round(currentHp, 0))}/{maxHp}";
        }

        PauseGame();
        SetTime();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseLayout.style.display = DisplayStyle.Flex;
            Time.timeScale = 0;
        }
    }

    private void ContinueButtonClick()
    {
        Time.timeScale = 1;
        _pauseLayout.style.display = DisplayStyle.None;
    }

    private void SetTime()
    {
        string result = "";
        timeRun += Time.deltaTime;
        int minute = (int)Math.Floor((double)timeRun / (double)60);
        int second = (int)Math.Floor((double)timeRun % (double)60);
        if (minute < 10)
        {
            result += $"0{minute} : ";
        } else
        {
            result += $"{minute} : ";
        }

        if (second < 10)
        {
            result += $"0{second}";
        }
        else
        {
            result += $"{second}";
        }
        totalTime = result;
        _time.text = result;
    }

    public void UpdateHealth(float playerCurrentHp, float playerMaxHp)
    {
        this.playerCurrentHp = playerCurrentHp;
        maxHp = playerMaxHp;
    }

    public void SetLoseScreen(int normalScore,int bossScore)
    {
        _bossScore.text = $"x{bossScore}";
        _normalScore.text = $"x{normalScore}";
        _totalTime.text = totalTime;
        _loseLayout.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
    }

    private void PlayAgainButtonClick()
    {
        Debug.Log("Play again");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateScore(int normal, int boss)
    {
        _normalScoreScreen.text = $"x{normal}";
        _bossScoreScreen.text = $"x{boss}";
    }

}
