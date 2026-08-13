using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] private Text timeTextLabel;
    [SerializeField] private float duration = 300;
    
    private float _timeLeft;
    private bool _finished;
    
    public float TimeLeft => _timeLeft;

    private void Awake()
    {
        if (timeTextLabel == null)
        {
            timeTextLabel =  GetComponent<Text>();
        }
        
        _timeLeft = duration;
        UpdateLabel();
    }
    
    void Update()
    {
        if (_finished) return;
        
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0f)
        {
            _timeLeft = 0f;
            _finished = true;
            UpdateLabel();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame(GameManager.EndReason.TimeUp);
            }
            
            return;
        }

        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (timeTextLabel == null) return;

        int total = (int)_timeLeft;
        timeTextLabel.text = total.ToString();
    }
}
