using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	#region Varablies
	private Rigidbody2D myRigidbody2d;

	private Animator anim;

	[SerializeField]
	private float moveSpeed = 8f, maxVelocity = 4f, jumpForce = 25f;
	#endregion

	#region Uniy Methods
	// Initialization
	void Awake () 
	{
		myRigidbody2d = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void FixedUpdate ()
	{
		PlayerMovementKeyboard();
	}
	#endregion

	#region Methods
	private void PlayerMovementKeyboard ()
	{
		float forceX = 0f, forceY = 0f;

		float vel = Mathf.Abs(myRigidbody2d.velocity.x);

		float horz = Input.GetAxisRaw("Horizontal");

		if(horz > 0)
		{
			if(vel < maxVelocity)
			{
				forceX = moveSpeed;
			}

			Vector3 scale =  transform.localScale;
			scale.x = 4;
			transform.localScale = scale;

			anim.SetBool("playerWalk", true);
		}
		else if(horz < 0)
		{
			if(vel < maxVelocity)
			{
				forceX = -moveSpeed;
			}

			Vector3 scale =  transform.localScale;
			scale.x = -4;
			transform.localScale = scale;

			anim.SetBool("playerWalk", true);
		}
		else
		{
			anim.SetBool("playerWalk", false);
		}

		myRigidbody2d.AddForce(new Vector2(forceX, forceY));
	}
	#endregion
}
