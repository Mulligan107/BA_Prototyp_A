using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerStats _playerStats;
    private PlayerControls _playerControls;
    private Vector2 _move;
    
    
    private void Awake()
    {
        if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_playerStats == null) _playerStats = GetComponent<PlayerStats>();
        
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
    
    void Update()
    {
        _move = _playerControls.Movement.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _move * _playerStats.PlayerSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        if (collision)
        {
            _playerStats.TakeDamage(1);
        }
    }
}
