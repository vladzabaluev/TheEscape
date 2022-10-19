using UnityEngine;
using UnityEngine.Events;

public class MovementController2D : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float _jumpForce = 700f;
	[Range(0, 0.3f)][SerializeField] private float _movementSmoothing = 0.05f;
	[SerializeField] private bool _airControl = true;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private const float groundedRadius = 0.2f;
	private bool _grounded;
	private Rigidbody2D _rigidbody;
	private Transform _transform;
	private Vector3 _velocity = Vector3.zero;
	private DirectionState _directionState = DirectionState.Right;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_transform = GetComponent<Transform>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		_directionState = _transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = _grounded;
		_grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, groundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_grounded = true;
				if (wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool jump)
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
		if (_grounded && jump)
		{
			_grounded = false;
			_rigidbody.AddForce(new Vector2(0f, _jumpForce));
		}
	}

	private void ChangeDirection()
	{
		if (_directionState == DirectionState.Right)
			_directionState = DirectionState.Left;
		else
			_directionState = DirectionState.Right;

		Vector3 localScale = _transform.localScale;
		localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
		_transform.localScale = localScale;
	}
	
	private enum DirectionState
	{
		Right,
		Left
	}
}
