using UnityEngine;
using System.Collections;

public class comboDistance : MonoBehaviour {

	public Transform playerOne;
	public Transform playerTwo;
	public static float distanceBetweenPlayers;
	public static float maxComboDistance = 3;
	public static Vector3 midpoint;
	public GameObject theMeteor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		distanceBetweenPlayers = Vector3.Distance(playerOne.position, playerTwo.position);
		midpoint = (playerOne.position+playerTwo.position)/2;
		//Debug.Log("Distance: "+distanceBetweenPlayers);

	}

	public void castMeteor(){
		GameObject theCombo = Instantiate(theMeteor, midpoint, Quaternion.identity) as GameObject;
	}
}
