using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {

	public Vector3 oldPosition;

	void Update () {
		if(oldPosition != transform.position){
			oldPosition = transform.position;
		}
	}
}
