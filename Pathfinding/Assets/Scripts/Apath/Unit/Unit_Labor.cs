using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit_Labor : MonoBehaviour {

	[HideInInspector]
	public State state = State.Idle;

	public Queue<Vector2> work = new Queue<Vector2>();
	

	private Unit unit;

	void Awake(){
		unit = GetComponent<Unit>();
	}

	void Update(){

	}

	public void InsertQueue (Vector2 tposition) {
		work.Enqueue(tposition);
		Next();
	}
	void Next(){
		StopCoroutine("TryNext");
		StartCoroutine("TryNext");
	}

	IEnumerator TryNext(){

		if(work.Count > 0 & !unit.procesing){
			unit.PathTry(work.Dequeue(),StateChange,Next);
			yield return null;
		}else if(work.Count > 0){
			yield return null;
			Next();
		}else if(work.Count == 0 & !unit.procesing){
			yield return null;
			StateChange("Idle");
		}
		yield return null;
	}

	void StateChange(string states){
		
		switch (states){
		case "Working" :
			state = State.Working;
			break;
		case "Idle" :
			state = State.Idle;
			break;
		default :
			state = State.Idle;
			break;
		}
		
	}

	public enum State{
		Idle, Working
	}
}
