using UnityEngine;
using System.Collections;

public class InstantiateImpactPoints : MonoBehaviour {
	
	public GameObject impactPointPrefab;
	
	GameObject instantiatedImpactPoint;
	
	// Use this for initialization
	void Start () {
		foreach (Transform childTransform in gameObject.transform) {
			instantiatedImpactPoint = Instantiate (impactPointPrefab, childTransform.position, Quaternion.identity) as GameObject;
			instantiatedImpactPoint.transform.parent = gameObject.transform.parent;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
