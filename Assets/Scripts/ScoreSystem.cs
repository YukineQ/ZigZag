using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private TextMeshProUGUI _scoreTextUI;

    private int _score;

    private int _highScore;

    public TextMeshProUGUI highScoreTextUI;

    private void Awake()
    {
        _scoreTextUI = GetComponent<TextMeshProUGUI>();
        GetHighScoreFromRegister();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        _score = 0;
    }

    private void GetHighScoreFromRegister()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        SetHighScoreIntoMainMenu();
    }

    private void SetHighScoreIntoMainMenu()
    {
        highScoreTextUI.text = $"high score: " + _highScore;
    }

    private void OnEnable()
    {
        StartCoroutine(ScoreUpdate());
    }

    private void OnDisable()
    {
        StopCoroutine(ScoreUpdate());
    }

    private IEnumerator ScoreUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            _score++;

            SetScoreOnTextUI();
        }
    }

    private void SetScoreOnTextUI()
    {
        _scoreTextUI.text = _score.ToString();
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        gameObject.SetActive(newGameState == GameState.Running);

        if (newGameState == GameState.Stoped)
            SaveHighScore();
    }

    private void SaveHighScore()
    {
        if (_highScore > _score)
            return;

        PlayerPrefs.SetInt("HighScore", _score);
        PlayerPrefs.Save();
    }
}