using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {

	GameObject[] Miners;
	public GameObject original;
	private Grid grid;

	void Awake () {
		Miners = GameObject.FindGameObjectsWithTag("Miner");
		grid = GetComponent<Grid>();
	}
	

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			bool found = false;
			for(int i = 0; i < Miners.Length - 1; i++){
				if(Miners[i].GetComponent<Unit_Labor>().state == Unit_Labor.State.Idle){
					found = true;
					Debug.Log("Idle Worker");
					Miners[i].GetComponent<Unit_Labor>().InsertQueue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
					break;
				}

			}
			if(!found){
				GameObject lessBusyMiner = Miners[0];
				for(int i = 0; i < Miners.Length; i++){
					if(Miners[i].GetComponent<Unit_Labor>().state == Unit_Labor.State.Working){
						if(lessBusyMiner != null & Miners[i].GetComponent<Unit_Labor>() != null){
							if(Miners[i].GetComponent<Unit_Labor>().work.Count < lessBusyMiner.GetComponent<Unit_Labor>().work.Count){
								lessBusyMiner = Miners[i];
							}
						}
							



					}
					
				}
				lessBusyMiner.GetComponent<Unit_Labor>().InsertQueue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}

		}

		if(Input.GetMouseButtonDown(1)){
		
			GameObject clone = (GameObject)Instantiate(original,Camera.main.ScreenToWorldPoint(Input.mousePosition),original.transform.rotation);
			clone.transform.position = new Vector3(clone.transform.position.x,clone.transform.position.y,0);
			clone.transform.localScale = new Vector3(100,100,1);
			grid.CreateGrid();
		}
	
	}
}
