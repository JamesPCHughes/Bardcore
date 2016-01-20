using UnityEngine;
using System.Collections;

public class comboCooldown : MonoBehaviour {

	public static int cooldown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown>0){
			cooldown--;
		}
	}
}
