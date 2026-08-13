using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private string format = "Amount of Points: {0}";
    
    private float _pointsPerSecond = 1f;
    private int _points = 0;
    private float _accumulator;
    
    public int Points => _points;
    private static bool IsGameOver => GameManager.Instance != null && GameManager.Instance.IsGameOver;

    private void OnEnable()
    {
        EnemyStats.Died += AddPoints;
    }

    private void OnDisable()
    {
        EnemyStats.Died -= AddPoints;
    }

    private void Start()
    {
        if (label == null)
        {
            Debug.LogWarning("Kein Label zugewiesen", this);
        }

        UpdateLabel();
    }
    
    public void AddPoints(int points)
    {
        if (IsGameOver) return;
        
        _points += points;
        UpdateLabel();
    }

    private void Update()
    {
        if (IsGameOver) return;
        
        _accumulator += _pointsPerSecond * Time.deltaTime;
        
        int whole = Mathf.FloorToInt(_accumulator);
        if (whole <= 0) return;
        
        _accumulator -= whole;
        _points += whole;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (label == null) return;
        label.text = string.Format(format, _points);
    }
}
