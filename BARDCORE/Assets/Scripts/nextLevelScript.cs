using UnityEngine;
using System.Collections;

public class nextLevelScript : MonoBehaviour {
	public int levelToLoad;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.anyKey||Input.GetButtonDown("Fire1")){
			Debug.Log("loading level");
			Application.LoadLevel(1);
		}
	}
}
