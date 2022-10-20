using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float MoveSpeed = 30f;
	[Range(0f, 1f)][SerializeField] private float _deadZoneY = 0.5f;
	[Range(0f, 1f)][SerializeField] private float _deadZoneX = 0.3f;
	public MovementController2D MovementController;
	
	private Animator _animatorController;
	private PlayerInputActions _playerInputActions;
	private InputAction _movement;
	private float _horizontalSpeed;
	private bool _allowJump;

	private void Awake()
	{
		_playerInputActions = new PlayerInputActions();
		
		MovementController = MovementController == null ? GetComponent<MovementController2D>() : MovementController;
		if (MovementController == null)
		{
			Debug.LogError("Player not set to controller");
		}

		_animatorController = GetComponent<Animator>();
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
			_horizontalSpeed = _movement.ReadValue<Vector2>().x * MoveSpeed;
		else
			_horizontalSpeed = 0f;
		
		if (_movement.ReadValue<Vector2>().y >= _deadZoneY)
			DoJump();
	}

	private void FixedUpdate()
	{
		MovementController.Move(_horizontalSpeed * Time.fixedDeltaTime);
	}

	private void DoJump()
	{
		MovementController.Jump();
	}
	
	private void DoJump(InputAction.CallbackContext obj)
	{
		DoJump();
	}

	public void OnLanding()
	{
		
	}
}
