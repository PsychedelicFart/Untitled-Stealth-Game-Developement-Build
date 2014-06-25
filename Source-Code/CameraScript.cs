using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public bool ortho = false;
	bool escape;
	Camera cam;

	void Start()
	{
		cam = gameObject.GetComponent<Camera>();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			escape = !escape;
		}
		if (ortho == true)
		{
			cam.orthographic = true;
		}
		else
		{
			cam.orthographic = false;
		}
	}

	void OnGUI()
	{
		if (escape == false)
		{
			ortho = GUI.Toggle(new Rect(10, 10, 100, 30), ortho, "Ortho./Perspec.");
		}
	}
}
