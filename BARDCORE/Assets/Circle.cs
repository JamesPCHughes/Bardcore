using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
	public int segments;
	public float radius =1;
	LineRenderer line;
	public bool expand = true;


	void Start ()
	{
		//StartCoroutine("Do");
		}

	void Update ()
	{
		//StartCoroutine ("Do");
		line = gameObject.GetComponent<LineRenderer>();
		
		line.SetVertexCount (segments + 1);
		line.useWorldSpace = false;
		CreatePoints ();
		if ((radius > 0) && (radius < 51) && (expand == true)) {
			radius += 1f;
		}
		if (radius > 50) {
			expand = false;
		}
		if (expand == false) {
						radius -= .5f;
				}
		if (radius < 0) {
			Destroy(gameObject);
				}

	}

	//IEnumerator Do() {
				//for (float radius = 0f; radius <= 50f; radius++) {
					//	radius += 1;
					//	print ("Do now");
					//	yield return new WaitForSeconds (2);
					//	print ("Do 2 seconds later");
			//	}
		
	//IEnumerator Example() {
   // yield return StartCoroutine("Do");
	//	print("Also after 2 seconds");
	//	print("This is after the Do coroutine has finished execution");
		//StartCoroutine ("Do");
	//}
	
	void CreatePoints ()
	{
		float x;
		float y;
		float z = 0f;
		
		float angle = 0f;
		
		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle);
			y = Mathf.Cos (Mathf.Deg2Rad * angle);
			line.SetPosition (i,new Vector3(x,y,z) * radius);
			angle += (360f / segments);
		}
	}
}
