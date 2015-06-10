using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit_Labor : MonoBehaviour {

	[HideInInspector]
	public State state = State.Idle;

	public Queue<Vector2> work = new Queue<Vector2>();

	public int count;

	private Unit unit;

	void Awake(){
		unit = GetComponent<Unit>();
	}

	void Update(){
		count = work.Count;
	}

	public void InsertQueue (Vector2 tposition) {
		work.Enqueue(tposition);
		TryNext();
	}
	
	void TryNext(){
		if(work.Count > 0 & !unit.procesing){
			unit.PathTry(work.Dequeue(),StateChange,TryNext);
		}else if(!unit.procesing){
			StateChange("Idle");
		}
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
