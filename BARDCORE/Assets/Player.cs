using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameObject punch1PatternPrefab;
	
	
	
	GameObject instantiatedPunch;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			instantiatedPunch = Instantiate (punch1PatternPrefab) as GameObject;
			instantiatedPunch.transform.parent = gameObject.transform;
		}
	}
}