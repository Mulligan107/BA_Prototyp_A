using UnityEngine;
using System.Collections.Generic;
public class UpgradeSelectionController : MonoBehaviour
{
    [SerializeField] private UpgradeCard[] cards = new UpgradeCard[3];
    [SerializeField] private List<UpgradeData> pool = new List<UpgradeData>();
    [SerializeField] private GameObject panel;
    
    private PlayerStats _playerStats;
    
    private void Awake() => panel.SetActive(false);
    
    public void Open(PlayerStats playerStats)
    {
        _playerStats = playerStats;

        var choices = PickRandom(pool, cards.Length);
        for (int i = 0; i < cards.Length; i++)
        {
            bool hasChoice = i < choices.Count;
            cards[i].gameObject.SetActive(hasChoice);
            if (hasChoice) cards[i].Bind(choices[i], OnPicked);
        }
        
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    private void OnPicked(UpgradeData data)
    {
        _playerStats.ApplyUpgrade(data);
        pool.Remove(data);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    private static List<UpgradeData> PickRandom(List<UpgradeData> source, int count)
    {
        var copy = new List<UpgradeData>(source);
        var result = new List<UpgradeData>();
        count = Mathf.Min(count, copy.Count);

        for (int i = 0; i < count; i++)
        {
            int idx = Random.Range(0, copy.Count);
            result.Add(copy[idx]);
            copy.RemoveAt(idx);
        }
        return result;
    }
}
