using UnityEngine;
using System.Collections;

public class playerThreeController : playerController {

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	public override void comboCheck(){
		base.comboCheck();
		if(otherPlayer.GetComponent<playerTwoController>().comboable==true && comboDistance.maxComboDistance>comboDistance.distanceBetweenPlayers){
			Debug.Log("Combo!");
			comboController.castMeteor();
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
		if(Input.GetKeyDown(KeyCode.RightShift)||Input.GetButtonDown("Fire1"))
		{
			if(attackCooldown<1)
			{
				Debug.Log("right shift pushed");
				lightAttack();
			}
		}
		
		if(Input.GetKey(KeyCode.RightShift)||Input.GetButton("Fire1"))
		{
			charge= charge+Time.deltaTime;
			
		}
		
		if(Input.GetKeyUp(KeyCode.RightShift)||Input.GetButtonUp("Fire1"))
		{
			if(charge>maxCharge){
				comboCounter = comboWindow;
				comboable = true;
				//	heavyAttack();
			}
			charge=0;
		}
	}
}
