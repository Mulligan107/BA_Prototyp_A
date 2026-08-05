using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float speed = 5;

    private void Awake()
    {
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = _playerControls.Movement.Move.ReadValue<Vector2>();
        rigidbody2D.linearVelocity = new  Vector2(move.x * speed, move.y * speed);
    }
}
