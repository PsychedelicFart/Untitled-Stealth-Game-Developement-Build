using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public int[] weapons =  new int[3]; 
	string[] wep = new string[3];
	string fyleName = " ";

	bool levelSelect = false;
	bool loadout = false;
	bool options = false;
	bool quit = false;

	bool audio = false;
	bool game = false;
	bool video = false;

	bool primary, secondary, nonLethal;

	test ttest;
	public GUISkin guiSkin;

	void Update () 
	{
	
	}

	void Start()
	{
		ttest = GameObject.Find("tezt5").GetComponent<test>();
	}
	
	void OnGUI()
	{
		GUI.skin = guiSkin;
		if (GUI.Button (new Rect (10,10,100,30), "Level Select") && !options && ! quit && !loadout)			//Menu select
		{
			levelSelect = !levelSelect;
		}
		if (levelSelect == true)
		{
			fyleName = GUI.TextField(new Rect(120,40,90,20), fyleName);
			if (GUI.Button (new Rect (120,10,90,20), "Load")) 
			{
				ttest.fileName = fyleName;
				Application.LoadLevel("SceneUno");
			}
		}
		if (GUI.Button (new Rect (10,50,100,30), "Loadout") && !options && ! quit && !levelSelect)			//Menu select
		{
			loadout = !loadout;
		}
		if (loadout == true)
		{
			if (GUI.Button (new Rect (120,50,100,30), "Primary")) 
			{
				primary = !primary;
				secondary = false;
				nonLethal = false;
			}
			if (GUI.Button (new Rect (120,90,100,30), "Secondary")) 
			{
				secondary = !secondary;
				primary = false;
				nonLethal = false;
			}
			if (GUI.Button (new Rect (120,130,100,30), "Non Lethal")) 
			{
				nonLethal = !nonLethal;
				primary = false;
				secondary = false;
			}
			if (primary == true)
			{
				if (GUI.Button (new Rect (220,50,100,20), "m16")) 
				{
					weapons[0] = 1;
					wep[0] = "m16";
				}
				if (GUI.Button (new Rect (220,80,100,20), "Uzi")) 
				{
					weapons[0] = 2;
					wep[0] = "Uzi";
				}
			}
			if (secondary == true)
			{
				if (GUI.Button (new Rect (220,90,100,20), "m1911")) 
				{
					weapons[1] = 3;
					wep[1] = "m1911";
				}
				if (GUI.Button (new Rect (220,120,100,20), "m9")) 
				{
					weapons[1] = 4;
					wep[1] = "m9";
				}
			}
			if (nonLethal == true)
			{
				if (GUI.Button (new Rect (220,130,100,20), "Tazer")) 
				{
					weapons[2] = 5;
					wep[2] = "Tazer";
				}
				if (GUI.Button (new Rect (220,160,100,20), "Tranq. Gun")) 
				{
					weapons[2] = 6;
					wep[2] = "Tranq.";
				}
			}
			if (GUI.Button (new Rect (220,185,100,30), "Accept")) 
			{
				for(int i = 0; i < 3; i++)
				{
					ttest.weapons[i] = weapons[i];
				}
			}
			GUI.Label(new Rect(400,50,100,20),wep[0]);
			GUI.Label(new Rect(400,90,100,20),wep[1]);
			GUI.Label(new Rect(400,130,100,20),wep[2]);
		}

		if (GUI.Button (new Rect (10,90,100,30), "Options") && !levelSelect && !quit && !loadout) 			//Options
		{
			options = !options;
		}
		if (options == true)
		{
			if (GUI.Button (new Rect (120,50,120,20), "Audio (coming soon)") && !game && !video ) 
			{
				audio = ! audio;
			}
			if (GUI.Button (new Rect (120,80,120,20), "Game(coming soon)") && !audio && !video) 
			{
				game = !game;
			}
			if (GUI.Button (new Rect (120,110,120,20), "Video(coming soon)") && !audio && !game) 
			{
				video = !video;
			}
		}
		if (audio == true)
		{
			//Audio options
		}
		if (game == true)
		{
			//Game options
		}
		if (video == true)
		{
			//Video options
		}

		if (GUI.Button (new Rect (10,130,100,30), "Quit") && !levelSelect && !options && !loadout) 				//Quit
		{
			quit = !quit;
		}
		if (quit == true)
		{
			if (GUI.Button (new Rect (120,90,90,20), "Yes, really.")) 
			{
				Application.Quit();
			}
			if (GUI.Button (new Rect (120,120,90,20), "No, not really.")) 
			{
				quit = false;
			}
		}
	}
}
