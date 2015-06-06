using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public Transform target;
	public float speed;
	public bool drawPath;
	Vector3[] path;
	int targetIndex;
	public Vector3 oldPosition;

	void Start(){
		oldPosition = target.position;
		PathRequestManager.RequestPath(transform.position,target.transform.position, OnPathFound);
	}
	void Update(){
		if(oldPosition != target.position){
			PathRequestManager.ClearQeue();
			PathRequestManager.RequestPath(transform.position,target.transform.position, OnPathFound);

		}

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
