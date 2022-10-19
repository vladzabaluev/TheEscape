using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float MoveSpeed = 30f;
	public MovementController2D MovementController;

	private float _horizontalSpeed;
	private bool _jump;
	private Animator _animatorController;
	private PlayerInputActions _playerInputActions;
	private InputAction _movement;
	
	private static readonly int Speed = Animator.StringToHash("Speed");
	private static readonly int IsJumping = Animator.StringToHash("IsJumping");

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
		_horizontalSpeed = _movement.ReadValue<Vector2>().x * MoveSpeed;
		_animatorController.SetFloat(Speed, Mathf.Abs(_horizontalSpeed));
	}

	private void FixedUpdate()
	{
		MovementController.Move(_horizontalSpeed * Time.fixedDeltaTime, _jump);
		_jump = false;
	}
	
	private void DoJump(InputAction.CallbackContext obj)
	{
		_jump = true;
		_animatorController.SetBool(IsJumping, true);
	}

	public void OnLanding()
	{
		_animatorController.SetBool(IsJumping, false);
	}
}
