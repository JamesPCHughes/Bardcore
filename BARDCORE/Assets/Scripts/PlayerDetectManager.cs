using UnityEngine;
using System.Collections;

public static class PlayerDetectManager {

	public static bool p2IsIn = false;
	public static bool p1IsIn = false; 
	



	public static void SetP2IsIn (bool inside) {
		p2IsIn = inside;

	}

	public static void SetP1IsIn (bool inside1) {
				p1IsIn = inside1;
		}

	
}
