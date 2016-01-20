using UnityEngine;
using System.Collections;

public class playerOneController : playerController {
	public BeatTimer beat; 
	public bool beatbool = false; 
	public bool FirstHit;
	public bool ExtraHit;
	public bool ExtraHit1;
	private Rigidbody rb;
	public int Jab1;
	// Use this for initialization
	void Start () {
		base.Start();
		playerNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//beatbool = beat.onBeat; 
		//base.Update();
	}
	
	public override void comboCheck(){
		base.comboCheck();
		if(otherPlayer.GetComponent<playerTwoController>().comboable==true && comboDistance.maxComboDistance>comboDistance.distanceBetweenPlayers){
			Debug.Log("Combo!");
			comboController.castMeteor();
			movementCooldown=2.5f;
			otherPlayer.GetComponent<playerTwoController>().movementCooldown=2.5f;
			//otherPlayer.GetComponent<playerOneController>().movementCooldown=0f;
			comboCounter = 0;
			comboable = false;
			comboCooldown.cooldown = 30;
		}
		
		if(comboCounter<1 && comboCooldown.cooldown<1){
			
			Debug.Log("combo cooldown is: "+comboCooldown.cooldown);
			heavyAttack();
			comboCounter=0;
			comboable = false;
		}
		
		else{
			comboCounter--;
		}
	}
	
	public override void faceCheck(){
		//Debug.Log("xFacing: "+xFacing+" zFacing: "+zFacing);
		base.faceCheck();
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
	}
	
	public override void attackCheck(){
		base.attackCheck ();
		
		if ((Input.GetButtonDown ("Punch2")) && (ExtraHit1 == true)) {
			lightAttack3 ();
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = false;
			ExtraHit = false;
			
		}
		
		if ( (Input.GetButtonDown ("Punch2")) && (ExtraHit == true)) {
			//if ( (Input.GetKeyDown (KeyCode.Space)) && (attackCooldown > 1) && (attackCooldown < 1.5) && (ExtraHit == true)) {
			Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 3);
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = true; 
			ExtraHit = false;
			FirstHit = false;


		}
		
		
		if (Input.GetButtonDown ("Punch") && (Jab1 == 1)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack5 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 += 1;
		}
		
		else if (Input.GetButtonDown ("Punch") && (Jab1 == 2)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack6 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 1;
		}
	}
}
