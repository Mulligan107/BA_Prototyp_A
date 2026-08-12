using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool AwareOfPlayer {get; set;}
    public Vector2 DirectionToPlayer {get; set;}

    [SerializeField] private float playerAwarenessDistance = 100f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 100f;

    private Transform _player;
    private Rigidbody2D _rb;
    private Vector2 _targetDirection;
    
    public void AddBonusSpeed(float  speedBonus)
    {
        speed += speedBonus;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        LookForPlayer();
    }
    
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsPlayer();
        SetVelocityToPlayer();
    }

    private void LookForPlayer()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }

    private void UpdateTargetDirection()
    {
        if (AwareOfPlayer)
        {
            _targetDirection = DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsPlayer()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        _rb.SetRotation(rotation);
    }

    private void SetVelocityToPlayer()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {
            _rb.linearVelocity = transform.up * speed;
        }
    }
}
