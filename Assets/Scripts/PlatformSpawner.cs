using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;

    private Vector3 _startSpawnPosition;
    private Vector3 _currentSpawnPosition;
    private Vector3 _nextSpawnPosition;

    private void Awake()
    {
        _startSpawnPosition = this.transform.position;
        _currentSpawnPosition = _startSpawnPosition;

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private IEnumerator SpawnNextPlatform()
    {
        while (true)
        {
            Instantiate(platformPrefab, GetNextSpawnPosition(), Quaternion.identity);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private Vector3 GetNextSpawnPosition()
    {
        _nextSpawnPosition = _currentSpawnPosition + RandomizeNextSpawnPosition();
        _currentSpawnPosition = _nextSpawnPosition;
        return _nextSpawnPosition;
    }

    private Vector3 RandomizeNextSpawnPosition()
    {
        int random = Random.Range(0, 2);

        return random == 0 ? new Vector3(2, 0, 0) : new Vector3(0, 0, 2);
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Running;
        StartCoroutine(SpawnNextPlatform());
    }
}
