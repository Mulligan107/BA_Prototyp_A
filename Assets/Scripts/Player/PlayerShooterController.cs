using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] private BulletBehaviour bulletPrefab;
    
    private PlayerStats _playerStats;
    private GameObject[] _enemyList;
    
    private float _nextShotTime;
    private float _refreshTimer;
    private const float RefreshInterval = 0.2f; //Damit gegner 5 mal die sekunde die liste befüllen

    private void Awake()
    {
        if (_playerStats == null) _playerStats = GetComponent<PlayerStats>();
    }
    
    private void Update()
    {
        _refreshTimer -= Time.deltaTime;
        if (_refreshTimer <= 0f)
        {
            _enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            _refreshTimer = RefreshInterval;
        }

        if (Time.time >= _nextShotTime)
        {
            if (Shoot())
            {
                _nextShotTime = Time.time + _playerStats.FireRate; // schüsse pro sekunde
            }
        }
    }

    private GameObject LookForNearestEnemy()
    {
        if (_enemyList == null) return null;

        GameObject targetEnemy = null;
        float maxDistance = _playerStats.MaxShootDistance;
        float targetDistanceSqrd = maxDistance * maxDistance;

        foreach (var enemy in _enemyList)
        {
            if (enemy == null) continue;

            Vector2 relativePosition = enemy.transform.position - transform.position;
            float distanceSqrd = relativePosition.sqrMagnitude;

            if (distanceSqrd < targetDistanceSqrd)
            {
                targetDistanceSqrd = distanceSqrd;
                targetEnemy = enemy;
            }
        }

        return targetEnemy; //nähester gegener
    }
    
    private bool Shoot()
    {
        GameObject target = LookForNearestEnemy();
        if (target == null) return false;
        
        Vector2 targetDirection = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        BulletBehaviour newBullet = Instantiate(
            bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, angle));

        newBullet.Init(_playerStats);
        return true;
    }
}
