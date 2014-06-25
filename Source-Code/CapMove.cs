using UnityEngine;
using System.Collections;
using System.IO;

public class CapMove : MonoBehaviour {




	/*
	 * 1. Fire
	 * 2. Reload
	 * 3. Dry Fire
	 */

	public AudioClip[] m16Sound = new AudioClip[2];
	public AudioClip[] m1911Sound = new AudioClip[3];
	public AudioClip[] UziSound = new AudioClip[2];

	public AudioSource playerAudio;

	int[,] tileArray = new int[20,20];
	public int[] weaponInv = new int[3];//Make wep inv

	public GameObject grid, tile, enemy, playe, ammo;
	GameObject camOrb;
	GameObject wepInst;
	public GameObject[] weapons = new GameObject[3];
	TextMesh healthBar;
	Transform cameraOrbital;
	Transform player;
	Transform currentTarget;
	TestAIScript a;
	GunSelect gunSelect;
	test ttest;
	
	public bool showGUI = true;
	public bool topDown = false;
	bool oldTD = false;
	bool rotateR = false;
	bool rotateL = false;
	bool shoot;
	bool inCover = false;
	bool left = false;
	bool escape;

	public int playerSpeed = 10;
	public int playerShotDis = 50;
	public int orbitCamSpeed = 10;
	public int health = 100;
	int gunSelection = 1;
	int damage = 1;

	float rateOfFire = 1;
	float camRotY;
	public float shotTimer;

	//Vec3s are cover lean related;
	Vector3 coverR;
	Vector3 coverV, cover;
	Vector3 coverL;

	public string moveMode = "vel";
	string leaning;
	string fileName;
	
	RaycastHit hit;

	/*
	 * 1. m16
	 * 2. Uzi
	 * 3. m1911
	 * 4. m9
	 * 5. Tazer
	 * 6. Tranqn Gun
	*/

	m16 am16;
	Uzi aUzi;
	m1911 am1911;
	m9 am9;
	Tazer aTazer;
	Tranq aTranq;
	
	void Start()
	{
		healthBar = gameObject.GetComponentInChildren<TextMesh>();
		wepInst = GameObject.Find("wepInst");
		camOrb = GameObject.Find("CameraOrbital");

		am16 = wepInst.GetComponent<m16>();
		aUzi = wepInst.GetComponent<Uzi>();
		am1911 = wepInst.GetComponent<m1911>();
		am9 = wepInst.GetComponent<m9>();
		aTazer = wepInst.GetComponent<Tazer>();
		aTranq = wepInst.GetComponent<Tranq>();

		ttest = GameObject.Find("tezt5").GetComponent<test>();
		fileName = ttest.fileName;
		gunSelect = GameObject.Find("GunSelect").GetComponent<GunSelect>();

		//Save(fileName);
		Read(fileName);
		RefreshMap();
	}

	void Save(string filename)
	{
		using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + filename)) 
		{
			for (int X = 0; X < 20; X++)
			{
				for (int Y = 0; Y < 20; Y++)
				{
					sw.Write(tileArray[X,Y] + " ");
				}
				sw.Write(",");
			}
		}
	}
	
	void Read(string filename)
	{
		string input;
		input = File.ReadAllText(Application.dataPath + "/" + filename);
		Debug.Log(input);
		int y = 0, x = 0;
		foreach (var row in input.Split(','))
		{
			y = 0;
			foreach (var column in row.Split(' '))
			{
				column.Trim();
				tileArray[x, y] = int.Parse(column);
				Debug.Log(x + " " + y + " " + row);
				if (y == 19)
				{
					break;
				}
				else
				{
					y++;
				}
				
			}
			if (x == 19)
			{
				break;
			}
			else
			{
				x++;
			}
		}
	}

	void RefreshMap()
	{
		GameObject mapBase = new GameObject("Map Base");
		Transform nool, tilee;
		nool = grid.transform;
		tilee = tile.transform;
		for (int X = 0; X < 20; X++)
		{
			for (int Y = 0; Y < 20; Y++)
			{
				if (tileArray[Y,X] == 0)
				{
					GameObject gridTile = (GameObject)Instantiate(grid, new Vector3(X * 2,0,Y * 2), nool.transform.rotation);
					gridTile.transform.parent = mapBase.transform;
				}
				else if (tileArray[Y,X] == 1)
				{
					GameObject tileTile = (GameObject)Instantiate(tile, new Vector3(X * 2,0,Y * 2), tilee.transform.rotation);
					tileTile.transform.parent = mapBase.transform;
				}
				if (tileArray[Y,X] == 2)
				{
					GameObject enemyTile = (GameObject)Instantiate(enemy, new Vector3(X * 2,0,Y * 2), nool.transform.rotation);
					enemyTile.transform.parent = mapBase.transform;
				}
				else if (tileArray[Y,X] == 3)
				{
					GameObject playeTile = (GameObject)Instantiate(playe, new Vector3(X * 2,0,Y * 2), nool.transform.rotation);
					playeTile.transform.parent = mapBase.transform;
				}
				else if (tileArray[Y,X] == 4)
				{
					GameObject ammoTile = (GameObject)Instantiate(ammo,  new Vector3(X * 2,0,Y * 2), nool.transform.rotation);
					ammoTile.transform.parent = mapBase.transform;
				}
			}
		}
	}

	int rotCount = 0;//Used for "lerping" of the camera rotation

	void Update()
	{
		healthBar.text = health.ToString();
		cameraOrbital = camOrb.transform;
		camRotY = cameraOrbital.eulerAngles.y;

		if (gunSelection == 1)
		{
			if (ttest.weapons[0] == 1)
			{
				damage = am16.damage;
				rateOfFire = am16.rateOfFire;
			}
			if (ttest.weapons[0] == 2)
			{
				damage = aUzi.damage;
				rateOfFire = aUzi.rateOfFire;
			}
		}
		if (gunSelection == 2)
		{
			if (ttest.weapons[1] == 3)
			{
				damage = am1911.damage;
				rateOfFire = am1911.rateOfFire;
			}
			if (ttest.weapons[1] == 4)
			{
				damage = am9.damage;
				rateOfFire = am9.rateOfFire;
			}
		}
		if (gunSelection == 3)
		{
			if (ttest.weapons[2] == 5)
			{
				damage = aTazer.damage;
				rateOfFire = aTazer.rateOfFire;
			}
			if (ttest.weapons[2] == 6)
			{
				damage = aTranq.damage;
				rateOfFire = aTranq.rateOfFire;
			}
		}
		
		/*Basic Game Mechanics*/
		if (health <= 0)
		{
			Destroy(gameObject);
		}

		/*GUI Update Code*/
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			gunSelection++;
			if (gunSelection >= 4)
			{
				gunSelection = 1;
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			gunSelection--;
			if (gunSelection <= 0)
			{
				gunSelection = 3;
			}
		}

		gunSelect.Select(gunSelection);
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			escape = !escape;
			showGUI = !showGUI;
		}
		if(rotateR == true)										//Consolidate into function
		{ 
			showGUI = false;
			cameraOrbital.eulerAngles += new Vector3(0,3f,0);
			rotCount++;
			if (rotCount >= 30)
			{
				showGUI = true;
				rotCount = 0;
				rotateR = false;
			}
		}
		if(rotateL == true)
		{
			showGUI = false;
			cameraOrbital.eulerAngles -= new Vector3(0,3f,0);
			rotCount++;
			if (rotCount >= 30)
			{
				showGUI = true;
				rotCount = 0;
				rotateL = false;
			}
		}
		if (topDown != oldTD)
		{
			if (topDown == true)
			{
				cameraOrbital.eulerAngles += new Vector3(55,0,0);
			}
			else if(topDown == false)
			{
				cameraOrbital.eulerAngles -= new Vector3(55,0,0);
			}
		}

		/*Targeting*/
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.gameObject.tag == "Enemy")
			{
				currentTarget = hit.transform;
				a = hit.collider.gameObject.GetComponent<TestAIScript>();
				shoot = true;
			}
			if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.gameObject.tag == "Cover" && inCover == false)//Cover targeting code is brute forced; revise
			{
				gameObject.transform.position = hit.collider.gameObject.transform.position + hit.normal;//Change to a "set destination"
				cover = hit.normal;

				float x, z;
				x = cover.x;
				z = cover.z;
				if (x == 1)//Brute force
				{
					coverV = new Vector3(z, 0, x+.5f);
				}
				else if(x == -1)
				{
					coverV = new Vector3(z, 0, x-.5f);
				}
				else if (z == 1)
				{
					coverV = new Vector3(-z-.5f, 0, x);
				}
				else if (z == -1)
				{
					coverV = new Vector3(-z+.5f, 0, x);
				}
				coverR = transform.position + coverV;
				coverL = transform.position - coverV;
				cover = transform.position;
				cover.y = .801f;
				inCover = true;
			}
		}
		if (inCover == true)
		{
			if (Input.GetKeyDown(KeyCode.D) && left == false)
			{
				transform.position = coverR;
			}
			else if (Input.GetKeyUp(KeyCode.D) && left == false)
			{
				transform.position = cover;
			}
			else if(Input.GetKeyDown(KeyCode.A) && left == false)
			{
				transform.position = coverL;
			}
			else if(Input.GetKeyUp(KeyCode.A))
			{
				transform.position = cover;
			}
		}
		if (shoot == true && Physics.Raycast(transform.position, currentTarget.transform.position - transform.position, out hit, playerShotDis) && hit.transform.gameObject.tag == "Enemy")
		{
			shotTimer += Time.deltaTime;
			if (shotTimer >= rateOfFire)
			{
				if (gunSelection == 1)//FUNCTION
				{
					if (ttest.weapons[0] == 1)
					{
						playerAudio.PlayOneShot(m16Sound[0]);
					}
					if (ttest.weapons[0] == 2)
					{
						playerAudio.PlayOneShot(UziSound[0]);
					}
				}
				if (gunSelection == 2)
				{
					if (ttest.weapons[1] == 3)
					{
						playerAudio.PlayOneShot(m1911Sound[0]);
					}
					if (ttest.weapons[1] == 4)
					{
						playerAudio.PlayOneShot(m1911Sound[0]);
					}
				}
				if (gunSelection == 3)
				{
					if (ttest.weapons[2] == 5)
					{
						playerAudio.PlayOneShot(m16Sound[0]);
					}
					if (ttest.weapons[2] == 6)
					{
						playerAudio.PlayOneShot(m16Sound[0]);
					}
				}

				a.health -= damage;
				shotTimer = 0;
			}
			if (a.health <= 0)
			{
				Destroy(hit.collider.gameObject);
				currentTarget = null;
				a = new TestAIScript();
				shoot = false;
			}
		}
		if (shoot == true)//draw targeting ray from player to enemy
		{
			Debug.DrawRay(transform.position, currentTarget.transform.position - transform.position, Color.blue);
		}
		oldTD = topDown;//oldTD is used to detect change in toggle
	}
	
	void FixedUpdate () 
	{
		if (inCover == true)
		{
			if (Input.GetKey(KeyCode.Tab))
			{
				inCover = false;
			}
		}
		Debug.Log(gunSelection);
		/*Start input code*/
		if (moveMode == "tran" && inCover == false)
		{
			if(camRotY > -30 && camRotY < 30)
			{
				if (Input.GetKey(KeyCode.W))
				{
					transform.Translate(new Vector3(0,0,playerSpeed * Time.deltaTime));
				}
				else if (Input.GetKey(KeyCode.S))
				{
					transform.Translate(new Vector3(0,0,-playerSpeed * Time.deltaTime));
				}
				if (Input.GetKey (KeyCode.A))
				{
					transform.Translate(new Vector3(-playerSpeed * Time.deltaTime,0,0));
				}
				else if(Input.GetKey (KeyCode.D))
				{
					transform.Translate(new Vector3(playerSpeed * Time.deltaTime,0,0));
				}
			}
			else if (camRotY > 200 && camRotY < 300)
			{
				if (Input.GetKey(KeyCode.D))
				{
					transform.Translate(new Vector3(0,0,playerSpeed * Time.deltaTime));
				}
				else if (Input.GetKey(KeyCode.A))
				{
					transform.Translate(new Vector3(0,0,-playerSpeed * Time.deltaTime));
				}
				if (Input.GetKey (KeyCode.W))
				{
					transform.Translate(new Vector3(-playerSpeed * Time.deltaTime,0,0));
				}
				else if(Input.GetKey (KeyCode.S))
				{
					transform.Translate(new Vector3(playerSpeed * Time.deltaTime,0,0));
				}
			}
			else if (camRotY > 100 && camRotY < 200)
			{
				if (Input.GetKey(KeyCode.S))
				{
					transform.Translate(new Vector3(0,0,playerSpeed * Time.deltaTime));
				}
				else if (Input.GetKey(KeyCode.W))
				{
					transform.Translate(new Vector3(0,0,-playerSpeed * Time.deltaTime));
				}
				if (Input.GetKey (KeyCode.D))
				{
					transform.Translate(new Vector3(-playerSpeed * Time.deltaTime,0,0));
				}
				else if(Input.GetKey (KeyCode.A))
				{
					transform.Translate(new Vector3(playerSpeed * Time.deltaTime,0,0));
				}
			}
			else if (camRotY > 1 && camRotY < 120)
			{
				if (Input.GetKey(KeyCode.A))
				{
					transform.Translate(new Vector3(0,0,playerSpeed * Time.deltaTime));
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.Translate(new Vector3(0,0,-playerSpeed * Time.deltaTime));
				}
				if (Input.GetKey (KeyCode.S))
				{
					transform.Translate(new Vector3(-playerSpeed * Time.deltaTime,0,0));
				}
				else if(Input.GetKey (KeyCode.W))
				{
					transform.Translate(new Vector3(playerSpeed * Time.deltaTime,0,0));
				}
			}
		}
		if (moveMode == "vel")
		{
			if (Input.GetKey(KeyCode.W))
			{
				rigidbody.velocity = new Vector3(0, 0, playerSpeed);
			}
			if (Input.GetKey(KeyCode.S))
			{
				rigidbody.velocity = new Vector3(0, 0, -playerSpeed);
			}
			if (Input.GetKey(KeyCode.A))
			{
				rigidbody.velocity = new Vector3(-playerSpeed, 0, 0);
			}
			if (Input.GetKey(KeyCode.D))
			{
				rigidbody.velocity = new Vector3(playerSpeed, 0, 0);
			}
		}
		/*End input code*/
	}

	void OnGUI()
	{
		if (showGUI == true)
		{
			if (GUI.Button (new Rect (100,40,80,20), "--->") || Input.GetKey(KeyCode.Q)) 
			{
				rotateL = true;
			}
			if (GUI.Button (new Rect (20,40,80,20), "<---") || Input.GetKey(KeyCode.E)) 
			{
				rotateR = true;
			}
			topDown = GUI.Toggle(new Rect(450, 10, 100, 30), topDown, "TopDown");
		}
		if (escape == true)
		{
			if (GUI.Button (new Rect (10,10,80,20), "Quit")) 
			{
				Application.LoadLevel(0);
			}
		}
	}
}
