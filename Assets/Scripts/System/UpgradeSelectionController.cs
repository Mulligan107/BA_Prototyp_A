using UnityEngine;
using System.Collections.Generic;
public class UpgradeSelectionController : MonoBehaviour
{
    [SerializeField] private UpgradeCard[] cards = new UpgradeCard[3]; //Karten im UI
    [SerializeField] private List<UpgradeData> pool; //Alle möglichen Upgrades
    [SerializeField] private GameObject panel; //Das panel selbst
    
    private PlayerStats _playerStats;
    
    private void Awake() => panel.SetActive(false);
    
    //Man kann das upgrade fenster mit OpenCardUpgradeGUI überall öffnen
    public void OpenCardUpgradeGUI(PlayerStats playerStats)
    {
        _playerStats = playerStats;

        //Kopie damit das selbe upgrade nicht zwei mal kommt
        var choices = new List<UpgradeData>(pool);

        foreach (var card in cards)
        {
            int index = Random.Range(0, choices.Count);
            card.Bind(choices[index], OnCardPicked);
            choices.RemoveAt(index);
        }
        
        Time.timeScale = 0f; //Spiel freeze
        panel.SetActive(true);
    }

    private void OnCardPicked(UpgradeData data)
    {
        _playerStats.ApplyUpgrade(data);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
