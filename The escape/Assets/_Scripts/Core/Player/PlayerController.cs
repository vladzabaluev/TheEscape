using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 30f;
	[SerializeField] private float _jumpForce = 700f;
	[Range(0f, 1f)][SerializeField] private float _deadZoneY = 0.5f;
	[Range(0f, 1f)][SerializeField] private float _deadZoneX = 0.3f;
	[SerializeField] private MovementController _movementController;

	private PlayerInputActions _playerInputActions;
	private InputAction _movement;

	private float _horizontalSpeed;

	private void Awake()
	{
		_playerInputActions = new PlayerInputActions();

		_movementController = _movementController == null ? GetComponent<MovementController>() : _movementController;
		if (_movementController == null)
			Debug.LogError("Player not set to movement controller");
	}

	private void OnEnable()
	{
		_movement = _playerInputActions.Player.Move;
		_movement.Enable();

		_playerInputActions.Player.Jump.performed += DoJump;
		_playerInputActions.Player.Jump.Enable();
	}

	private void OnDisable()
	{
		_movement.Disable();
		_playerInputActions.Player.Jump.Disable();
	}

	private void Update()
	{
		if (Mathf.Abs(_movement.ReadValue<Vector2>().x) >= _deadZoneX)
			_horizontalSpeed = _movement.ReadValue<Vector2>().x * _moveSpeed;
		else
			_horizontalSpeed = 0f;

		if (_movement.ReadValue<Vector2>().y >= _deadZoneY)
			DoJump();
	}

	private void FixedUpdate()
	{
		_movementController.Move(_horizontalSpeed * Time.fixedDeltaTime);
	}

	public void OnLanding()
	{

	}

	private void DoJump()
	{
		_movementController.Jump(_jumpForce);
	}

	private void DoJump(InputAction.CallbackContext obj)
	{
		DoJump();
	}
}
