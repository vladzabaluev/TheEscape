using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float MoveSpeed = 30f;
	[SerializeField] private Controller Controller;
	[SerializeField] private Animator Animator;

	private float horizontalSpeed = 0f;
	private bool jump = false;

	private void Update()
	{
		horizontalSpeed = Input.GetAxisRaw("Horizontal") * MoveSpeed;
		Animator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			Animator.SetBool("IsJumping", true);
		}
	}

	private void FixedUpdate()
	{
		Controller.Move(horizontalSpeed * Time.fixedDeltaTime, jump);
		jump = false;
	}

	public void OnLanding()
	{
		Animator.SetBool("IsJumping", false);
	}
}
