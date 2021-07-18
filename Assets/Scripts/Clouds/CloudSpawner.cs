using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour 
{
	#region Varabiles
	[SerializeField]
	private GameObject[] clouds, collectables;

	private GameObject player;
	
	private float distanceBetweenClouds = 3f, minPosX, maxPosX, controlX, lastCloudPositionY;
	#endregion


	#region Unity Methods
	void Awake ()
	{
		controlX = 0f;

		SetMinXMaxX();
		CreateClouds();

		player = GameObject.Find("Player");
	}

	void Start ()
	{
		PositionThePlayer();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.tag == "Cloud" || collider.tag == "DeadlyCloud"){
			if(collider.transform.position.y == lastCloudPositionY ){

				Shuffle(clouds);

				Vector3 temp = collider.transform.position;

				for(int i = 0; i < clouds.Length; i++){
					if(!clouds[i].activeInHierarchy){
						if(controlX == 0f){
							temp.y = Random.Range(0.0f, maxPosX);
							controlX = 1f;
						} else if (controlX == 1f){
							temp.y = Random.Range(0f, minPosX);
							controlX = 2f;
						} else if (controlX == 2f){
							temp.y = Random.Range(1f, maxPosX);
							controlX = 3f;
						}else if (controlX == 3f){
							temp.y = Random.Range(1f, maxPosX);
							controlX = 0f;
						}

						temp.y -= distanceBetweenClouds;

						lastCloudPositionY = temp.y;

						clouds[i].transform.position = temp;
						clouds[i].SetActive(true);
					}
				}
			}
		}
	}
	#endregion


	#region Methods
	void SetMinXMaxX ()
	{
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

		minPosX = -bounds.x + 0.5f;
		maxPosX = bounds.x - 0.5f;
	}
	

	void Shuffle (GameObject[] arrayShuffle)
	{
		for(int i = 0; i < arrayShuffle.Length; i++)
		{
			GameObject temp =  arrayShuffle[i];

			int random = Random.Range(i, arrayShuffle.Length);

			arrayShuffle[i] = arrayShuffle[random];

			arrayShuffle[random] = temp;
		}
	}


	void CreateClouds ()
	{
		Shuffle(clouds);

		float positionY =0f; // Intilize Y position

		//Controll on random X position
		for(int i = 0; i < clouds.Length; i++)
		{
			Vector3 tempPosition = clouds[i].transform.position;

			tempPosition.y = positionY;
		
			if(controlX == 0f)
			{
				tempPosition.x = Random.Range(0.0f, maxPosX);
				controlX = 1f;
			}
			else if(controlX == 1f)
			{
				tempPosition.x = Random.Range(0.0f, minPosX);
				controlX = 2f;
			}
			else if(controlX == 2f)
			{
				tempPosition.x = Random.Range(1.0f, maxPosX);
				controlX = 3f;
			}
			else if(controlX == 3f)
			{
				tempPosition.x = Random.Range(-1.0f, minPosX);
				controlX = 0f;
			}

			lastCloudPositionY = positionY;

			clouds[i].transform.position = tempPosition;

			positionY -= distanceBetweenClouds; // Change Y Position
		}
	}


	void PositionThePlayer ()
	{
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("DeadlyCloud");
		GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

		// Create first placeable cloud
		for(int i = 0; i < darkClouds.Length; i++)
		{
			if(darkClouds[i].transform.position.y == 0f)
			{
				Vector3 tempDarkCloudPos = darkClouds[i].transform.position;

				darkClouds[i].transform.position = new Vector3(
					clouds[0].transform.position.x,
					clouds[0].transform.position.y,
					clouds[0].transform.position.z
					);

				clouds[0].transform.position = tempDarkCloudPos;
			}
		}

		//Place the player
		Vector3 tempCloudPos = clouds[0].transform.position;

		for(int i = 1; i < clouds.Length; i++)
		{
			if(tempCloudPos.y < clouds[i].transform.position.y)
			{
				tempCloudPos = clouds[i].transform.position;
			}
		}

		tempCloudPos.y += 0.8f;

		player.transform.position = tempCloudPos;
	}
	#endregion
}
