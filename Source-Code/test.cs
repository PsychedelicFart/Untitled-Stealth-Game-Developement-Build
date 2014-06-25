using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public int[] weapons = new int[3];
	public string fileName;
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}
