using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float smoothnessOfFollowing;

    public Transform player;

    private Vector3 _distanceToPlayer;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        _distanceToPlayer = player.position - transform.position;
    }
    
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 currentCameraPosition = transform.position;
        Vector3 targetCameraPosition = player.position - _distanceToPlayer;

        transform.position = Vector3.Lerp(currentCameraPosition, targetCameraPosition, smoothnessOfFollowing * Time.deltaTime);
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Running;
    }
}
