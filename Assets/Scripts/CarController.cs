using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed;

    private bool _firstClick = true;


    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    private void Update()
    {
        Move();
        ChangeDirection();

        if (transform.position.y <= -2)
        {
            GameStateManager.Instance.SetState(GameState.Stoped);
        }
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Move()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        if (IsFirstClick())
        {
            _firstClick = false;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            MovingLeft();
        }
        else
        {
            MovingRight();
        }
    }

    private bool IsFirstClick()
    {
        return _firstClick;
    }

    private void MovingRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MovingLeft()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Running;
    }
}