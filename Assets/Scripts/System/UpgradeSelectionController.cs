using UnityEngine;
using System.Collections.Generic;
public class UpgradeSelectionController : MonoBehaviour
{
    [SerializeField] private UpgradeCard[] cards = new UpgradeCard[3];
    [SerializeField] private List<UpgradeData> pool;
    [SerializeField] private GameObject panel;
    
    private PlayerStats _playerStats;
    
    private void Awake() => panel.SetActive(false);
    
    public void OpenCardUpgradeGUI(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        
        var choices = new List<UpgradeData>(pool);

        foreach (var card in cards)
        {
            int index = Random.Range(0, choices.Count);
            card.Bind(choices[index], OnCardPicked);
            choices.RemoveAt(index);
        }
        
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    private void OnCardPicked(UpgradeData data)
    {
        _playerStats.ApplyUpgrade(data);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
