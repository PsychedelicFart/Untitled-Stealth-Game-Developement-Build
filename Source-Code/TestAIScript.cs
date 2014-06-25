using UnityEngine;
using System.Collections;

public class TestAIScript : MonoBehaviour {
	RaycastHit hit;

	TextMesh healthBar;
	NavMeshAgent ai;
	CapMove playr;

	public AudioClip shot;
	public AudioSource audio;

	public LayerMask a;
	public LayerMask b;
	
	public int health = 100;
	
	public float x = 0, y = 0, z = 0;
	float yy;
	float sinY;
	float cosY;
	public float aiSight = 30;
	public float deacTimer;
	float deactivationTime;
	public float shotTimer;
	public float activationTimer;
	float X, Z;//possibly already same var stored

	Vector3 aiVec;
	Vector3 lastPlayerPos;
	
	Color alertRayColor = Color.green;

	public Transform player;

	public bool showRays = false;
	bool moveToPlayer = false;
	bool playerSpotted = false;
	bool[] rayHitPlayer =  new bool[24];
	bool outofLOS = true;
	bool fire = false;
	
	void Start()
	{
		healthBar = gameObject.GetComponentInChildren<TextMesh>();
		ai = gameObject.GetComponent<NavMeshAgent>();
		playr = player.gameObject.GetComponent<CapMove>();
	}

	float FindDistanceOf()
	{
		float playerX, playerZ, aiX, aiZ, a, b, c;
		playerX = player.transform.position.x;
		playerZ = player.transform.position.z;
		aiX = X;
		aiZ = Z;
		a = playerX - aiX;
		b = playerZ - aiZ;
		a *= a;
		b *= b;
		c = a + b;
		c = Mathf.Sqrt(c);
		return c;
	}
	
	void Update () 
	{
		/*Updating Variables*/
		X = gameObject.transform.position.x;
		Z = gameObject.transform.position.z;
		healthBar.text = health.ToString();
		StartCoroutine(RayCheck());
		Debug.DrawRay(transform.position, aiVec);
		float fdo = FindDistanceOf();
		//Debug.Log(fdo);

		/*Targeting*/
		if(Physics.Raycast(gameObject.transform.position, player.position - transform.position, out hit, 50) 
		   && hit.transform.gameObject.tag == "Player")//check to see if player is in line of sight
		{
			outofLOS = false;
		}
		else
		{
			outofLOS = true;
			shotTimer = 0;
		}

		if (RayActivated () == true)//Were any rays tripped by player
		{
			deacTimer = 0;
			playerSpotted = true;
			lastPlayerPos = player.position;
		}
		else 
		{
			playerSpotted = false;
			if (deacTimer < 7)
			{
				lastPlayerPos = player.position;
			}
		}
		if (playerSpotted == true)
		{
			activationTimer += Time.deltaTime;
			deacTimer = 0;
			if (activationTimer >= 0.5f || fdo < 15f)				//Aware
			{
				alertRayColor = Color.yellow;
				deactivationTime = 2.8f;
			}
			if (activationTimer >= 1f || fdo < 7f)					//suspicious
			{
				moveToPlayer = true;
				alertRayColor = Color.blue;
				deactivationTime = 8f;
			}
			else
			{
				moveToPlayer = false;
			}
			if (activationTimer >= 1.5f || fdo < 2f)					//hostile
			{
				fire = true;
				alertRayColor = Color.red;
				deactivationTime = 30f;
			}
		}
		else
		{
			if (deacTimer < deactivationTime)			//Deactivate
			{
				deacTimer += Time.deltaTime;
			}
			else
			{
				activationTimer = 0;
				alertRayColor = Color.green;
				moveToPlayer = false;
			}
		}

		if (fire == true && !outofLOS)
		{
			if (shotTimer < .7f)
			{
				shotTimer += Time.deltaTime;
			}
			else
			{
				audio.PlayOneShot(shot);
				playr.health -= Random.Range(10,20);
				shotTimer = 0;
			}
		}

		if (moveToPlayer == true)
		{
			if (fdo <= 2)
			{
				ai.SetDestination (gameObject.transform.position);
			}
			else
			{
				ai.SetDestination (lastPlayerPos - aiVec * 2);
				
			}
		}
		else 
		{
			ai.SetDestination (gameObject.transform.position);
		}
	}

	//Use a const int to make to change number of rays easily
	//Combine with RayCheck()
	bool RayActivated()//Run on a separate thread
	{
		int hitCount = 0;//Debug feature
		for (int i = 0; i < 24; i++)
		{
			if(rayHitPlayer[i] == true)
			{
				hitCount++;
			}
		}
		if (hitCount >= 1)
		{
			return true;
		}
		return false;
	}

	IEnumerator RayCheck()//Should be running on a separate thread :/
	{
		int j = 0;//Keeps count of number of rays drawn. Also for the rayHitPlayer[] array
		for (int k = 0; k < 3; k++)//Run 3 times with different offsets
		{
			for(float i = -4; i < 4; i+=1)//Run 8 times for 8 rays
			{
				y = (gameObject.transform.eulerAngles.y+((i*10)+k*3))* Mathf.Deg2Rad;
				sinY = Mathf.Sin(y);
				cosY = Mathf.Cos(y);
				aiVec = new Vector3(sinY,0,cosY);
				if (Physics.Raycast(transform.position, aiVec, out hit ,aiSight) && hit.transform.gameObject.tag == "Player")
				{
					if (showRays == true)
					{
						Debug.DrawLine(transform.position, hit.point, Color.red);//Don't think can move
					}
					rayHitPlayer[j] = true;
				}
				else
				{
					if (showRays == true)
					{
						Debug.DrawLine(transform.position, hit.point, alertRayColor);
					}
					rayHitPlayer[j] = false;
				}
				j++;
			}
		}
		yield return null;
	}
}
