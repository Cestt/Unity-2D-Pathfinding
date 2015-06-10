using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {

	GameObject[] Miners;

	void Awake () {
		Miners = new GameObject[25];
		Miners = GameObject.FindGameObjectsWithTag("Miner");
	}
	

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			for(int i = 0; i < Miners.Length; i++){
				if(Miners[i].GetComponent<Unit_Labor>().state == Unit_Labor.State.Idle){
					Miners[i].GetComponent<Unit_Labor>().InsertQueue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
					break;
				}
			}

		}
	
	}
}
