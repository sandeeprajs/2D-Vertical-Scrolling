using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	#region Varablies
	private float speed = 1f, acceleration = 0.2f, maxSpeed = 3f;

	[HideInInspector]
	public bool moveCamera;
	#endregion


	#region Unity Methods
	// Use this for initialization
	void Start () 
	{
		moveCamera = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(moveCamera)
		{
			MoveCamera();
		}
	}
	#endregion
	

	#region Methods
	void MoveCamera ()
	{
		if(moveCamera)
		{
			Vector3 temp = transform.position;
			
			float oldY = temp.y;
			float newY = temp.y - (speed * Time.deltaTime);
			
			temp.y = Mathf.Clamp(temp.y, oldY, newY);
			
			temp.y = newY;
			
			transform.position = temp;
			
			speed += acceleration * Time.deltaTime;
			
			if (speed > maxSpeed)
				speed = maxSpeed;
		}
	}
	#endregion
}
