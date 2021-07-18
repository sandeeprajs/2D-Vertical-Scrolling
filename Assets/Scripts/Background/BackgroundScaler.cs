using UnityEngine;
using System.Collections;

public class BackgroundScaler : MonoBehaviour 
{
	#region Unity Methods
	void Start ()
	{
		SpriteRenderer sr  = GetComponent<SpriteRenderer>();

		Vector3 scale = transform.localScale;

		float srWidth  = sr.sprite.bounds.size.x;

		float worldHeight = Camera.main.orthographicSize * 2f;

		float worldWeight = worldHeight / Screen.height * Screen.width;

		scale.x = worldWeight / srWidth;

		transform.localScale = scale;
	}
	#endregion
}
