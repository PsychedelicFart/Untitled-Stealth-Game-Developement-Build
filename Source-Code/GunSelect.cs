using UnityEngine;
using System.Collections;

public class GunSelect : MonoBehaviour {

	public GameObject[] guns = new GameObject[3];

	public void Select(int selection)
	{
		if (selection == 1)
		{
			guns[0].SetActive(true);
			guns[1].SetActive(false);
			guns[2].SetActive(false);
		}
		if (selection == 2)
		{
			guns[0].SetActive(false);
			guns[1].SetActive(true);
			guns[2].SetActive(false);
		}
		if (selection == 3)
		{
			guns[0].SetActive(false);
			guns[1].SetActive(false);
			guns[2].SetActive(true);
		}
	}
}
