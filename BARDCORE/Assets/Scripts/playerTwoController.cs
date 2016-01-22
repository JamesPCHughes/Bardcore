using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class playerTwoController : playerController {
	public GameObject object1;
	public GameObject object2;
	public GameObject object3;
	public GameObject P1GameObject;

	float primaryAttack;
	public static bool p2Attacking;
	public bool p1AttackingCopy;

	public static bool p2HoldingA;
	public static bool p2HoldingB;
	public static bool p2HoldingX;
	public static bool p2HoldingY;
	public static bool p2HoldingLB;
	public static bool p2HoldingRB;

	public GameObject EnergyYP2;
	public GameObject EnergyBP2;
	public GameObject EnergyRB2;

	public bool p1HoldA;
	public bool p1HoldB;
	public bool p1HoldX;
	public bool p1HoldY;
	public bool p1HoldLB;
	public bool p1HoldRB;

	public bool P2in;
	public bool P1in;
	public float powerCharge;
	public bool Duo1Checked;

	public BeatTimer beat; 
	 
	public bool beatbool = false; 
	//public BeatTimer timer;
	public Material material1;
	public Material material2;
	public Material material3;
	public Renderer rend;
	public AudioSource Drums;
	public AudioSource Bass;
	public AudioSource Guitar;
	public AudioSource Vox;
	public GameObject Sword;
	//public GameObject Sword2;
	private Animator myAnimator;
	private float Saiyan1;
	public bool FirstHit;
	public bool ExtraHit;
	public bool ExtraHit1;
	public int Jab1;
	public int ComboCount;

	//private bool Saiyan2;
	//private bool Saiyan3;
	//private bool Saiyan4;
	private Rigidbody rb;	
	// Use this for initialization
	void Awake () {
		Duo1Checked = Duo1Check.Duo1Checker;
		p1AttackingCopy = PlayerWonController.p1Attacking;
		p1HoldA = PlayerWonController.p1HoldingA;
		p1HoldB = PlayerWonController.p1HoldingB;
		p1HoldX = PlayerWonController.p1HoldingX;
		p1HoldY = PlayerWonController.p1HoldingY;
		p1HoldLB = PlayerWonController.p1HoldingLB;
		p1HoldRB = PlayerWonController.p1HoldingRB;
		P2in = PlayerDetectManager.p2IsIn;
		P1in = PlayerDetectManager.p1IsIn;
		
	}

	void Start () {
		p2Attacking = false;

		P1GameObject = GameObject.FindGameObjectWithTag("Player");


		beat = FindObjectOfType<BeatTimer> ();
		//rend = GetComponent<Renderer>();
		FirstHit = true;
		ExtraHit1 = false; 
		ExtraHit = false; 
		rend.enabled = true;
		Jab1 = 1;
		myAnimator = GetComponentInChildren<Animator>();

		base.Start();
		playerNumber=2;

		Drums.volume = (0.0f);
		Bass.volume = (0.0f);
		Guitar.volume = (0.0f);
		Vox.volume = (0.0f);
	
		Saiyan1 = 0;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		primaryAttack = Input.GetAxis ("P2TriggerR");
		P2in = PlayerDetectManager.p2IsIn;
		P1in = PlayerDetectManager.p1IsIn;
		p1AttackingCopy = PlayerWonController.p1Attacking;
		p1HoldA = PlayerWonController.p1HoldingA;
		p1HoldB = PlayerWonController.p1HoldingB;
		p1HoldX = PlayerWonController.p1HoldingX;
		p1HoldY = PlayerWonController.p1HoldingY;
		p1HoldLB = PlayerWonController.p1HoldingLB;
		p1HoldRB = PlayerWonController.p1HoldingRB;

		Duo1Checked = Duo1Check.Duo1Checker;
		//beatbool = beat.onBeat; 
		beatbool = true;
	
		base.Update();

			if((P1in) && (P2in) && (powerCharge == 1f)) {
			powerCharge =2f;
			StartCoroutine(powerLevel());
		}

		if (Saiyan1 < 1) {
						Drums.volume = (0.0f);
						Bass.volume = (0.0f);
						Guitar.volume = (0.0f);
						Vox.volume = (0.0f);
				}

		//if(animation_bool == true)
		//{
		//	animation.Play("SwordSwing");
			
		//}
		//base.Update();
	}



	public override void comboCheck(){
		//if(comboable){
		base.comboCheck();
		Debug.Log("Player 2 combochecking");
		
		if(comboCooldown.cooldown>0){
			comboable=false;
		}

		if(otherPlayer.GetComponent<playerOneController>().comboable==true  && comboDistance.maxComboDistance>comboDistance.distanceBetweenPlayers){
				Debug.Log("Combo!");
				movementCooldown=2.5f;
				otherPlayer.GetComponent<playerOneController>().movementCooldown=2.5f;

				comboController.castMeteor();
				comboCounter = 0;
				comboable = false;
				comboCooldown.cooldown = 30;				
			}

		if(comboCounter<1 && comboCooldown.cooldown<1){
				heavyAttack();
				comboCounter=0;
				comboable = false;
			}

		else{
				comboCounter--;
			}

			//}
	}
		IEnumerator resetComboChain() {
		yield return new WaitForSeconds(5f);
		Jab1 = 1;

	}

		IEnumerator lightCooldown() {
		yield return new WaitForSeconds(.24f);
		Jab1 = 2;

		}

		IEnumerator lightCooldown1() {
		yield return new WaitForSeconds(.24f);
		Jab1 = 1;

		}

		IEnumerator isP2Attacking() {
			yield return new WaitForSeconds(.24f);
			p2Attacking = false;
		}

		IEnumerator powerLevel() {
		yield return new WaitForSeconds(10f);
		powerCharge = 1;

	}

	public override void faceCheck(){
		base.faceCheck();
		//Debug.Log("xFacing: "+xFacing+" zFacing: "+zFacing);
		if(Input.GetKey(KeyCode.W)||Input.GetAxis("Vertical1")>0f){
			zFacing = 1;		
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			//new
			transform.forward = new Vector3(0f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.D)||Input.GetAxis("Horizontal1")>0f){
			xFacing = 1;
			zFacing = 0;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(1f, 0f, 0f);

		}
		
		if(Input.GetKey(KeyCode.S)||Input.GetAxis("Vertical1")<00f){
			zFacing = -1;
			xFacing = 0;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.A)||Input.GetAxis("Horizontal1")<0f){
			xFacing=-1;
			zFacing=0;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(-1f, 0f, 0f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1")<0&&Input.GetAxis("Horizontal1")<0){
			zFacing = -1;
			xFacing = -1;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1")<0&&Input.GetAxis("Horizontal1")>0){
			zFacing = -1;
			xFacing = 1;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1")>0&&Input.GetAxis("Horizontal1")>0){
			zFacing = 1;
			xFacing = 1;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1")>0&&Input.GetAxis("Horizontal1")<0){
			zFacing = 1;
			xFacing = -1;
			//GetComponent<Rigidbody>().AddForce(transform.forward * 2);
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, 1f);
		}
		if(Input.GetKey(KeyCode.W)||Input.GetAxis("Vertical1Right")>0f){
			//zFacing = 1;		
			//xFacing = 0;
			//movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.D)||Input.GetAxis("Horizontal1Right")>0f){
			//xFacing = 1;
			//zFacing = 0;
			//movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(1f, 0f, 0f);
			
		}
		
		if(Input.GetKey(KeyCode.S)||Input.GetAxis("Vertical1Right")<00f){
			//zFacing = -1;
			//xFacing = 0;
			//movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.A)||Input.GetAxis("Horizontal1Right")<0f){
			//xFacing=-1;
			//zFacing=0;
			//movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(-1f, 0f, 0f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1Right")<0&&Input.GetAxis("Horizontal1Right")<0){
			//zFacing = -1;
			//xFacing = -1;
			//movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1Right")<0&&Input.GetAxis("Horizontal1Right")>0){
			//zFacing = -1;
			//xFacing = 1;
			//movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1Right")>0&&Input.GetAxis("Horizontal1Right")>0){
			//zFacing = 1;
			//xFacing = 1;
			//movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1Right")>0&&Input.GetAxis("Horizontal1Right")<0){
			//zFacing = 1;
			//xFacing = -1;
			//movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, 1f);
		}
	}

	public override void attackCheck(){
				base.attackCheck ();

		//if ((Input.GetButtonDown ("Fire2")) && (ExtraHit1 == true) && (Input.GetButtonDown ("Punch2")) && (beatbool == true) && (P1in == true) || (P2in == true)) {
			//if (Input.GetButtonDown ("Fire") && Input.GetButtonDown("Punch")) {	
			//	object3.transform.position = 0.5f*(object1.transform.position + object2.transform.position);
			//	GameObject tempFire1 = Instantiate (object3, object3.transform.position, Quaternion.identity) as GameObject;
			//	}
		if (Input.GetButton ("P2Y")) {
			p2HoldingY = true;
			EnergyYP2.SetActive(true);

			//aoe ();
		} else {
			p2HoldingY = false; 
			EnergyYP2.SetActive(false);
		}
		
		if (Input.GetButton ("P2X")) {
			p2HoldingX = true;

			//aoe ();
		} else {
			p2HoldingX = false; 
		}
		
		if ((Input.GetButton ("P2B"))) {
			p2HoldingB = true;
			p2Attacking = true;
			EnergyBP2.SetActive(true);
		
			
		}
		else {
			p2HoldingB = false;
			p2Attacking = false;
			EnergyBP2.SetActive(false);
		}
		
		if ((Input.GetButton ("P2A"))) {
			p2HoldingA = true;
			
		}
		else {
			p2HoldingA = false;
		}

		if ((Input.GetButton ("P2LB"))) {
			p2HoldingLB = true;

		} else {
			p2HoldingLB = false;

		}
		if ((Input.GetButton ("P2RB"))) {
			p2HoldingRB = true;
			EnergyRB2.SetActive (true);
		} else {
			p2HoldingRB = false;
			EnergyRB2.SetActive (false);
		}

		if (Input.GetButtonDown ("Fire4")) {
						aoe ();
				}

		//if (primaryAttack > 1f) {
			//lightDodge();
			//rb.transform.Translate (Vector3.forward * 10);
			//lightDodge();
		//}
		// the below is the second part of the combo
		/*if ((Input.GetButtonDown ("Fire2")) && (ExtraHit1 == true)) {

			lightAttack3 ();
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = false;
			ExtraHit = false;

		}*/ 

		/*if ( (Input.GetButtonDown ("Fire2")) && (ExtraHit == true) && (beatbool == true)) {
			//if ( (Input.GetKeyDown (KeyCode.Space)) && (attackCooldown > 1) && (attackCooldown < 1.5) && (ExtraHit == true)) {
			Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack2 ();
			lightAttack ();
			detectorPatterns();
			//circleSpawn();
			ExtraHit1 = true; 
			ExtraHit = false;
			FirstHit = false;
		}*/
		/*if ( (Input.GetButtonDown ("Fire2")) && (ExtraHit == true) && (beatbool == false) ) {
			//if ( (Input.GetKeyDown (KeyCode.Space)) && (attackCooldown > 1) && (attackCooldown < 1.5) && (ExtraHit == true)) {
			Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack2 ();
			lightAttack ();
			//circleSpawn();
			ExtraHit1 = true; 
			ExtraHit = false;
			FirstHit = false;
		}*/
		//if (Input.GetButtonDown ("Fire") && (P1in == true) && (P2in == true)) {
		if (Input.GetButtonDown ("P2A") && p1HoldA == true) {
			StartCoroutine("lightJab");
			comboConnectP2();
		}
		else if (Input.GetButtonDown ("P2A")){
			StartCoroutine("lightJab");
			}

		if (Input.GetButtonDown ("P2X") && p1HoldX == true) {
			StartCoroutine("frontAttackPowered");			
			StartCoroutine("comboX");


		}

		else if (Input.GetButtonDown ("P2X")) {
			StartCoroutine("comboX");
		}

		if (Input.GetButtonDown ("P2LB") && p1HoldRB == true && beatbool == true) {
			
			comboConnectP2 ();	
			
		}

		if ((Input.GetButtonDown ("P2A")) && (beatbool == true) && (Duo1Checked == true) && (p1AttackingCopy == true)) { 
		//circleSpawn();
			//frontAttack ();

			StartCoroutine("frontAttackPowered");
			//rb.transform.Translate (Vector3.forward * -1);
			//lightAttack5 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			//Jab1 += 1;
			ComboCount += 1;
			
			
		}

		if (Input.GetButtonDown ("P2X") && p1HoldY == true && beatbool == true) {
			lightDodge();
			gameObject.transform.position = (P1GameObject.transform.position);		
			lightDodge ();
		}
		else if (Input.GetButtonDown ("P2X")) {
			//lightDodge();
			//rb.transform.Translate (Vector3.forward * 10);
		//	lightDodge();
		}

		if ((Input.GetKey (KeyCode.C) || Input.GetButtonDown("P2A")) && (beatbool == true) && (p1HoldB == true) && (Jab1 == 1)) {
			//circleSpawn();
			//myAnimator.SetBool ("Attack", true);
			//rb.transform.Translate (Vector3.forward * 3);
			StartCoroutine("frontAttackPowered");			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p2Attacking = true;

			StartCoroutine(isP2Attacking());
			StartCoroutine(lightCooldown());
			//StartCoroutine(resetComboChain());
			


				}

		else if (( Input.GetKey (KeyCode.C) || Input.GetButtonDown("P2A")) && (beatbool == true) && (p1HoldB == true) && (Jab1 == 2)) {
			//circleSpawn();
			//myAnimator.SetBool ("Attack", true);
			//rb.transform.Translate (Vector3.forward * 3);
			StartCoroutine("frontAttackPowered");			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p2Attacking = true;
			StartCoroutine(isP2Attacking());
			StartCoroutine(lightCooldown1());
			



		}


		//basic light attack
		//if ((primaryAttack == 1 || Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("P2A")) && (Jab1 == 1)) {
			
			/*if ((Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("P2A")) && (Jab1 == 1)) {
			
			rb.transform.Translate (Vector3.forward * 1);
			lightJab ();
						
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p2Attacking = true;

			StartCoroutine(isP2Attacking());
			StartCoroutine(lightCooldown());
			
			


				}
		//basic light attack second swing
		else if ((Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("P2A")) && (Jab1 == 2)) {
			
			rb.transform.Translate (Vector3.forward * 1);
			lightJab();
						
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p2Attacking = true;
			StartCoroutine(isP2Attacking());
			StartCoroutine(lightCooldown1());
			



		}*/



		




				//if ((attackCooldown > 1.5) && (attackCooldown < 2)) {
				//		rend.sharedMaterial = material2;
				//} else { 
				//		rend.sharedMaterial = material1;
				//}
				//if ((attackCooldown > 2) && (attackCooldown < 2.5)) {
				//		rend.sharedMaterial = material3;
				//}
				//Debug.Log ("charge: "+charge);
		//if (Input.GetKeyDown (KeyCode.V) || Input.GetButtonDown ("Fire3")) {
			//if ((attackCooldown > 2.25) && (attackCooldown < 2.9)) {
			//Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			//lightAttack2 ();
		//}

				/*if (Input.GetKeyDown (KeyCode.Z) || Input.GetButtonDown ("Fire2")) {





						if ((Saiyan1 < 1)) {
						//if ((attackCooldown < 1) && (Saiyan1 < 1)) {
					
								Debug.Log ("FIRE pushed");
				ExtraHit = true;
				ExtraHit1 = false;
								Drums.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 1)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Bass.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 2)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Guitar.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 3)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Vox.volume = (1.0f);
								Saiyan1 = 0;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						//GetComponent<AudioSource>().PlayOneShot(Guitar1, 0.7F);

						//Sword.GetComponent<Animation>().Play("SwordSwing");
						//Sword.gameObject.animation.Play("SwordSwing");
						//if (charAnimator.GetBool("die"))
						//	charAnimator.SetBool("die", false);	
						//Sword.GetComponent<Animator>().bool	
						//REINSTATEmyAnimator.SetBool ("Swing", true);
						//Swing = true;
						

					
					
					


						//REINSTATElightAttack ();
				//}
			//if (Input.GetKeyDown (KeyCode.V) || Input.GetButtonDown ("Fire3")) {




	
						if ((attackCooldown > 1.5) && (attackCooldown < 2)) {
								Debug.Log ("Fire 2 pushed");
				//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
				//myAnimator.SetBool ("Swing2", true);
							//	lightAttack2 ();
				//ExtraHit1 = true; 

						}
		
				} else {
			myAnimator.SetBool ("Attack", false);			
			//myAnimator.SetBool ("Swing", false);
			myAnimator.SetBool ("Swing2", false);
			myAnimator.SetBool ("Swing3", false);
				}
		
		if(Input.GetKey(KeyCode.Space)||Input.GetButton("Fire2"))
		{
			Debug.Log("Player two charge at: "+charge);
			charge= charge+Time.deltaTime;
			
		}
		
		if(Input.GetKeyUp(KeyCode.Space)||Input.GetButtonUp("Fire2"))
		{
			Debug.Log("Player two FIRE released");
			if(charge>maxCharge){
				Debug.Log("Player two super fireball");
				comboCounter = comboWindow;
				comboable = true;
				//	heavyAttack();
			}
			charge=0;
		}*/
	}

}
