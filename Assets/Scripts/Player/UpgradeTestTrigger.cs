using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeTestTrigger : MonoBehaviour
{
    [SerializeField] private UpgradeSelectionController popup;
    [SerializeField] private PlayerStats stats;

    private PlayerControls _controls;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Testing.RandomTests.performed += OnTestPressed;
    }

    private void OnEnable()  => _controls.Testing.Enable();
    private void OnDisable() => _controls.Testing.Disable();

    private void OnDestroy()
    {
        _controls.Testing.RandomTests.performed -= OnTestPressed;
        _controls.Dispose();
    }

    private void OnTestPressed(InputAction.CallbackContext ctx)
    {
        Debug.Log("U gedrückt");
        popup.OpenCardUpgradeGUI(stats);
    }
}