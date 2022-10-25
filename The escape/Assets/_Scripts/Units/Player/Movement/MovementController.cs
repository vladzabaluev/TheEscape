using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
	[Header("Movement Parameters")]
	[Range(0, 0.3f)][SerializeField] private float _movementSmoothing = 0.08f;
	[SerializeField] private bool _airControl = true;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private const float groundedRadius = 0.2f;
	private bool _grounded;
	private bool _isAllowJump;
	private Rigidbody2D _rigidbody;
	private Transform _transform;
	private GameObject _gameObject;
	private Vector3 _velocity = Vector3.zero;
	private DirectionState _directionState = DirectionState.Right;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_transform = GetComponent<Transform>();
		_gameObject = GetComponent<GameObject>();

		OnLandEvent ??= new UnityEvent();

		_directionState = _transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = _grounded;
		_grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, groundedRadius, _whatIsGround);
		foreach (var foundCollider in colliders)
		{
			if (foundCollider.gameObject != _gameObject)
			{
				_grounded = true;
				if (wasGrounded)
				{
					OnLandEvent.Invoke();
					SwitchJumpPermission();
				}
			}
		}
	}

	public void Move(float move)
	{
		if (_grounded || _airControl)
		{
			Vector2 velocity = _rigidbody.velocity;
			Vector3 targetVelocity = new Vector2(move * 10f, velocity.y);
			_rigidbody.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, _movementSmoothing);

			switch (move)
			{
				case > 0 when _directionState == DirectionState.Left:
				case < 0 when _directionState == DirectionState.Right:
					ChangeDirection();
					break;
			}
		}
	}

	public void Jump(float jumpForce)
	{
		if (_grounded && _isAllowJump)
		{
			SwitchJumpPermission();
			_grounded = false;
			_rigidbody.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void SwitchJumpPermission()
	{
		_isAllowJump = !_isAllowJump;
	}

	private void ChangeDirection()
	{
		_directionState = _directionState == DirectionState.Right ? DirectionState.Left : DirectionState.Right;

		_transform.Rotate(0f, 180f, 0f);
	}

	private enum DirectionState
	{
		Right,
		Left
	}
}