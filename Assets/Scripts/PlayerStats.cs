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
}
