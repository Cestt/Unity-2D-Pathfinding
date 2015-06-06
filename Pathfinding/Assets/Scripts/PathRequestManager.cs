using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class PathRequestManager : MonoBehaviour {

	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager manager;
	PathFinding pathFinding;

	bool isProcessingPath;

	void Awake(){
		manager = this;
		pathFinding = GetComponent<PathFinding>(); 
	}

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback){
		PathRequest newPathRequest = new PathRequest(pathStart,pathEnd,callback);
		manager.pathRequestQueue.Enqueue(newPathRequest);
		manager.TryProcessNext();
	}
	public static void ClearQeue(){
		manager.pathRequestQueue.Clear();
	}
	void TryProcessNext(){
		if(!isProcessingPath & pathRequestQueue.Count > 0){
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
			pathFinding.StartFindPath(currentPathRequest.pathStart,currentPathRequest.pathEnd);
		}
	}
	public void FinishedProcessingPath(Vector3[] path, bool success){
		currentPathRequest.callback(path,success);
		isProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest{
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _pathStart, Vector3 _pathEnd, Action<Vector3[], bool> _callback){
			pathStart = _pathStart;
			pathEnd = _pathEnd;
			callback = _callback;
		}
	}
}
