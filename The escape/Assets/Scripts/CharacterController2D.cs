using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float jumpForce = 700f;
	[Range(0, 0.3f)][SerializeField] private float movementSmoothing = 0.05f;
	[SerializeField] private bool airControl = false;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private const float groundedRadius = 0.2f;
	private bool grounded;
	private Rigidbody2D _rigidbody;
	private Transform _transform;
	private Vector3 velocity = Vector3.zero;
	private DirectionState directionState = DirectionState.Right;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_transform = GetComponent<Transform>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		directionState = _transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, groundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool jump)
	{
		if (grounded || airControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody.velocity.y);
			_rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (move > 0 && directionState == DirectionState.Left)
			{
				ChangeDirection();
			}
			else if (move < 0 && directionState == DirectionState.Right)
			{
				ChangeDirection();
			}
		}
		if (grounded && jump)
		{
			grounded = false;
			_rigidbody.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void ChangeDirection()
	{
		if (directionState == DirectionState.Right)
			directionState = DirectionState.Left;
		else
			directionState = DirectionState.Right;

		_transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
	}


	enum DirectionState
	{
		Right,
		Left
	}
}
