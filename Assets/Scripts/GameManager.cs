using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        GameStateManager.Instance.SetState(GameState.Running);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.Running)
        {

            gameObject.SetActive(false);
            return;
        }
        

        ReloadLevel();
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene($"Scenes/Game");
    }
    
}