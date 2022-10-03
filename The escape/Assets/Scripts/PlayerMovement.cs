using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float MoveSpeed = 30f;
	public CharacterController2D Player;

	private float horizontalSpeed = 0f;
	private bool jump = false;
	private Animator _animatorController;

	private void Awake()
	{
		Player = Player == null ? GetComponent<CharacterController2D>() : Player;
		if (Player == null)
		{
			Debug.LogError("Player not set to controller");
		}

		_animatorController = GetComponent<Animator>();
	}

	private void Update()
	{
		horizontalSpeed = Input.GetAxisRaw("Horizontal") * MoveSpeed;
		_animatorController.SetFloat("Speed", Mathf.Abs(horizontalSpeed));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			_animatorController.SetBool("IsJumping", true);
		}
	}

	private void FixedUpdate()
	{
		Player.Move(horizontalSpeed * Time.fixedDeltaTime, jump);
		jump = false;
	}

	public void OnLanding()
	{
		_animatorController.SetBool("IsJumping", false);
	}
}
