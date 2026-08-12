using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //SpielerStats an einem GameObject, dem Spieler
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletSize = .2f;
    [SerializeField] private float maxShootDistance = 10f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float invulnerabilityDuration = 0.5f;
    
    private float _lastHitTime = float.NegativeInfinity; // damit der allererste Treffer garantiert durchgeht

    public event System.Action OnStatsChanged;

    public int PlayerHealth
    {
        get => playerHealth;
        set => playerHealth = value;
    }

    public float PlayerSpeed
    {
        get => playerSpeed;
        set => playerSpeed = value;
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public int BulletDamage
    {
        get => bulletDamage;
        set => bulletDamage = value;
    }

    public float BulletSize
    {
        get => bulletSize;
        set => bulletSize = value;
    }

    public float MaxShootDistance
    {
        get => maxShootDistance;
        set => maxShootDistance = value;
    }

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }
    
    public void TakeDamage(int damage)
    {
        //während unverwundbarkeit kann kein schaden nehmen
        if (Time.time < _lastHitTime + invulnerabilityDuration)
            return;

        //wenn getroffen unverwundbarkeit anwenden
        _lastHitTime = Time.time;
        playerHealth -= damage;

        if (playerHealth <= 0)
            Die();
        
        Debug.Log("Player damaged: " + playerHealth);
    }

    public void ApplyUpgrade(UpgradeData up)
    {
        switch (up.stat)
        {
            case UpgradableStat.MaxHealth: playerHealth += Mathf.RoundToInt(up.amount); break;
            case UpgradableStat.MoveSpeed: playerSpeed += up.amount; break;
            case UpgradableStat.BulletSpeed: bulletSpeed += up.amount; break;
            case UpgradableStat.BulletDamage: bulletDamage += Mathf.RoundToInt(up.amount); break;
            case UpgradableStat.BulletSize: bulletSize += up.amount; break;
        }
        OnStatsChanged?.Invoke();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
