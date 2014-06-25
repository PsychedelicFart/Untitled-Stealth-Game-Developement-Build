using UnityEngine;
using System.Collections;

public class CoverBlock : MonoBehaviour {

	public bool left, right, front, back;
	public GameObject[] sides = new GameObject[4];
	public GameObject player;

	float FindDistanceOf()
	{
		float X,Z,pX,pZ,a,b,c;
		X = transform.position.x;
		Z = transform.position.z;
		pX = player.transform.position.x;
		pZ = player.transform.position.z;
		a = pX - X;
		b = pZ - Z;
		a *= a;
		b *= b;
		c = a + b;
		c = Mathf.Sqrt(c);

		return c;
	}

	void Update () 
	{
		float fdo = FindDistanceOf();
		/*Two levels of avtivation:
		 	<7: Icons on
		 	<5: Colliders on; can attach to cover
		 */
		if (fdo <= 7)
		{
			sides[0].gameObject.SetActive(left);
			sides[1].gameObject.SetActive(right);
			sides[2].gameObject.SetActive(front);
			sides[3].gameObject.SetActive(back);
		}
		else
		{
			sides[0].gameObject.SetActive(false);
			sides[1].gameObject.SetActive(false);
			sides[2].gameObject.SetActive(false);
			sides[3].gameObject.SetActive(false);
		}

	}
}
