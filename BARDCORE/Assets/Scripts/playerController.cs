using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//basic player ID
	public int playerNumber;
	public Color defaultColor; //the regular color of the player
	public float movementSpeed = 2;
	public float defaultMovementSpeed = 2;
	public GameObject fire;
	public GameObject fire2;
	public GameObject fire3;
	public GameObject fire4;
	public GameObject otherPlayer;
	public Color currentColor;
	public GameObject myParticleSystem;
	public Transform frontAttackContainer;
	public GameObject P2GameObjectPC;
	public GameObject P1GameObjectPC;


	//sound related limits
	public float chargeSoundStarts = .5f;
	public AnimationCurve colorCurve;

	//stat checks; if the number is above 0 they can't behave
	public float movementCooldown=0;
	public float attackCooldown;

	//Outside references
	public comboDistance comboController;
	public soundInventory mySoundinventory;
	
	//all combo related stuff
	public bool comboable = false; //true if you can combo
	public int comboWindow = 15; //the number of frames you have to execute a combo
	public int comboCounter = 0; // counts how long you're waiting for the combo
	public Color comboReadyColor; // just for debug purposes


	//these are specific cooldowns for each attack
	public float lightAttackCooldown = 1f;
	public float charge = 0; //tracks charge level
	public float maxCharge = 3; //how long you have to hold to get max charge

	//this determines the distance where an attack APPEARS
	public float attackSpawnDistance= 1;
	
	//keeps track of the direction the direction player if facing.
	public int facing =0;
	public int xFacing =0;
	public int zFacing =0;

	// Use this for initialization
	public virtual void Start () {
		P2GameObjectPC = GameObject.FindGameObjectWithTag("Player2");
		P1GameObjectPC = GameObject.FindGameObjectWithTag("Player");
		comboController = GameObject.FindObjectOfType<comboDistance>();
		mySoundinventory = GameObject.FindObjectOfType<soundInventory>();
		myParticleSystem = gameObject.GetComponentInChildren<ParticleSystem>().gameObject;
		myParticleSystem.SetActive(false);
		currentColor=defaultColor;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
		
		movementSpeed=0;
		
		if(comboable){comboCheck();}
		cooldownManagement();
		faceCheck();
		moving();
		attackCheck();
		colorCheck();
	}

	//These methods needs to be overriden in each child class

	public virtual void comboCheck(){
		//if(comboable){
		//Debug.Log("Player 1 combochecking");
		
		if(comboCooldown.cooldown>0){
			comboable=false;
		}

//		if(otherPlayer.GetComponent<playerTwoController>().comboable==true && comboDistance.maxComboDistance>comboDistance.distanceBetweenPlayers){
//			Debug.Log("Combo!");
//			comboController.castMeteor();
//			comboCounter = 0;
//			comboable = false;
//			comboCooldown.cooldown = 30;
//		}
//		
//		if(comboCounter<1 && comboCooldown.cooldown<1){
//			
//			Debug.Log("combo cooldown is: "+comboCooldown.cooldown);
//			heavyAttack();
//			comboCounter=0;
//			comboable = false;
//		}
//		
//		else{
//			comboCounter--;
//		}
		//}
	}
	
	public void colorCheck(){
		if(charge>0){


			currentColor.r = Mathf.Lerp(currentColor.r, comboReadyColor.r, Time.deltaTime/3);
			currentColor.g = Mathf.Lerp(currentColor.g, comboReadyColor.g, Time.deltaTime/3);
			currentColor.b = Mathf.Lerp(currentColor.b, comboReadyColor.b, Time.deltaTime/3);
			myParticleSystem.SetActive(true);

			/*
			currentColor.r +=.5f;
			currentColor.g +=.5f;
			currentColor.b +=.5f;
			*/
		}
		else{
			currentColor = defaultColor;
			myParticleSystem.SetActive(false);
		}
		gameObject.GetComponent<Renderer>().material.color = currentColor;
	}
	
	public void moving(){
		if(movementCooldown<1){
			if(charge>0){
				//REINSTATE THIS
				//movementSpeed=movementSpeed/charge;
			}
			Vector3 tempVector = gameObject.transform.position;
			tempVector.z = tempVector.z+((movementSpeed*Time.deltaTime*zFacing));
			tempVector.x = tempVector.x+((movementSpeed*Time.deltaTime*xFacing));
			gameObject.transform.position = tempVector;
		}
	}
	
	public virtual void faceCheck(){
		//Debug.Log("xFacing: "+xFacing+" zFacing: "+zFacing);
		/*
		if(Input.GetKey(KeyCode.UpArrow)||Input.GetAxis("Vertical")>0){
			zFacing = 1;		
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.RightArrow)||Input.GetAxis("Horizontal")>0){
			xFacing = 1;
			zFacing = 0;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.DownArrow)||Input.GetAxis("Vertical")<0){
			zFacing = -1;
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow)||Input.GetAxis("Horizontal")<0){
			xFacing=-1;
			zFacing=0;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.DownArrow)&&Input.GetKey(KeyCode.LeftArrow)||Input.GetAxis("Vertical")<0&&Input.GetAxis("Horizontal")<0)
		{
			zFacing = -1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.DownArrow)&&Input.GetKey(KeyCode.RightArrow)||Input.GetAxis("Vertical")<0&&Input.GetAxis("Horizontal")>0){
			zFacing = -1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.UpArrow)&&Input.GetKey(KeyCode.RightArrow)||Input.GetAxis("Vertical")>0&&Input.GetAxis("Horizontal")>0){
			zFacing = 1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
		}
		
		if(Input.GetKey(KeyCode.UpArrow)&&Input.GetKey(KeyCode.LeftArrow)||Input.GetAxis("Vertical")>0&&Input.GetAxis("Horizontal")<0){
			zFacing = 1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
		}
		 */
	}
	
	public void cooldownManagement(){
		
		if(movementCooldown>0){
			movementCooldown = movementCooldown-Time.deltaTime;
		}
		
		if(attackCooldown>0){
			attackCooldown = attackCooldown-Time.deltaTime;
		}
		
		
		
	}
	
	public virtual void attackCheck(){
		if(charge>=.5f && charge <=.75f){
			GetComponent<AudioSource>().clip = mySoundinventory.chargeSound;
			GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip,.5f);
		}
		if(charge==0){
			GetComponent<AudioSource>().Stop();
		}
		//Debug.Log ("charge: "+charge);

	}
	
	public void heavyAttack(){
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+attackSpawnDistance*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);
		/*
		firePoints[0] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance);
		firePoints[1] = new Vector3(gameObject.transform.position.x+attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		firePoints[2] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z-attackSpawnDistance);
		firePoints[3] = new Vector3(gameObject.transform.position.x-attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
*/
		GameObject tempFire = Instantiate(fire, myFirePoint, Quaternion.identity) as GameObject;
		tempFire.transform.localScale = new Vector3(2,2,2);
		tempFire.GetComponent<fire>().playerID=playerNumber;
		tempFire.GetComponent<fire>().chargedShot = true;
		tempFire.GetComponent<fire>().lifespan = 5;
		tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		attackCooldown = lightAttackCooldown;
	}
	public void comboConnect() {

		Vector3 connector = Vector3.Lerp(gameObject.transform.position, P2GameObjectPC.transform.position, 0.1F);
		GameObject tempFire1 = Instantiate (fire4, connector, Quaternion.identity) as GameObject;
		Vector3 connector2 = Vector3.Lerp(gameObject.transform.position, P2GameObjectPC.transform.position, 0.3F);
		GameObject tempFire2 = Instantiate (fire4, connector2, Quaternion.identity) as GameObject;	
		Vector3 connector3 = Vector3.Lerp(gameObject.transform.position, P2GameObjectPC.transform.position, 0.5F);
		GameObject tempFire3 = Instantiate (fire4, connector3, Quaternion.identity) as GameObject;	
		Vector3 connector4 = Vector3.Lerp(gameObject.transform.position, P2GameObjectPC.transform.position, 0.7F);
		GameObject tempFire4 = Instantiate (fire4, connector4, Quaternion.identity) as GameObject;	
		Vector3 connector5 = Vector3.Lerp(gameObject.transform.position, P2GameObjectPC.transform.position, 0.9F);
		GameObject tempFire5 = Instantiate (fire4, connector5, Quaternion.identity) as GameObject;	

	}

	public void comboConnectP2() {
		
		Vector3 connector = Vector3.Lerp(gameObject.transform.position, P1GameObjectPC.transform.position, 0.1F);
		GameObject tempFire1 = Instantiate (fire4, connector, Quaternion.identity) as GameObject;
		Vector3 connector2 = Vector3.Lerp(gameObject.transform.position, P1GameObjectPC.transform.position, 0.3F);
		GameObject tempFire2 = Instantiate (fire4, connector2, Quaternion.identity) as GameObject;	
		Vector3 connector3 = Vector3.Lerp(gameObject.transform.position, P1GameObjectPC.transform.position, 0.5F);
		GameObject tempFire3 = Instantiate (fire4, connector3, Quaternion.identity) as GameObject;	
		Vector3 connector4 = Vector3.Lerp(gameObject.transform.position, P1GameObjectPC.transform.position, 0.7F);
		GameObject tempFire4 = Instantiate (fire4, connector4, Quaternion.identity) as GameObject;	
		Vector3 connector5 = Vector3.Lerp(gameObject.transform.position, P1GameObjectPC.transform.position, 0.9F);
		GameObject tempFire5 = Instantiate (fire4, connector5, Quaternion.identity) as GameObject;	
		
	}



	//public void comboConnector() {
		//StartCoroutine ("comboConnect");
	//	}
	public void lightAttack(){
		attackSpawnDistance = 1;
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+attackSpawnDistance*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);
		//IF movementCooldown = 0f; THEN YOU DASH.22
		//movementCooldown = .1f;
		//REINSTATE THIS
		//movementCooldown = lightAttackCooldown+.2f;

		/*
		Vector3[] firePoints = new Vector3[4];
		firePoints[0] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance);
		firePoints[1] = new Vector3(gameObject.transform.position.x+attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		firePoints[2] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z-attackSpawnDistance);
		firePoints[3] = new Vector3(gameObject.transform.position.x-attackSpawnDistance, gameObject.transform.position.y, gameObject.transform.position.z);
		*/

		GameObject tempFire = Instantiate(fire, myFirePoint, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		tempFire.GetComponent<fire>().playerID=playerNumber;
		tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		attackCooldown = lightAttackCooldown;
	}

	public void lightAttack2(){
		attackSpawnDistance = 3;
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+attackSpawnDistance*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);

		GameObject tempFire = Instantiate(fire, myFirePoint, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		tempFire.GetComponent<fire>().playerID=playerNumber;
		tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	public void lightAttack3(){
		attackSpawnDistance = 5;
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+attackSpawnDistance*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);
		
		GameObject tempFire = Instantiate(fire, myFirePoint, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		tempFire.GetComponent<fire>().playerID=playerNumber;
		tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	public void lightAttack4(){
		attackSpawnDistance = 3;
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+(10+attackSpawnDistance)*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);
		
		GameObject tempFire = Instantiate(fire, myFirePoint, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		tempFire.GetComponent<fire>().playerID=playerNumber;
		tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	public void lightAttack5(){
		//Right Swing
		//attackSpawnDistance = 3;
		Transform PunchPoint1 = transform.FindChild ("1");
		Transform PunchPoint2 = transform.FindChild ("2");
		Transform PunchPoint3 = transform.FindChild ("3");
		Transform PunchPoint4 = transform.FindChild ("4");
		Transform PunchPoint4a = transform.FindChild ("4a");

		GameObject tempFire = Instantiate(fire, PunchPoint1.position, Quaternion.identity) as GameObject;
		GameObject tempFire1 = Instantiate(fire, PunchPoint2.position, Quaternion.identity) as GameObject;
		GameObject tempFire2 = Instantiate(fire, PunchPoint3.position, Quaternion.identity) as GameObject;
		GameObject tempFire3 = Instantiate(fire, PunchPoint4.position, Quaternion.identity) as GameObject;
		GameObject tempFire3a = Instantiate(fire, PunchPoint4a.position, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		//tempFire.GetComponent<fire>().playerID=playerNumber;
		//tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	public void lightJab(){
		//Right Swing
		//attackSpawnDistance = 3;
		Transform PunchPoint1 = transform.FindChild ("jab");
		Quaternion PunchRotation1 = gameObject.transform.rotation;
		
		//GameObject tempFire = Instantiate(fire, PunchPoint1.position, Quaternion.identity) as GameObject;
		GameObject tempFire = Instantiate(fire, PunchPoint1.position, PunchRotation1) as GameObject;

	}

	public void lightAttack6(){
		//Left Swing
		//attackSpawnDistance = 3;
		Transform PunchPoint1 = transform.FindChild ("5");
		Transform PunchPoint2 = transform.FindChild ("6");
		Transform PunchPoint3 = transform.FindChild ("7");
		Transform PunchPoint4 = transform.FindChild ("8");
		Transform PunchPoint4a = transform.FindChild ("8a");

		GameObject tempFire5 = Instantiate(fire, PunchPoint1.position, Quaternion.identity) as GameObject;
		GameObject tempFire6 = Instantiate(fire, PunchPoint2.position, Quaternion.identity) as GameObject;
		GameObject tempFire7 = Instantiate(fire, PunchPoint3.position, Quaternion.identity) as GameObject;
		GameObject tempFire8 = Instantiate(fire, PunchPoint4.position, Quaternion.identity) as GameObject;
		GameObject tempFire8a = Instantiate(fire, PunchPoint4a.position, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		//tempFire.GetComponent<fire>().playerID=playerNumber;
		//tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	public void frontAttack(){
		//Left Swing
		//attackSpawnDistance = 3;
		Transform PunchPoint1 = transform.FindChild ("front1");
		Transform PunchPoint2 = transform.FindChild ("front2");
		Transform PunchPoint3 = transform.FindChild ("front3");
		Transform PunchPoint4 = transform.FindChild ("front4");
		//Transform PunchPoint4a = transform.FindChild ("8a");
		
		GameObject tempFire5 = Instantiate(fire, PunchPoint1.position, Quaternion.identity) as GameObject;
		GameObject tempFire6 = Instantiate(fire, PunchPoint2.position, Quaternion.identity) as GameObject;
		GameObject tempFire7 = Instantiate(fire, PunchPoint3.position, Quaternion.identity) as GameObject;
		GameObject tempFire8 = Instantiate(fire, PunchPoint4.position, Quaternion.identity) as GameObject;
		//GameObject tempFire8a = Instantiate(fire, PunchPoint4a.position, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		//tempFire.GetComponent<fire>().playerID=playerNumber;
		//tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	//public void frontAttackPowered(){
	IEnumerator frontAttackPowered() {
	//Left Swing

		//attackSpawnDistance = 3;
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontA"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontB"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontC"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontD"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontE"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontF"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontG"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("frontH"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front1"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front2"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front3"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front4"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front5"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front6"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front7"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front8"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front9"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front10"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front11"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front12"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front13"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front14"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front15"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front16"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front17"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front18"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front19"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front20"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front21"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front22"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front23"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("front24"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward1"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward2"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward3"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward4"), 0f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward5"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward6"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward7"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward8"), .1f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward9"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward10"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward11"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward12"), .2f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward13"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward14"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward15"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward16"), .3f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward17"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward18"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward19"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward20"), .4f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward21"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward22"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward23"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward24"), .5f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward25"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward26"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward27"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward28"), .6f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward29"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward30"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward31"), .7f));
		StartCoroutine(launchFrontAttackPiece(frontAttackContainer.FindChild ("DuoForward32"), .7f));
		yield break;
//		Transform PunchPointA = frontAttackContainer.FindChild ("frontA");
//		Transform PunchPointB = frontAttackContainer.FindChild ("frontB");
//		Transform PunchPointC = frontAttackContainer.FindChild ("frontC");
//		Transform PunchPointD = frontAttackContainer.FindChild ("frontD");
//		Transform PunchPointE = frontAttackContainer.FindChild ("frontE");
//		Transform PunchPointF = frontAttackContainer.FindChild ("frontF");
//		Transform PunchPointG = frontAttackContainer.FindChild ("frontG");
//		Transform PunchPointH = frontAttackContainer.FindChild ("frontH");
//		Transform PunchPoint1 = frontAttackContainer.FindChild ("front1");
//		Transform PunchPoint2 = frontAttackContainer.FindChild ("front2");
//		Transform PunchPoint3 = frontAttackContainer.FindChild ("front3");
//		Transform PunchPoint4 = frontAttackContainer.FindChild ("front4");
//		Transform PunchPoint5 = frontAttackContainer.FindChild ("front5");
//		Transform PunchPoint6 = frontAttackContainer.FindChild ("front6");
//		Transform PunchPoint7 = frontAttackContainer.FindChild ("front7");
//		Transform PunchPoint8 = frontAttackContainer.FindChild ("front8");
//		Transform PunchPoint9 = frontAttackContainer.FindChild ("front9");
//		Transform PunchPoint10 = frontAttackContainer.FindChild ("front10");
//		Transform PunchPoint11 = frontAttackContainer.FindChild ("front11");
//		Transform PunchPoint12 = frontAttackContainer.FindChild ("front12");
//		Transform PunchPoint13 = frontAttackContainer.FindChild ("front13");
//		Transform PunchPoint14 = frontAttackContainer.FindChild ("front14");
//		Transform PunchPoint15 = frontAttackContainer.FindChild ("front15");
//		Transform PunchPoint16 = frontAttackContainer.FindChild ("front16");
//		Transform PunchPoint17 = frontAttackContainer.FindChild ("front17");
//		Transform PunchPoint18 = frontAttackContainer.FindChild ("front18");
//		Transform PunchPoint19 = frontAttackContainer.FindChild ("front19");
//		Transform PunchPoint20 = frontAttackContainer.FindChild ("front20");
//		Transform PunchPoint21 = frontAttackContainer.FindChild ("front21");
//		Transform PunchPoint22 = frontAttackContainer.FindChild ("front22");
//		Transform PunchPoint23 = frontAttackContainer.FindChild ("front23");
//		Transform PunchPoint24 = frontAttackContainer.FindChild ("front24");
//		//Transform PunchPoint4a = transform.FindChild ("8a");
//
//		GameObject tempFireA = Instantiate(fire, PunchPointA.position, Quaternion.identity) as GameObject;
//		tempFireA.transform.SetParent(null);
//		GameObject tempFireB = Instantiate(fire, PunchPointB.position, Quaternion.identity) as GameObject;
//		tempFireB.transform.SetParent(null);
//		GameObject tempFireC = Instantiate(fire, PunchPointC.position, Quaternion.identity) as GameObject;
//		tempFireC.transform.SetParent(null);
//		GameObject tempFireD = Instantiate(fire, PunchPointD.position, Quaternion.identity) as GameObject;
//		tempFireD.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFireE = Instantiate(fire, PunchPointE.position, Quaternion.identity) as GameObject;
//		tempFireE.transform.SetParent(null);
//		GameObject tempFireF = Instantiate(fire, PunchPointF.position, Quaternion.identity) as GameObject;
//		tempFireF.transform.SetParent(null);
//		GameObject tempFireG = Instantiate(fire, PunchPointG.position, Quaternion.identity) as GameObject;
//		tempFireG.transform.SetParent(null);
//		GameObject tempFireH = Instantiate(fire, PunchPointH.position, Quaternion.identity) as GameObject;
//		tempFireH.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire1 = Instantiate(fire, PunchPoint1.position, Quaternion.identity) as GameObject;
//		tempFire1.transform.SetParent(null);
//		GameObject tempFire2 = Instantiate(fire, PunchPoint2.position, Quaternion.identity) as GameObject;
//		tempFire2.transform.SetParent(null);
//		GameObject tempFire3 = Instantiate(fire, PunchPoint3.position, Quaternion.identity) as GameObject;
//		tempFire3.transform.SetParent(null);
//		GameObject tempFire4 = Instantiate(fire, PunchPoint4.position, Quaternion.identity) as GameObject;
//		tempFire4.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire5 = Instantiate(fire, PunchPoint5.position, Quaternion.identity) as GameObject;
//		tempFire5.transform.SetParent(null);
//		GameObject tempFire6 = Instantiate(fire, PunchPoint6.position, Quaternion.identity) as GameObject;
//		tempFire6.transform.SetParent(null);
//		GameObject tempFire7 = Instantiate(fire, PunchPoint7.position, Quaternion.identity) as GameObject;
//		tempFire7.transform.SetParent(null);
//		GameObject tempFire8 = Instantiate(fire, PunchPoint8.position, Quaternion.identity) as GameObject;
//		tempFire8.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire9 = Instantiate(fire, PunchPoint9.position, Quaternion.identity) as GameObject;
//		tempFire9.transform.SetParent(null);
//		GameObject tempFire10 = Instantiate(fire, PunchPoint10.position, Quaternion.identity) as GameObject;
//		tempFire10.transform.SetParent(null);
//		GameObject tempFire11 = Instantiate(fire, PunchPoint11.position, Quaternion.identity) as GameObject;
//		tempFire11.transform.SetParent(null);
//		GameObject tempFire12 = Instantiate(fire, PunchPoint12.position, Quaternion.identity) as GameObject;
//		tempFire12.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire13 = Instantiate(fire, PunchPoint13.position, Quaternion.identity) as GameObject;
//		tempFire13.transform.SetParent(null);
//		GameObject tempFire14 = Instantiate(fire, PunchPoint14.position, Quaternion.identity) as GameObject;
//		tempFire14.transform.SetParent(null);
//		GameObject tempFire15 = Instantiate(fire, PunchPoint15.position, Quaternion.identity) as GameObject;
//		tempFire15.transform.SetParent(null);
//		GameObject tempFire16 = Instantiate(fire, PunchPoint16.position, Quaternion.identity) as GameObject;
//		tempFire16.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire17 = Instantiate(fire, PunchPoint17.position, Quaternion.identity) as GameObject;
//		tempFire17.transform.SetParent(null);
//		GameObject tempFire18 = Instantiate(fire, PunchPoint18.position, Quaternion.identity) as GameObject;
//		tempFire18.transform.SetParent(null);
//		GameObject tempFire19 = Instantiate(fire, PunchPoint19.position, Quaternion.identity) as GameObject;
//		tempFire19.transform.SetParent(null);
//		GameObject tempFire20 = Instantiate(fire, PunchPoint20.position, Quaternion.identity) as GameObject;
//		tempFire20.transform.SetParent(null);
//		yield return new WaitForSeconds(.1f);
//		GameObject tempFire21 = Instantiate(fire, PunchPoint21.position, Quaternion.identity) as GameObject;
//		tempFire21.transform.SetParent(null);
//		GameObject tempFire22 = Instantiate(fire, PunchPoint22.position, Quaternion.identity) as GameObject;
//		tempFire22.transform.SetParent(null);
//		GameObject tempFire23 = Instantiate(fire, PunchPoint23.position, Quaternion.identity) as GameObject;
//		tempFire23.transform.SetParent(null);
//		GameObject tempFire24 = Instantiate(fire, PunchPoint24.position, Quaternion.identity) as GameObject;
//		tempFire24.transform.SetParent(null);
		//GameObject tempFire8a = Instantiate(fire, PunchPoint4a.position, Quaternion.identity) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		//tempFire.GetComponent<fire>().playerID=playerNumber;
		//tempFire.GetComponent<fire>().setDirection(xFacing, zFacing, 6);
		//attackCooldown = lightAttackCooldown;
	}

	IEnumerator launchFrontAttackPiece(Transform transform, float delay) {
		Vector3 pos = transform.position;
		Quaternion rot = transform.rotation;
		yield return new WaitForSeconds(delay);
		GameObject newFire = Instantiate(fire2, pos, rot) as GameObject;
		newFire.transform.parent = null;
	}

	public IEnumerator lightCooldown() {
		yield return new WaitForSeconds(50f);

	}





	public void lightDodge() {

				Transform DodgePoint1 = transform.FindChild ("Dodge1");
				Transform DodgePoint2 = transform.FindChild ("Dodge2");
				Transform DodgePoint3 = transform.FindChild ("Dodge3");
				Transform DodgePoint4 = transform.FindChild ("Dodge4");
				Transform DodgePoint5 = transform.FindChild ("Dodge5");
		
				GameObject tempFire1 = Instantiate (fire3, DodgePoint1.position, Quaternion.identity) as GameObject;
				GameObject tempFire2 = Instantiate (fire3, DodgePoint2.position, Quaternion.identity) as GameObject;
				GameObject tempFire3 = Instantiate (fire3, DodgePoint3.position, Quaternion.identity) as GameObject;
				GameObject tempFire4 = Instantiate (fire3, DodgePoint4.position, Quaternion.identity) as GameObject;
				GameObject tempFire5 = Instantiate (fire3, DodgePoint5.position, Quaternion.identity) as GameObject;
		}

	public void aoe() {
				Transform aoePoint1 = transform.FindChild ("aoe");
				GameObject tempFire1 = Instantiate (fire2, aoePoint1.position, Quaternion.identity) as GameObject;
		}

	public void circleSpawn(){
		attackSpawnDistance = 10;
		Vector3 myFirePoint = new Vector3(gameObject.transform.position.x+attackSpawnDistance*xFacing, gameObject.transform.position.y, gameObject.transform.position.z+attackSpawnDistance*zFacing);
		
		GameObject tempFire = Instantiate(fire3, myFirePoint, Quaternion.Euler (90, 0, 0)) as GameObject;
		GameObject tempFire2 = Instantiate(fire4, myFirePoint, Quaternion.Euler (0, 0, 0)) as GameObject;
		//tempFire.audio.clip = mySoundinventory.heavyAttackSound;
		//tempFire.GetComponent<fire3>().playerID=playerNumber;
		//tempFire.GetComponent<fire3>().setDirection(xFacing, zFacing, 6);	
		}

	public void detectorPatterns() {
				//Transform DetectorPoint1 = transform.GameObject.Find ("Detector1");
				//Transform DetectorPoint2 = transform.GameObject.Find ("Detector2");
				//Transform DetectorPoint3 = transform.GameObject.Find ("Detector3");
				//Transform DetectorPoint4 = transform.GameObject.Find ("Detector4");
				//Transform DetectorPoint5 = transform.GameObject.Find ("Detector5");
		
				//GameObject tempFire1 = Instantiate (fire4, DetectorPoint1.position, Quaternion.identity) as GameObject;
				//GameObject tempFire2 = Instantiate (fire4, DetectorPoint2.position, Quaternion.identity) as GameObject;
				//GameObject tempFire3 = Instantiate (fire4, DetectorPoint3.position, Quaternion.identity) as GameObject;
				//GameObject tempFire4 = Instantiate (fire4, DetectorPoint4.position, Quaternion.identity) as GameObject;
				//aGameObject tempFire5 = Instantiate (fire4, DetectorPoint5.position, Quaternion.identity) as GameObject;

	}

	//public void OnTriggerEnter(Collider other){
		//Debug.Log ("colliding with: "+other.tag);
		//if(other.tag == "Boundary"){
		//	Application.LoadLevel(2);
	//	}

	//}

	//public void OnCollisionEnter(Collision other){
		//Debug.Log ("colliding with: "+other.gameObject.tag);
		//if(other.gameObject.tag == "Boundary"){
		//	Application.LoadLevel(2);
	//	}
		
	//}
}
