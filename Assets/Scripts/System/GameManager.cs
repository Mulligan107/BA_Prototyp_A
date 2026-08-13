using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum EndReason
    {
        PlayerDied,
        TimeUp
    }
    
    public static GameManager Instance { get; private set; }
    
    public static event Action<EndReason> GameEnded;
    
    [SerializeField] private GameOverScreen  gameOverScreen;
    [SerializeField] private bool freezeTimeOnGameOver;
    
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        IsGameOver = false;
    }

    private void OnEnable()
    {
        PlayerStats.Died += HandlePlayerDied;
    }

    private void OnDisable()
    {
        PlayerStats.Died -= HandlePlayerDied;

        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void HandlePlayerDied()
    {
        EndGame(EndReason.PlayerDied);
    }

    public void EndGame(EndReason endReason)
    {
        if (IsGameOver) return;
        IsGameOver = true;

        if (freezeTimeOnGameOver)
        {
            Time.timeScale = 0;
        }
        
        GameEnded?.Invoke(endReason);

        if (gameOverScreen != null)
        {
            gameOverScreen.Show(endReason);
        }
        else
        {
            Debug.LogWarning("Keine GameOverScreen ref", this);
        }
    }
}
