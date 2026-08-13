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

    private void OnEnable()
    {
        EnemyStats.Died += AddPoints;
    }

    private void OnDisable()
    {
        EnemyStats.Died -= AddPoints;
    }

    public void AddPoints(int points)
    {
        _points += points;
        UpdateLabel();
    }

    private void Start()
    {
        if (label == null)
        {
            enabled = false;
            return;
        }

        UpdateLabel();
    }

    private void Update()
    {
        _accumulator += _pointsPerSecond * Time.deltaTime;
        
        int whole = Mathf.FloorToInt(_accumulator);
        if (whole <= 0) return;
        
        _accumulator -= whole;
        _points += whole;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        label.text = string.Format(format, _points);
    }
}
