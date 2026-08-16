using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bulletSize = .2f;
    [SerializeField] private float maxShootDistance = 10f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float invulnerabilityDuration = 0.5f;
    
    private float _lastHitTime = float.NegativeInfinity;
    private bool _isDead;

    public static event Action Died;

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
        if (Time.time < _lastHitTime + invulnerabilityDuration)
            return;
        
        _lastHitTime = Time.time;
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Die();
        }
        
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
    }
    
    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        
        Died?.Invoke();
        
        gameObject.SetActive(false);
    }
}
