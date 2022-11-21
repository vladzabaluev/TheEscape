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

	private Rigidbody2D _rigidbody;
	private Transform _transform;
	private GameObject _gameObject;
	private Vector3 _velocity = Vector3.zero;
	private DirectionState _directionState = DirectionState.Right;

	private const float GroundedRadius = 0.2f;
	private bool _isGrounded;
	private bool _isAllowJump;

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
		bool wasGrounded = _isGrounded;
		_isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, GroundedRadius, _whatIsGround);
		foreach (Collider2D foundCollider in colliders)
		{
			if (foundCollider.gameObject != _gameObject)
			{
				_isGrounded = true;
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
		if (_isGrounded || _airControl)
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
		if (_isGrounded && _isAllowJump)
		{
			SwitchJumpPermission();
			_isGrounded = false;
			_rigidbody.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void SwitchJumpPermission()
	{
		_isAllowJump = !_isAllowJump;
	}

	private void ChangeDirection()
	{
		_transform.Rotate(0f, 180f, 0f);

		_directionState = _directionState == DirectionState.Right ? DirectionState.Left : DirectionState.Right;
	}

	private enum DirectionState
	{
		Right,
		Left
	}
}
