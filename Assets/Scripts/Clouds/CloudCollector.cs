using UnityEngine;
using System.Collections;

public class CloudCollector : MonoBehaviour 
{
	#region Unity Methods
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Cloud" || collider.tag == "DeadlyCloud")
		{
			collider.gameObject.SetActive(false);
		}
	}
	#endregion
}
