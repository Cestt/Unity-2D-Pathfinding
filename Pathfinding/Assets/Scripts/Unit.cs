using UnityEngine;
using System;
using System.Collections;

public class Unit : MonoBehaviour {

	public Transform target;
	public float speed;
	public bool drawPath;
	Vector3[] path;
	public Vector3 oldTargetPos;
	int targetIndex;
	Agent agent;

	PathRequestManager.PathRequest queueIndex;

	void Start(){
		agent = target.GetComponent<Agent>();
		queueIndex = PathRequestManager.RequestPath(transform.position,target.transform.position, OnPathFound);
		InvokeRepeating("PathTry",0,0.1f);
	}

	void PathTry(){
		PathRequestManager.RemoveFromQeue(queueIndex);
		queueIndex = PathRequestManager.RequestPath(transform.position,target.transform.position, OnPathFound);
	}
	public void OnPathFound(Vector3[] newPath,bool pathSuccessful){
		if(pathSuccessful){
			path = null;
			path = newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	IEnumerator FollowPath(){
		if(path [0] != null){
			
			Vector3 currentWayPoint = path[0];
			while(true){
				if(transform.position == currentWayPoint){
					targetIndex++;
					if(targetIndex >= path.Length){
						yield break;
					}
					currentWayPoint = path[targetIndex];
				}
				transform.position = Vector3.MoveTowards(transform.position,currentWayPoint,speed * Time.fixedDeltaTime);
				yield return null;
			}
		}
	}

	public void OnDrawGizmos() {
		if(drawPath){
			if (path != null) {
				for (int i = targetIndex; i < path.Length; i ++) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube(path[i], Vector3.one);
					
					if (i == targetIndex) {
						Gizmos.DrawLine(transform.position, path[i]);
					}
					else {
						Gizmos.DrawLine(path[i-1],path[i]);
					}
				}
			}
		}
			
	}

}
