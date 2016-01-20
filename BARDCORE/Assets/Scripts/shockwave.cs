using UnityEngine;
using System.Collections;

public class shockwave : expirable {

	public AnimationCurve xCurve;
	public float rateOfExpansion = 1.01f;

	public override void Update(){
		base.Update();
					
			Vector3 tempVect = gameObject.transform.localScale;
			//tempVect = new Vector3(xCurve.Evaluate(Time.deltaTime),xCurve.Evaluate(Time.deltaTime),xCurve.Evaluate(Time.deltaTime));
			tempVect *= rateOfExpansion;	
			gameObject.transform.localScale = tempVect;
			

	}
}
