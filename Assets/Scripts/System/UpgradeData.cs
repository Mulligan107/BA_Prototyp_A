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
}
