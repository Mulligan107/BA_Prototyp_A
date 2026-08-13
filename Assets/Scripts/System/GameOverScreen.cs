using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text titleText;
    [SerializeField] private Text scoreText;
    [SerializeField] private PointSystem pointSystem;
    
    [Header("Texts")]
    [SerializeField] private string deathTitle = "Game Over";
    [SerializeField] private string timeUpTitle = "Time Up";
    [SerializeField] private string scoreFormat = "Achieved Points: {0}";

    public void Show(GameManager.EndReason reason)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling(); //oberstes hud

        if (titleText != null)
        {
            titleText.text = reason == GameManager.EndReason.PlayerDied ?  deathTitle : timeUpTitle;
        }

        if (scoreText != null)
            scoreText.text = string.Format(scoreFormat, pointSystem != null ? pointSystem.Points : 0);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
