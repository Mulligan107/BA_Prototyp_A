using Unity.VisualScripting;
using UnityEngine;


//erscheint im inspector als dropdown-menu
public enum UpgradableStat
{
    MaxHealth, MoveSpeed, BulletSpeed, BulletDamage, BulletSize
}

//reiner datencontainer
[CreateAssetMenu(fileName = "Upgrade_", menuName = "Game/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string title;
    public string description;
    public UpgradableStat stat;
    public float amount;
    
    [Header("Tooltip")]
    [Tooltip("Leer")]
    [TextArea] public string tooltipOverride;

    public string GetTooltip()
    {
        if (!string.IsNullOrWhiteSpace(tooltipOverride))
            return tooltipOverride;
        
        string sign = amount >= 0f ? "+" : "-";
        return $"{GetDisplayName(stat)}\n{sign}{Mathf.Abs(amount):0.##}{GetUnit(stat)}";
    }

    private static string GetDisplayName(UpgradableStat stat)
    {
        switch (stat)
        {
            case UpgradableStat.MaxHealth: return "Max Health";
            case UpgradableStat.MoveSpeed: return "Move Speed";
            case UpgradableStat.BulletSpeed: return "Bullet Speed";
            case UpgradableStat.BulletDamage: return "Bullet Damage";
            case UpgradableStat.BulletSize: return "Bullet Size";
            default: return stat.ToString();
        }
    }

    private static string GetUnit(UpgradableStat stat)
    {
        return stat == UpgradableStat.BulletSize ? "x" : "";
    }
}
