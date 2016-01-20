using UnityEngine;
using System.Collections;

public class flameMovementController : MonoBehaviour {

	//if this is above 0, character can't move
	float movementCooldown=0;

	//if this is above 0, character can't attack
	float attackCooldown;

	//these are specific cooldowns for each attack
	float lightAttackCooldown = 1.5f;

	//public float movementSpeed = 1;
	public float movementSpeed = 0;

	//for charge attacks;
	float charge = 0; //tracks charge level
	public float maxCharge = 3; //how long you have to hold to get max charge

	//keeps track of the direction the direction player if facing.
	int facing =0;
	int xFacing =0;
	int zFacing =0;
	
	//this determines the distance where an attack APPEARS
	float attackSpawnDistance=1;

	public GameObject fire;

	// Use this for initialization
	/*	

void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		cooldownManagement();
		movementCheck();
		attackCheck();
	}

	void movementCheck(){
	if(movementCooldown<1){
			Debug.Log("movable");
			if(Input.GetKey(KeyCode.UpArrow)){
				Vector3 tempVector = gameObject.transform.position;
				tempVector.z = tempVector.z+(movementSpeed*Time.deltaTime);
				transform.position = tempVector;
				facing = 0;
				zFacing = 1;

			}

			if(Input.GetKey(KeyCode.RightArrow)){
				Vector3 tempVector = gameObject.transform.position;
				tempVector.x = tempVector.x+(movementSpeed*Time.deltaTime);
				transform.position = tempVector;
				facing = 1;
				xFacing = 1;
			}

			if(Input.GetKey(KeyCode.DownArrow)){
				Vector3 tempVector = gameObject.transform.position;
				tempVector.z = tempVector.z-(movementSpeed*Time.deltaTime);
				transform.position = tempVector;
				facing = 2;
				zFacing = -1;
			}

			if(Input.GetKey(KeyCode.LeftArrow)){
				Vector3 tempVector = gameObject.transform.position;
				tempVector.x = tempVector.x-(movementSpeed*Time.deltaTime);
				transform.position = tempVector;
				facing = 3;
				xFacing = -1;
			}
		}
	}

	void cooldownManagement(){

		if(movementCooldown>0){
			movementCooldown = movementCooldown-Time.deltaTime;
		}

		if(attackCooldown>0){
			attackCooldown = attackCooldown-Time.deltaTime;
		}

	}

	void attackCheck(){
		Debug.Log("attack checked");
		//Debug.Log ("charge: "+charge);
		if(Input.GetKeyDown(KeyCode.RightShift))
		{
			if(attackCooldown<1)
				{
				Debug.Log("right shift pushed");
				lightAttack();
				}
		}

		if(Input.GetKey(KeyCode.RightShift))
		{
			charge= charge+Time.deltaTime;

		}

		if(Input.GetKeyUp(KeyCode.RightShift))
		{
			if(charge>maxCharge){heavyAttack();}
			charge=0;
		}
	}

	void heavyAttack(){
		Vector3[] firePoints = new Vector3[4];
		firePoints[0] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance);
		firePoints[1] = new Vector3(gameObject.transform.position.x+attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		firePoints[2] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z-attackSpawnDistance);
		firePoints[3] = new Vector3(gameObject.transform.position.x-attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		GameObject tempFire = Instantiate(fire, firePoints[facing], Quaternion.identity) as GameObject;
		tempFire.GetComponent<fire>().setDirection(facing, 10);
		tempFire.transform.localScale = new Vector3(2,2,2);
		attackCooldown = lightAttackCooldown;
	}

	void lightAttack(){

		Vector3[] firePoints = new Vector3[4];
		firePoints[0] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance);
		firePoints[1] = new Vector3(gameObject.transform.position.x+attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		firePoints[2] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z-attackSpawnDistance);
		firePoints[3] = new Vector3(gameObject.transform.position.x-attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		GameObject tempFire = Instantiate(fire, firePoints[facing], Quaternion.identity) as GameObject;
		tempFire.GetComponent<fire>().setDirection(facing, 5);
		attackCooldown = lightAttackCooldown;

	}	
	*/
}
